using Unity.Burst;
using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(InitializeBoardSystem))]
public partial class MouseoverInputSystem : SystemBase {

    private Camera _camera;

    private Camera MainCamera {
        get {
            if(_camera == null) {
                _camera = Camera.main;
            }
            return _camera;
        }
    }

    protected override void OnCreate() {
    }

    protected override void OnUpdate() {
        var component = SystemAPI.GetSingletonRW<MouseOverComponent>();
        component.ValueRW.Valid = false;

        var position = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        foreach (var item in SystemAPI.Query<BoardItemAspect>()) {
            if (item.Bounds.Contains(position)) {
                component.ValueRW.Valid = true;
                component.ValueRW.PrevTarget = component.ValueRO.Target;
                component.ValueRW.Target = item.Self;
                break;
            }
        }
    }
}
