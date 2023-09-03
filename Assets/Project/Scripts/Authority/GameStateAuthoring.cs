using Unity.Entities;
using UnityEngine;

public class GameStateAuthoring : MonoBehaviour {

    [SerializeField]
    private TurnState _initialState;

    class Baker : Baker<GameStateAuthoring> {
        public override void Bake(GameStateAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new TurnComponent { State = authoring._initialState });
            AddComponent(entity, new MouseOverComponent { Target = entity, Valid = false, PrevTarget = entity });
        }
    }
}
