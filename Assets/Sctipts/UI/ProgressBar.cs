using UnityEngine;

public class ProgressBar:Bar
{
    [SerializeField] private Spawner _spawner;
    
    private void OnEnable()
    {
        _spawner.EnemySpawned += OnValueChanged;
    }

    private void OnDisable()
    {
        _spawner.EnemySpawned -= OnValueChanged;
    }   
}
