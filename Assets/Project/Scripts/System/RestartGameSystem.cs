using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

[BurstCompile]
public partial struct RestartGameSystem : ISystem {

    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<RestartComponent>();
    }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        var job = new ResetCellStateJob { };
        job.ScheduleParallel();

        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        var x = SystemAPI.GetSingletonEntity<RestartComponent>();
        ecb.RemoveComponent<RestartComponent>(x);

        ecb.Playback(state.EntityManager);
    }

    [BurstCompile]
    [WithOptions(EntityQueryOptions.IgnoreComponentEnabledState)]
    public partial struct ResetCellStateJob : IJobEntity {

        public void Execute(EnabledRefRW<CellComponent> cell, EnabledRefRW<CellTag> tag, EnabledRefRW<SelectableComponent> selectable) {
            cell.ValueRW = false;
            tag.ValueRW = false;
            selectable.ValueRW = true;
        }
    }
}
