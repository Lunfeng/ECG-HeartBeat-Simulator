using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ECGPageControl : MonoBehaviour
{
    private Text speedText;
    private Slider speedSlider;

    // Start is called before the first frame update
    void Start()
    {
        speedText = transform.Find("SpeedText").GetComponent<Text>();
        speedSlider = transform.Find("SpeedSlider").GetComponent<Slider>();
    }

    public void SetSpeedText(float speed)
    {
        speedText.text = "≤•∑≈ÀŸ∂»£∫" + speed.ToString("#0.00");
    }

    public void SetSliderValue(float value)
    {
        speedSlider.value = value;
    }
}
