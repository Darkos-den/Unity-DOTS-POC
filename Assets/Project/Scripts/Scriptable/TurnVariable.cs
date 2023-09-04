using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/TurnVariable", fileName = "TurnState")]
public class TurnVariable : ScriptableObject, ISerializationCallbackReceiver {

    public TurnState initialState;

    [NonSerialized]
    public TurnState state;

    public void OnAfterDeserialize() {
        state = initialState;
    }

    public void OnBeforeSerialize() {
        
    }
}
