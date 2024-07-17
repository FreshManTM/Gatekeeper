using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Striker : MonoBehaviour
{
    public bool DirectionLeft;

    [SerializeField] GameObject _ballPrefab;
    float _randomXSpawnPosition;
    public Transform[] _bounds;
    ObjectPool _pool;
    Vector2 _strikePos;

    GameObject _ball;
    public void Init(float strikeDelay)
    {
        _pool = ObjectPool.Instance;
        if (DirectionLeft)
        {
            _randomXSpawnPosition = Random.Range(_bounds[0].position.x, 0f);
        }
        else
        {
            _randomXSpawnPosition = Random.Range(0f, _bounds[1].position.x);
        }
        Vector2 ballSpawnPos = (Vector2)transform.position + Vector2.up *Vector2.one;
        _ball = _pool.Spawn(_ballPrefab, ballSpawnPos, Quaternion.identity);
        _strikePos = new Vector2(_randomXSpawnPosition, _bounds[0].position.y);
        StartCoroutine(IStrike(strikeDelay, ballSpawnPos));
    }
    
    IEnumerator IStrike(float delay, Vector2 ballPos)
    {
        delay -= Time.fixedDeltaTime;
        if(delay <= 0f)
        {
            _ball.GetComponent<Ball>().Init(_strikePos);
            Invoke("Despawn", 1f);
            yield return null;
        }
        else
        {
            yield return new WaitForFixedUpdate();
            var distance = Vector2.Distance(transform.position, ballPos);
            transform.position = Vector2.MoveTowards(transform.position, ballPos, distance / delay * Time.fixedDeltaTime);
            StartCoroutine(IStrike(delay, ballPos));
        }
    }

    void Despawn()
    {
        _pool.Despawn(gameObject);
    }

}
