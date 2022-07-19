using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveState : State
{
    private static readonly int WalkHash = Animator.StringToHash("Walk");
    
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private float distanceToJump = 2;

    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 directionToTarget = (Target.transform.position - transform.position);
        
        if (directionToTarget.y > distanceToJump)
        {
            _mover.Jump();
        }
        
        _mover.Move(directionToTarget);
    }

    private void OnEnable()
    {
        _animator.Play(WalkHash);
    }
}
