using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperSpawner : MonoBehaviour
{
    [SerializeField] GameObject _keeperPrefab;

    [SerializeField] int _maxKeeperAmount;
    [SerializeField] float _keeperSize;
    [SerializeField] float _keeperMoveSpeed;

    List<GameObject> _keepers = new List<GameObject>();
    List<GameObject> _keepersOnDrag = new List<GameObject>();
    int _currentKeeperAmount, _keeperAmount;
    Vector2 _spawnOffset;
    ObjectPool _pool;

    private void Start()
    {
        _pool = ObjectPool.Instance;
        //_pool.PreLoad(_keeperPrefab, 10);
    }

    public void SpawnKeeper()
    {
        _keepersOnDrag.Add(_pool.Spawn(_keeperPrefab, Vector2.zero, Quaternion.identity, transform));
        _currentKeeperAmount++;
    }

    public void SpawnKeepersOnDrag(Vector2 startTouch, Vector2 mousePosition)
    {
        float distance = Vector2.Distance(startTouch, mousePosition);

        if (distance > _currentKeeperAmount * _keeperSize && _keeperAmount + _currentKeeperAmount < _keeperAmount + _maxKeeperAmount)
        {
            SpawnKeeper();
        }
        else if (distance< (_currentKeeperAmount * _keeperSize) - _keeperSize)
        {
            DespawnKeeper(_keepersOnDrag[_keepersOnDrag.Count - 1]);
            _keepersOnDrag.RemoveAt(_keepersOnDrag.Count - 1);
            _currentKeeperAmount--;
        }

        _spawnOffset = Vector2.zero;
        foreach (var keeper in _keepersOnDrag)
        {
            keeper.transform.position = startTouch + _spawnOffset * (mousePosition - startTouch).normalized;
            _spawnOffset += new Vector2(_keeperSize, _keeperSize);
        }
    }

    public void SetKeepers()
    {
        StopAllCoroutines();

        foreach (var keeper in _keepersOnDrag)
        {
            if (_keeperAmount < _maxKeeperAmount)
            {
                _keepers.Add(keeper);
                _keeperAmount++;
                Color color = keeper.GetComponent<SpriteRenderer>().color;
                color.a = 1;
                keeper.GetComponent<SpriteRenderer>().color = color;
                keeper.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                var destinationPos = keeper.transform.position;
                StartCoroutine(IMoveKeeper(_keepers[0].transform, destinationPos));
                _keepers.Add(_keepers[0]);
                _keepers.Remove(_keepers[0]);
                DespawnKeeper(keeper);
            }
        }

        _spawnOffset = Vector2.zero;
        _currentKeeperAmount = 0;
        _keepersOnDrag.Clear();
    }

    IEnumerator IMoveKeeper(Transform keeper, Vector2 destinationPos)
    {
        if ((Vector2)keeper.position != destinationPos)
        {
            keeper.position = Vector2.MoveTowards(keeper.position, destinationPos, _keeperMoveSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            StartCoroutine(IMoveKeeper(keeper, destinationPos));
        }
        else
        {
            yield return null;
        }

    }

    public void DespawnKeeper(GameObject keeper)
    {
        _pool.Despawn(keeper);
    }
}
