using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;
    [SerializeField] private float _delay = 3f;
    [SerializeField] private List<Wave> _waves;

    private int _waveIndex = 0;
    private int _countSpawned = 0;
    private WaitForSeconds _waitForWaves;
    private WaitForSeconds _waitForWave;

    public event UnityAction WaveEnded;
    public event UnityAction<int,int> EnemySpawned;
    
    public int WaveIndex => _waveIndex;
    public int WaveCount => _countSpawned;
    public int SpawnCount => _countSpawned;

    public bool SetNextWave()
    {
        if (++_waveIndex < _waves.Count)
        {
            _countSpawned = 0;
            return true;
        }
        
        EnemySpawned.Invoke(0,1);
        return false;
    }

    public void SpawnWave()
    {
        StartCoroutine(SpawnWave(_waves[_waveIndex]));
    }
    

    private void Awake()
    {
        _waveIndex = 0;
        _waitForWaves = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        StartCoroutine(SpawnWave(_waves[_waveIndex]));
    }

    private IEnumerator Wait()
    { 
        yield return _waitForWaves;
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        _waitForWave = new WaitForSeconds(wave.Delay);
        
        for (int i = 0; i < wave.Count; i++)
        {
            Spawn(wave.Template);
            EnemySpawned.Invoke(WaveCount,wave.Count);
            yield return _waitForWave;
        }
        
        WaveEnded.Invoke();
    }

    private void Spawn(GameObject template)
    {
        GameObject enemyGameObject = Instantiate(template);
        
        if (enemyGameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Init(_player);
            _player.ListOnEnemy(enemy);
        }

        enemy.transform.position = _spawnPoint.transform.position;
        _countSpawned++;
    }
}

[Serializable] 
public class Wave
{
    public int Count = 2;
    public GameObject Template;
    public float Delay;
}
