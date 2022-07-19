using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _reward = 10;

    [SerializeField] private Player _target;

    public event UnityAction<Enemy> Died ;
    public Player Target => _target;
    public int Reward => _reward;

    public void Init(Player target)
    {
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= Mathf.Clamp(damage, 0, int.MaxValue);

        if (_health <= 0)
        {
            Died.Invoke(this);
            Destroy(gameObject);
        }
    }
}
