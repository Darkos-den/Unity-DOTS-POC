using Unity.Entities;
using UnityEngine;

public class GameStateAuthoring : MonoBehaviour {

    [SerializeField]
    private TurnState _initialState;

    [SerializeField]
    private TurnVariable _variable;

    [SerializeField]
    private GameEvent _onDraw;

    [SerializeField]
    private GameEvent _onUserWin;

    class Baker : Baker<GameStateAuthoring> {
        public override void Bake(GameStateAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.None);

            authoring._variable.state = authoring._initialState;

            AddComponent(entity, new TurnComponent { State = authoring._initialState });
            AddComponentObject(entity, new TurnHudComponent { State = authoring._variable });
            AddComponentObject(entity, new GameEventComponent { OnDraw = authoring._onDraw, OnUserWin = authoring._onUserWin });
        }
    }
}
