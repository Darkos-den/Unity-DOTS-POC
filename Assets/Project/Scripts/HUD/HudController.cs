using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    [SerializeField] private Image _playerImage;

    [SerializeField] private Sprite _player1;
    [SerializeField] private Sprite _player2;

    private void Start() {
        _playerImage.sprite = _player1;
    }

    private void OnEnable() {
        var system = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<ApplyUserChoiceSystem>();
        system.OnTurnChanged += OnTurnChanged;
    }

    private void OnDisable() {
        if (World.DefaultGameObjectInjectionWorld == null) return;
        var system = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<ApplyUserChoiceSystem>();
        system.OnTurnChanged -= OnTurnChanged;
    }

    public void OnTurnChanged(TurnState state) {
        if (state == TurnState.Player1) {
            _playerImage.sprite = _player1;
        } else {
            _playerImage.sprite = _player2;
        }
    }
}
