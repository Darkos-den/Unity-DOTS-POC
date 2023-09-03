using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

class SpawnerAuthoring : MonoBehaviour {
    public GameObject Prefab;
    public float SpawnRate;

    public Sprite Sprite;

    public SpriteRenderer spriteRenderer;
}

class SpawnerBaker : Baker<SpawnerAuthoring> {
    public override void Bake(SpawnerAuthoring authoring) {
        var entity = GetEntity(TransformUsageFlags.None);

        Spawner spawner = new Spawner();
        spawner.sprite = authoring.Sprite;

        AddComponentObject(entity, spawner);
        //AddComponentObject(entity, authoring.spriteRenderer);
    }
}
