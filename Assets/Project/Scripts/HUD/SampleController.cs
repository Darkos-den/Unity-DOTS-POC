using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleController : MonoBehaviour
{
    [SerializeField]
    private TurnVariable state;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSampleClick() {
        if(state.state == TurnState.Player1) {
            state.state = TurnState.Player2;
        } else {
            state.state = TurnState.Player1;
        }
    }
}
