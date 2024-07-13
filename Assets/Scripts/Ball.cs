using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D _rb;
    Vector2 _strikePos;
    [SerializeField] float _speed;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(IMove());

    }

    public void Init(Vector2 strikePos)
    {
        _strikePos = strikePos;
    }

    private void FixedUpdate()
    {
    }
    IEnumerator IMove()
    {
        _rb.AddForce((_strikePos - (Vector2)transform.position).normalized * _speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        yield return new WaitForFixedUpdate();
        StartCoroutine(IMove());
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Keeper":
                StopAllCoroutines();
                break;
            case "Bounds":
                _rb.velocity = Vector2.zero;
                break;

        }
        print(collision.gameObject.tag);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, _strikePos);

    }
}
