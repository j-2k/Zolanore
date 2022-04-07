using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderNumScript : MonoBehaviour
{
    Slider sliderSetting;
    TextMeshProUGUI sliderText;
    // Start is called before the first frame update

    private void Awake()
    {
        sliderSetting = GetComponentInChildren<Slider>();
        if (GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            sliderText = GetComponentInChildren<TextMeshProUGUI>();
            sliderText.text = sliderSetting.value.ToString();
        }
    }

    public void ChangeTextValue()
    {
        sliderText.text = sliderSetting.value.ToString();
    }

    public void ChangeTextValueSensitivity(TMP_InputField input)
    {
        input.text = (Mathf.Round(sliderSetting.value * 100f) / 100f).ToString();
    }

    public void ChangeSliderValue(TMP_InputField input)
    {
        sliderSetting.value = Mathf.Abs(float.Parse(input.text));
    }

    private void OnEnable()
    {
        if (GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            sliderText.text = sliderSetting.value.ToString();
        }
    }
}
