using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameEventComponent : IComponentData {

    public GameEvent OnDraw;
    public GameEvent OnUserWin;
}
