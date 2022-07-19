using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Spawner _spawner;

    private void Awake()
    {
        _spawner.WaveEnded += OnWaveEnded;
        _button.onClick.AddListener(OnButtonClick);
        _button.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _spawner.WaveEnded -= OnWaveEnded;
    }

    private void OnButtonClick()
    {
        _button.gameObject.SetActive(false);
        
        if (_spawner.SetNextWave())
        {
            _spawner.SpawnWave();
        }
    }

    private void OnWaveEnded()
    {
        _button.gameObject.SetActive(true);
    }
}
