using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(MouseInputSystem))]
public partial struct CellRenderSystem : ISystem {

    public void OnCreate(ref SystemState state) {
    }

    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state) {
        var turn = SystemAPI.GetSingleton<TurnComponent>();
        var sprites = SystemAPI.ManagedAPI.GetSingleton<SpritesComponent>();

        foreach ((var _, var entity) in SystemAPI.Query<HighlightTag>().WithDisabled<HighlightComponent>().WithAll<SelectableComponent>().WithEntityAccess()) {
            var spriteRenderer = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(entity);
            spriteRenderer.sprite = sprites.SelectSprite(turn.State);

            SystemAPI.SetComponentEnabled<HighlightComponent>(entity, true);
        }

        foreach ((var _, var entity) in SystemAPI.Query<HighlightComponent>().WithDisabled<HighlightTag>().WithAll<SelectableComponent>().WithEntityAccess()) {
            var spriteRenderer = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(entity);
            spriteRenderer.sprite = sprites.Empty;

            SystemAPI.SetComponentEnabled<HighlightComponent>(entity, false);
        }

        foreach ((var _, var entity) in SystemAPI.Query<CellTag>().WithDisabled<CellComponent>().WithEntityAccess()) {
            var cell = SystemAPI.GetComponent<CellComponent>(entity);
            var spriteRenderer = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(entity);
            spriteRenderer.sprite = sprites.SelectSprite(cell.State);

            SystemAPI.SetComponentEnabled<CellComponent>(entity, true);
        }
    }

}
