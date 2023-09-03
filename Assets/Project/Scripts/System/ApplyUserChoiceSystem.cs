using System;
using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(MouseoverInputSystem))]
public partial class ApplyUserChoiceSystem : SystemBase {

    public Action<TurnState> OnTurnChanged;

    protected override void OnCreate() {
    }

    protected override void OnUpdate() {
        if (Input.GetMouseButtonDown(0)) {
            var mouseover = SystemAPI.GetSingletonRW<MouseOverComponent>();
            if (!mouseover.ValueRO.Valid) {
                return;
            }

            var turn = SystemAPI.GetSingletonRW<TurnComponent>();
            var boardItem = SystemAPI.GetAspect<BoardItemAspect>(mouseover.ValueRO.Target);

            boardItem.IsSelectable = false;
            boardItem.Cell = turn.ValueRO.State;
            boardItem.IsSelected = true;

            turn.ValueRW.State = turn.ValueRO.Invert();
            OnTurnChanged?.Invoke(turn.ValueRO.State);
        }
    }
}