using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerSpawner : MonoBehaviour
{
    [SerializeField] Transform[] _gateBounds;
    [SerializeField] GameObject _strikerPrefab;

    GameObject _striker, _firstStriker;

    Vector2 _spawnPos;
    float _delayBetweenWaves;
    float _delayBeforeLevel = 5f;
    int _wavesSpawned;

    ObjectPool _pool;
    LevelInfo _level;

    void Start()
    {
        _pool = ObjectPool.Instance;
        _level = GameManager.Instance.CurrentLevelInfo;
        _delayBetweenWaves = _level.LevelTime / _level.StrikerWavesAmount;
        StartCoroutine(ISpawnWave());
    }

    IEnumerator ISpawnWave()
    {
        if (_wavesSpawned == 0)
            yield return new WaitForSeconds(_delayBeforeLevel);
        SpawnStrikers();
        _wavesSpawned++;

        if(_wavesSpawned < _level.StrikerWavesAmount)
        {
            yield return new WaitForSeconds(_delayBetweenWaves);
            StartCoroutine(ISpawnWave());
        }
        else
        {
            yield return null;
        }

    }

    void SpawnStrikers()
    {

        int strikerAmount = Random.Range(2, _level.MaxStrikerAmountPerWave + 1);

        for (int i = 0; i < strikerAmount; i++)
        {
            if (i == 0)
            {
                _spawnPos = new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(-5f, -2f));
                _striker = _pool.Spawn(_strikerPrefab, _spawnPos, Quaternion.identity, transform);

                _striker.GetComponent<Striker>().DirectionLeft = !CheckIsLeftSpawn(_spawnPos);
                _firstStriker = _striker;
            }
            else
            {
                if (CheckIsLeftSpawn(_spawnPos))
                {
                    _spawnPos.x = Random.Range(_spawnPos.x + .5f, _spawnPos.x + 1.5f);
                }
                else
                {
                    _spawnPos.x = Random.Range(_spawnPos.x - .5f, _spawnPos.x - 1.5f);
                }
                _spawnPos.y = Random.Range(-5f, -2f);

                _striker = _pool.Spawn(_strikerPrefab, _spawnPos, Quaternion.identity, transform);
                _striker.GetComponent<Striker>().DirectionLeft = !_firstStriker.GetComponent<Striker>().DirectionLeft;
            }
            _striker.GetComponent<Striker>()._bounds = _gateBounds;
            _striker.GetComponent<Striker>().Init(strikerAmount / 2);
        }
    }

    bool CheckIsLeftSpawn(Vector2 spawnPos)
    {
        if(spawnPos.x < -1.5f || spawnPos.x > 0f && spawnPos.x < 1.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
