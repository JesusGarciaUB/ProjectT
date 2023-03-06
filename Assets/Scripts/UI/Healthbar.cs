using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textMesh;

    public void SetMaxHealth(int value)
    {
        slider.maxValue = value;
        slider.value = value;
        textMesh.text = value.ToString();
    }
    public void SetHealth(int value)
    {
        slider.value -= value;
        textMesh.text = slider.value.ToString();
    }
}
