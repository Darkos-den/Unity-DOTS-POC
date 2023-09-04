using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(CellRenderSystem))]
public partial class WinCheckSystem : SystemBase {

    protected override void OnUpdate() {

        if (Input.GetMouseButtonDown(0)) {

            var turn = SystemAPI.GetSingleton<TurnComponent>().Invert();
            List<int> indexs = new();

            int total = 0;

            foreach ((var position, var cell) in SystemAPI.Query<PositionComponent, CellComponent>()) {
                if (cell.State == turn) {
                    indexs.Add(position.Y * 3 + position.X);
                }
                total++;
            }

            var horizontales = (indexs.Contains(0) && indexs.Contains(1) && indexs.Contains(2)) ||
                (indexs.Contains(3) && indexs.Contains(4) && indexs.Contains(5)) ||
                (indexs.Contains(6) && indexs.Contains(7) && indexs.Contains(8));
            var verticales = (indexs.Contains(0) && indexs.Contains(3) && indexs.Contains(6)) ||
                (indexs.Contains(1) && indexs.Contains(4) && indexs.Contains(7)) ||
                (indexs.Contains(2) && indexs.Contains(5) && indexs.Contains(8));
            var diagonales = (indexs.Contains(0) && indexs.Contains(4) && indexs.Contains(8)) ||
                (indexs.Contains(2) && indexs.Contains(4) && indexs.Contains(6));

            var events = SystemAPI.ManagedAPI.GetSingleton<GameEventComponent>();

            if (horizontales || verticales || diagonales) {
                events.OnUserWin.Invoke();
                RemoveSelectables();
            } else if (total == 9) {
                events.OnDraw.Invoke();
            }
        }
    }

    private void RemoveSelectables() {
        foreach (var item in SystemAPI.Query<EnabledRefRW<SelectableComponent>>()) {
            item.ValueRW = false;
        }
    }
}