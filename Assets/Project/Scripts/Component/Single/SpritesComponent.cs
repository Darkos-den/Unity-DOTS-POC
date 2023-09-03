using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpritesComponent : IComponentData
{
    public Sprite Player1;
    public Sprite Player2;
    public Sprite Empty;

    public Sprite SelectSprite(TurnState? state) {
        if (state == TurnState.Player1) {
            return Player1;
        } if (state == TurnState.Player2) { 
            return Player2;
        } else {
            return Empty;
        } 
    }
}
