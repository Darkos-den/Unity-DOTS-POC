using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpritesAuthoring : MonoBehaviour
{
    [SerializeField] private Sprite _spriteUser1;
    [SerializeField] private Sprite _spriteUser2;
    [SerializeField] private Sprite _spriteEmpty;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    class Baker : Baker<SpritesAuthoring> {
        public override void Bake(SpritesAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponentObject(entity, new SpritesComponent {
                Player1 = authoring._spriteUser1,
                Player2 = authoring._spriteUser2,
                Empty = authoring._spriteEmpty,
            });
        }
    }
}
