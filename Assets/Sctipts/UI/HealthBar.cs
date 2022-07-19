using System;
using UnityEngine;

public class HealthBar:Bar
{
    [SerializeField] private Player _player;
    
    private void OnEnable()
    {
        _player.HealthChaged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.HealthChaged -= OnValueChanged;
    }
}
