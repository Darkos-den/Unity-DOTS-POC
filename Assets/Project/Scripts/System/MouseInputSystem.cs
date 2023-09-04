using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[UpdateAfter(typeof(InitializeBoardSystem))]
public partial class MouseInputSystem : SystemBase {

    private Camera _camera;

    private Camera MainCamera {
        get {
            if(_camera == null) {
                _camera = Camera.main;
            }
            return _camera;
        }
    }

    protected override void OnCreate() {
    }

    protected override void OnUpdate() {
        var position = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        var stub = new StubJob { };
        var job = new CheckMousePositionJob { Position = position };
        var handle = job.ScheduleParallel(stub.Schedule());
        handle.Complete();

        if (Input.GetMouseButtonDown(0)) {
            CheckClickEvent();
        }
    }

    private void CheckClickEvent() {
        foreach ((var _, var entity) in SystemAPI.Query<HighlightTag>().WithEntityAccess()) {

            var cell = SystemAPI.GetComponentRW<CellComponent>(entity);
            var turn = SystemAPI.GetSingletonRW<TurnComponent>();
            var hud = SystemAPI.ManagedAPI.GetSingleton<TurnHudComponent>().State;

            cell.ValueRW.State = turn.ValueRO.State;
            turn.ValueRW.State = turn.ValueRO.Invert();
            hud.state = turn.ValueRO.State;

            SystemAPI.SetComponentEnabled<HighlightTag>(entity, false);
            SystemAPI.SetComponentEnabled<SelectableComponent>(entity, false);
            SystemAPI.SetComponentEnabled<CellTag>(entity, true);
            break;
        }
    }

    [BurstCompile]
    public partial struct StubJob : IJob {
        public void Execute() {
            //do nothing
        }
    }

    [BurstCompile]
    [WithOptions(EntityQueryOptions.IgnoreComponentEnabledState)]
    [WithAll(typeof(SelectableComponent))]
    public partial struct CheckMousePositionJob : IJobEntity {

        public Vector3 Position;

        public void Execute(BoardItemAspect item, EnabledRefRO<HighlightComponent> component, EnabledRefRW<HighlightTag> tag) {
            if (item.Bounds.Contains(Position)) {
                if (!component.ValueRO) {
                    tag.ValueRW = true;
                }
            } else {
                if (tag.ValueRO) {
                    tag.ValueRW = false;
                }
            }
        }
    }
}
