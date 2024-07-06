using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker : MonoBehaviour
{
    public GameObject Gates;
    public bool DirectionLeft;
    float _randomXSpawnPosition;
    Transform[] _bounds;

    void Start()
    {
        _bounds = Gates.GetComponentsInChildren<Transform>();
        if (DirectionLeft)
        {
            _randomXSpawnPosition = Random.Range(_bounds[1].position.x, 0f);
        }
        else
        {
            _randomXSpawnPosition = Random.Range(0f, _bounds[2].position.x);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


        Vector2 strikePos = new Vector2(_randomXSpawnPosition, _bounds[0].position.y);

        Gizmos.DrawLine(transform.position, strikePos);
        Gizmos.DrawSphere(_bounds[1].position, .1f);
        Gizmos.DrawSphere(_bounds[2].position, .1f);
    }
}
