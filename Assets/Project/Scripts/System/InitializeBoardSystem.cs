using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
[BurstCompile]
public partial struct InitializeBoardSystem : ISystem {

    [BurstCompile]
    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<PrefbsComponent>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {

        PrefbsComponent prefabs = SystemAPI.GetSingleton<PrefbsComponent>();

        EntityCommandBuffer ecb = new(Allocator.TempJob);
        EntityCommandBuffer.ParallelWriter parallelEcb = ecb.AsParallelWriter();

        var spawnJob = new SpawnBoardItemJob { 
            ECB = parallelEcb,
            Prefab = prefabs.BoardItemPrefab,
        };
        var spawnHandle = spawnJob.Schedule(9, 1);
        spawnHandle.Complete();

        ecb.Playback(state.EntityManager);

        state.Enabled = false;
    }

    [BurstCompile]
    public partial struct SpawnBoardItemJob : IJobParallelFor {

        public EntityCommandBuffer.ParallelWriter ECB;
        public Entity Prefab;

        [BurstCompile]
        public void Execute(int index) {
            var ecb = ECB;
            Entity newEntity = ecb.Instantiate(index, Prefab);

            var position = ProcessPosition(index);
            var cell = new CellComponent { State = TurnState.Player1 };
            var transform = ProcessTransform(position);

            ecb.AddComponent(index, newEntity, position);
            ecb.AddComponent(index, newEntity, cell);
            ecb.SetComponent(index, newEntity, transform);

            ecb.AddComponent<SelectableComponent>(index, newEntity);
            ecb.AddComponent<HighlightComponent>(index, newEntity);
            ecb.AddComponent<HighlightTag>(index, newEntity);
            ecb.AddComponent<CellTag>(index, newEntity);

            ecb.SetComponentEnabled<HighlightComponent>(index, newEntity, false);
            ecb.SetComponentEnabled<HighlightTag>(index, newEntity, false);
            ecb.SetComponentEnabled<CellComponent>(index, newEntity, false);
            ecb.SetComponentEnabled<CellTag>(index, newEntity, false);
        }

        private PositionComponent ProcessPosition(int index) {
            return new PositionComponent {
                X = index / 3,
                Y = (index + 1) % 3,
            };
        }

        private LocalTransform ProcessTransform(PositionComponent position) {
            return LocalTransform.FromPosition(
                new float3(
                    position.X * 1.25f,
                    position.Y * 1.25f,
                    -2f
                )
           );
        }
    }
}
