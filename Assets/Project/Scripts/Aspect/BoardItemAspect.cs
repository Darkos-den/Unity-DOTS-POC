using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct BoardItemAspect : IAspect {

    public readonly Entity Self;

    private readonly RefRO<LocalTransform> _transform;
    private readonly RefRO<PositionComponent> _position;

    public PositionComponent Position {
        get => _position.ValueRO;
    }

    public Rect Bounds {
        get {
            var transform = _transform.ValueRO;
            return new Rect { x = transform.Position.x - 0.5f, y = transform.Position.y - 0.5f, width = 1, height = 1 };
        }
    }
    public int PostitionIndex {
        get => _position.ValueRO.Y * 3 + _position.ValueRO.X;
    }
    
}
