using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(ApplyUserChoiceSystem))]
public partial struct CellRenderSystem : ISystem {

    public void OnCreate(ref SystemState state) {
    }

    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state) {

        var turn = SystemAPI.GetSingleton<TurnComponent>();
        var sprites = SystemAPI.ManagedAPI.GetSingleton<SpritesComponent>();

        var mouseover = SystemAPI.GetSingleton<MouseOverComponent>();

        foreach ((var _, var entity) in SystemAPI.Query<PositionComponent>().WithEntityAccess()) {
            var item = SystemAPI.GetAspect<BoardItemAspect>(entity);

            var spriteRenderer = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(item.Self);
      
            if(item.IsSelected) {
                spriteRenderer.color = Color.white;
                spriteRenderer.sprite = sprites.SelectSprite(item.Cell);
            } else {
                spriteRenderer.color = Color.white;
                spriteRenderer.sprite = sprites.Empty;
            }
        }

        if (mouseover.Valid) {
            if(SystemAPI.HasComponent<PositionComponent>(mouseover.PrevTarget)) {
                var sr = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(mouseover.PrevTarget);
                sr.sprite = sprites.Empty;
            }

            var spriteRenderer = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(mouseover.Target);
            var color = Color.white;
            color.a = 0.5f;

            spriteRenderer.color = color;
            spriteRenderer.sprite = sprites.SelectSprite(turn.State);
        }
    }

}
