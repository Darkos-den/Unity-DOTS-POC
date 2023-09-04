using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SampleHud : MonoBehaviour
{
    [SerializeField]
    private TurnVariable state;

    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = state.state.ToString();
    }


}
