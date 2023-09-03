using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct MouseOverComponent : IComponentData {
    public Entity Target;
    public Entity PrevTarget;
    public bool Valid;
}
