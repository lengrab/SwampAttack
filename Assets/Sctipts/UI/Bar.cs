using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider _slider;

    public void OnValueChanged(int value, int maxValue)
    {
        _slider.value = (float)value / (float)maxValue;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
}
