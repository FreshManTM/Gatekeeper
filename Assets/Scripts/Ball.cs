using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Ball : MonoBehaviour
{
    Rigidbody2D _rb;
    Vector2 _strikePos;
    [SerializeField] float _speed;

    LevelManager _lvlManager;
    bool collided;
    public void Init(Vector2 strikePos)
    {
        _rb = GetComponent<Rigidbody2D>();
        _lvlManager = LevelManager.Instance;
        _strikePos = strikePos;
        collided = false;

        StartCoroutine(IMove());
    }

    IEnumerator IMove()
    {
        _rb.AddForce((_strikePos - (Vector2)transform.position).normalized * _speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        yield return new WaitForFixedUpdate();
        StartCoroutine(IMove());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collided)
        {
            switch (collision.gameObject.tag)
            {
                case "Keeper":
                    collided = true;
                    StopAllCoroutines();
                    _lvlManager.BallSave();
                    Invoke("Despawn", 2f);
                    break;
                case "Bounds":
                    collided = true;
                    StopAllCoroutines();
                    _rb.velocity = Vector2.zero;
                    transform.position = collision.contacts[0].point;
                    Invoke("Despawn", 2f);
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gates")
        {
            StopAllCoroutines();
            _lvlManager.BallMiss();
            _rb.velocity = _rb.velocity / 2;
        }
    }
    void Despawn()
    {
        ObjectPool.Instance.Despawn(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, _strikePos);

    }
}
