using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime = 3;

    private void Awake()
    {
        StartCoroutine(DestroyDelay());
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
