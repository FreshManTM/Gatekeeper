using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public bool _isDraging = false;
    public Vector2 _startTouch, _endTouch, _mousePosition, _spawnOffset, _swipeDelta;

    KeeperSpawner _spawner;
    public int _keeperAmount;

    private void Start()
    {
        _spawner = GetComponent<KeeperSpawner>();
    }
    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        #region PC version
        if (Input.GetMouseButtonDown(0))
        {

            _spawner.HideAll();
            _spawnOffset = Vector2.zero;
            _keeperAmount = 0;

            _isDraging = true;
            _startTouch = _mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDraging = false;
            _endTouch = _mousePosition;
            while (GetDistance() > _keeperAmount * .3f)
            {
                _spawner.SpawnKeeper(_startTouch + _spawnOffset * (_endTouch - _startTouch).normalized);
                _spawnOffset += new Vector2(.3f, .3f);
                _keeperAmount++;
            }
            print(GetDistance() + " | " + _keeperAmount * .3f);

        }
        #endregion

        #region Mobile version
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _isDraging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _isDraging = false;
            }
        }
        #endregion
        

        if (_isDraging)
        {

        }

    }

    public float GetDistance()
    {
        return Vector2.Distance(_startTouch, _endTouch);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_startTouch, _mousePosition);
        //print(Vector2.Distance(_startTouch, _mousePosition));
    }
}
