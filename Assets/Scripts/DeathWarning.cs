using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathWarning : MonoBehaviour
{
    [SerializeField] private Image fillColor;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < 3)
        {
            fillColor.color = new Color32(255,57, 91, 255);
        }
        else
        {
            fillColor.color = new Color32(243,241, 112, 255);
        }
    }
}
