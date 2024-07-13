using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Striker : MonoBehaviour
{
    public bool DirectionLeft;

    [SerializeField] GameObject _ballPrefab;
    [SerializeField] float _ballSpeed; 
    float _randomXSpawnPosition;
    public Transform[] _bounds;
    ObjectPool _pool;
    Vector2 _strikePos;
        

    void Start()
    {
        _pool = ObjectPool.Instance;
        if (DirectionLeft)
        {
            _randomXSpawnPosition = Random.Range(_bounds[1].position.x, 0f);
        }
        else
        {
            _randomXSpawnPosition = Random.Range(0f, _bounds[2].position.x);
        }

        GameObject ball = _pool.Spawn(_ballPrefab, transform.position, Quaternion.identity);
        _strikePos = new Vector2(_randomXSpawnPosition, _bounds[0].position.y);
        ball.GetComponent<Ball>().Init(_strikePos);
    }

}
