using Unity.Burst;
using Unity.Collections;
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

        var result = new NativeArray<bool>(1, Allocator.TempJob);
        var turn = SystemAPI.GetSingletonRW<TurnComponent>();

        var isClick = Input.GetMouseButtonDown(0);
        result[0] = false;
        var job = new CheckMousePositionJob { 
            Position = position, 
            IsClick = isClick,
            TurnState = turn.ValueRO.State,
            Result = result,
        };
        job.ScheduleParallel(new StubJob().Schedule()).Complete();

        if (result[0]) {
            turn.ValueRW.State = turn.ValueRO.Invert();

            var hud = SystemAPI.ManagedAPI.GetSingleton<TurnHudComponent>().State;
            hud.state = turn.ValueRO.State;
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
        public bool IsClick;
        public TurnState TurnState;

        public NativeArray<bool> Result;

        public void Execute(
            BoardItemAspect item, 
            EnabledRefRO<HighlightComponent> component, 
            EnabledRefRW<HighlightTag> tag,
            EnabledRefRW<SelectableComponent> selectable,
            RefRW<CellComponent> cell,
            EnabledRefRW<CellTag> cellTag,
            [EntityIndexInQuery] int sortKey
        ) {
            if (item.Bounds.Contains(Position)) {
                if (IsClick) {
                    cell.ValueRW.State = TurnState;
                    Result[0] = true;

                    tag.ValueRW = false;
                    selectable.ValueRW = false;
                    cellTag.ValueRW = true;
                } else if (!component.ValueRO) {
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
