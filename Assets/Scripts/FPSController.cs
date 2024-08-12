using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FPSController : MonoBehaviour
{
    //public static FPSController instance;

    [SerializeField] private Slider fpsSlider;
    [SerializeField] private TextMeshProUGUI fpsText;

    private void Start()
    {
        fpsSlider.value = 60;
        fpsText.text = "60";
        Application.targetFrameRate = 60;
        fpsSlider.onValueChanged.AddListener(ChangeFPS);
    }

    private void ChangeFPS(float arg0)
    {
        Application.targetFrameRate = (int)arg0;
        fpsText.text = fpsSlider.value.ToString();  
    }
}
