using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct CellComponent : IComponentData, IEnableableComponent {

    public TurnState State;
}
