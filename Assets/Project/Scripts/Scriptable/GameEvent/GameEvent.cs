using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/GameEvent", fileName = "GameEvent")]
public class GameEvent : ScriptableObject {

    private List<GameEventListener> _listeners = new();

    public void Invoke() {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Invoke();
    }

    public void RegisterListener(GameEventListener listener) { _listeners.Add(listener); }

    public void UnregisterListener(GameEventListener listener) { _listeners.Remove(listener); }
}
