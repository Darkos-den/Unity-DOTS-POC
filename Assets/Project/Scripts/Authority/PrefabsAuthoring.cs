using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PrefabsAuthoring : MonoBehaviour
{
    [SerializeField]
    private GameObject _boardItemPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    class Baker : Baker<PrefabsAuthoring> {
        public override void Bake(PrefabsAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new PrefbsComponent { 
                BoardItemPrefab = GetEntity(authoring._boardItemPrefab, TransformUsageFlags.Dynamic) 
            });
        }
    }
}
