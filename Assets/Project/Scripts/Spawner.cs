using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : IComponentData {

    public Sprite sprite;
}

public struct SpawnerTag : IComponentData { }