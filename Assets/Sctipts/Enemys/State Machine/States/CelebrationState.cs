using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CelebrationState : State
{
    private static readonly int CelebrationHash = Animator.StringToHash("Celebration");
    
    private Animator _animator;
    private EnemyStateMachine _enemyStateMachine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
    }

    private void OnEnable()
    {
        _animator.Play(CelebrationHash);
        
        if (_enemyStateMachine)
        {
            _enemyStateMachine.enabled = false;
            DisableStates();
        }
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }

    private void DisableStates()
    {
        State[] _states = GetComponents<State>();

        foreach (State state in _states)
        {
            state.enabled = false;
        }
    }
}
