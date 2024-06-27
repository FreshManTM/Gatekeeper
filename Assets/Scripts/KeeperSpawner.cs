using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _keepers;
    [SerializeField] GameObject _keeperPrefab;

    private void Update()
    {
    }

    public void SpawnKeeper(Vector2 spawnPos)
    {
        foreach (var keeper in _keepers)
        {
            if (!keeper.active)
            {
                keeper.SetActive(true);
                keeper.transform.position = spawnPos;
                return;
            }
        }
    }
    public void HideAll()
    {
        foreach (var keeper in _keepers)
        {
            keeper.SetActive(false);
        }
    }
}
