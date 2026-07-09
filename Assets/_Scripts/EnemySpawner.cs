using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField] GameObject _enemyGameObject;

    [SerializeField] List<Transform> _enemySpawnPositionList;

    int _enemyCountMax = 6;
    int _enemyCountMin = 3;
    int _enemyCount;

    float _spawnTimer;
    float _spawnTimerMax = 5;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _enemyCount = _enemyCountMin;
    }

    private void Update()
    {
        if (GameManager.Instance.GameIsPlaying() == false) return;
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer < _spawnTimerMax) return;
        if(_enemyCount == _enemyCountMax) return;

        _spawnTimer = 0;
        Transform spawnTransform = _enemySpawnPositionList[Random.Range(0, _enemySpawnPositionList.Count)];
        Instantiate(_enemyGameObject, spawnTransform.position, Quaternion.identity);
        AddEnemyCount(1);
    }

    public void AddEnemyCount(int countToAdd)
    {
        _enemyCount += countToAdd;
    }
}
