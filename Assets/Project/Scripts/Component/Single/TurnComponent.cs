using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public enum TurnState {
    Player1, Player2
}

public struct TurnComponent : IComponentData
{
    public TurnState State;

    public static TurnState Invert(TurnState state) {
        if (state == TurnState.Player2) {
            return TurnState.Player1;
        } else {
            return TurnState.Player2;
        }
    }

    public TurnState Invert() {
        if (State == TurnState.Player2) {
            return TurnState.Player1;
        } else {
            return TurnState.Player2;
        }
    }
}
