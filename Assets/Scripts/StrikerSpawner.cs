using System.Collections.Generic;
using UnityEngine;

public class StrikerSpawner : MonoBehaviour
{
    [SerializeField] GameObject _gates;
    [SerializeField] GameObject _strikerPrefab;

    List<GameObject> _strikerList = new List<GameObject>();
    void Start()
    {
        Vector2 targetPos = Vector2.zero;
        Vector2 spawnPos = Vector2.zero;
        GameObject striker;
        for (int i = 0; i < 2; i++)
        { 
            if(i == 0)
            {
                spawnPos = new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(-5f, -2f));
                striker = Instantiate(_strikerPrefab, spawnPos, Quaternion.identity);

                striker.GetComponent<Striker>().DirectionLeft = !CheckIsLeftSpawn(spawnPos);
            }
            else
            {
                if (CheckIsLeftSpawn(spawnPos))
                {
                    spawnPos.x = Random.Range(spawnPos.x + .5f, spawnPos.x + 1.5f);
                }
                else
                {
                    spawnPos.x = Random.Range(spawnPos.x - .5f, spawnPos.x - 1.5f);
                }
                spawnPos.y = Random.Range(-5f, -2f);

                striker = Instantiate(_strikerPrefab, spawnPos, Quaternion.identity);
                striker.GetComponent<Striker>().DirectionLeft = !_strikerList[0].GetComponent<Striker>().DirectionLeft;

            }
            _strikerList.Add(striker);
            _strikerList[i].GetComponent<Striker>().Gates = _gates;
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
