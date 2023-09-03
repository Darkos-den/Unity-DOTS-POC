using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct SpawnerSystem : ISystem {
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state) {
        // Queries for all Spawner components. Uses RefRW because this system wants
        // to read from and write to the component. If the system only needed read-only
        // access, it would use RefRO instead.

        foreach ((Spawner spawner, Entity entity) in SystemAPI.Query<Spawner>().WithEntityAccess()) {
            var spriteRenderer = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(entity);

            if(spriteRenderer.sprite != spawner.sprite) {
                spriteRenderer.sprite = spawner.sprite;
            }
        }
        

        
        
    }

    private void ProcessSpawner(ref SystemState state) {
        // If the next spawn time has passed.
        //if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime) {
            // Spawns a new entity and positions it at the spawner.
            //Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
            // LocalPosition.FromPosition returns a Transform initialized with the given position.
            //state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(spawner.ValueRO.SpawnPosition));

            // Resets the next spawn time.
        //    spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;

       //     var spriteRenderer = SystemAPI.ManagedAPI.GetSingleton<SpriteRenderer>();
            //spriteRenderer.sprite = Sprite.Create();
        //    spriteRenderer.color = Color.red;
       // }
    }
}
