using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public Slider sliderSensivity;
    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayerSensivity"))
            sliderSensivity.value = PlayerPrefs.GetFloat("PlayerSensivity");
    }
    public void SensivityChange()
    {
        PlayerPrefs.SetFloat("PlayerSensivity", sliderSensivity.value);
    }
}
