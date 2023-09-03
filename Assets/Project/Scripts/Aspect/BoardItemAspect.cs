using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public readonly partial struct BoardItemAspect : IAspect {

    public readonly Entity Self;

    private readonly RefRO<LocalTransform> _transform;
    private readonly RefRO<PositionComponent> _position;

    private readonly EnabledRefRW<SelectableComponent> _selectable;
    private readonly RefRW<CellComponent> _cell;

    public PositionComponent Position {
        get => _position.ValueRO;
    }

    public Rect Bounds {
        get {
            var transform = _transform.ValueRO;
            return new Rect { x = transform.Position.x - 0.5f, y = transform.Position.y - 0.5f, width = 1, height = 1 };
        }
    }

    public bool IsSelected {
        get => _cell.ValueRO.Valid;
        set => _cell.ValueRW.Valid = value;
    }

    public bool IsSelectable {
        get => _selectable.ValueRO;
        set => _selectable.ValueRW = value;
    }

    public TurnState Cell {
        get => _cell.ValueRO.State;
        set => _cell.ValueRW.State = value;
    }
}
