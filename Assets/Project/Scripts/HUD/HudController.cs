using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    [SerializeField] private Image _playerImage;

    [SerializeField] private Sprite _player1;
    [SerializeField] private Sprite _player2;

    [SerializeField] private TurnVariable _turn;

    private TurnState _currentState;

    private void Start() {
        _playerImage.sprite = _player1;
    }

    private void OnEnable() {
        
    }

    private void OnDisable() {
        
    }

    private void Update() {
        OnTurnChanged(_turn.state);
    }

    public void OnTurnChanged(TurnState state) {
        if(state == _currentState) {
            return;
        }
        _currentState = state;
        Debug.Log(">> " + state);
        if (state == TurnState.Player1) {
            _playerImage.sprite = _player1;
        } else {
            _playerImage.sprite = _player2;
        }
    }
}
