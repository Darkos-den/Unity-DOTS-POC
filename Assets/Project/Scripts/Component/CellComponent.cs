using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct CellComponent : IComponentData {

    public TurnState State;
    public bool Valid;
}
