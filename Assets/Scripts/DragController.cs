using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public bool _isDraging = false;
    public Vector2 _startTouch, _mousePosition;

    KeeperSpawner _spawner;

    private void Start()
    {
        _spawner = GetComponent<KeeperSpawner>();
    }
    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GetDrag();

        if (_isDraging)
        {
            _spawner.SpawnKeepersOnDrag(_startTouch, _mousePosition);
        }

    }

    void GetDrag()
    {
        #region PC version
        if (Input.GetMouseButtonDown(0))
        {
            _isDraging = true;
            _startTouch = _mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDraging = false;
            _spawner.SetKeepers();
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
                _spawner.SetKeepers();
            }
        }
        #endregion
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_startTouch, _mousePosition);
    }
}
