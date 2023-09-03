using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public partial struct RestartGameSystem : ISystem {

    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<RestartComponent>();
    }

    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state) {
        

        foreach ((var _, var entity) in SystemAPI.Query<PositionComponent>().WithEntityAccess()) {
            var item = SystemAPI.GetAspect<BoardItemAspect>(entity);

            item.IsSelectable = true;
            item.IsSelected = false;
        }

        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        var x = SystemAPI.GetSingletonEntity<RestartComponent>();
        ecb.RemoveComponent<RestartComponent>(x);

        ecb.Playback(state.EntityManager);
    }
}
