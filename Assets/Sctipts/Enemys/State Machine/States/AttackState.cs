using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyRenderer))]
public class AttackState : State
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _delay;
    
    private Animator _animator;
    private float _lastAttackTime;
    private EnemyRenderer _enemyRenderer;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyRenderer = GetComponent<EnemyRenderer>();
        _lastAttackTime = 0;
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, Target.transform.position); 
        
        if (_lastAttackTime <= 0 && distance <= _attackRange)
        {
            Attack(Target);
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Player target)
    {
        _enemyRenderer.RotateToTarget(Target.transform);
        _animator.Play(AttackHash);
        target.TakeDamage(_damage);
    }
}
