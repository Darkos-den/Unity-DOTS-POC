using TMPro;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class PauseHudController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _hud;

    private void OnEnable() {
    }

    private void OnDisable() {
    }

    public void OnUserWin() {
        _hud.SetActive(true);

        _text.text = "You WIN";
    }

    public void OnDraw() {
        _hud.SetActive(true);

        _text.text = "DRAW";
    }

    public void OnRestartClick() {
        //todo: needs refactor

        _hud.SetActive(false);

        var world = World.DefaultGameObjectInjectionWorld;
        var entityManager = world.EntityManager;
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);

        foreach(var entity in entityManager.GetAllEntities()) {
            ecb.AddComponent<RestartComponent>(entity);
            break;
        }

        ecb.Playback(entityManager);
    }
}
