using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(ApplyUserChoiceSystem))]
public partial class WinCheckSystem : SystemBase {

    public Action<TurnState> OnUserWin;
    public Action OnDraw;

    protected override void OnUpdate() {

        if (Input.GetMouseButtonDown(0)) {

            var turn = SystemAPI.GetSingleton<TurnComponent>().Invert();
            List<int> indexs = new();

            int total = 0;

            foreach ((var _, var entity) in SystemAPI.Query<PositionComponent>().WithDisabled<SelectableComponent>().WithEntityAccess()) {
                var item = SystemAPI.GetAspect<BoardItemAspect>(entity);

                if (item.Cell == turn) {
                    indexs.Add(item.Position.Y * 3 + item.Position.X);
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

            if (horizontales || verticales || diagonales) {
                OnUserWin?.Invoke(turn);
                RemoveSelectables();
            } else if (total == 9) {
                OnDraw?.Invoke();
            }
        }
    }

    private void RemoveSelectables() {
        foreach (var item in SystemAPI.Query<BoardItemAspect>()) {
            item.IsSelectable = false;
        }
    }
}