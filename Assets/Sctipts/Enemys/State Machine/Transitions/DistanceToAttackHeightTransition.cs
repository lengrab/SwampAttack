using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DistanceToAttackHeightTransition : Transition
{
    [SerializeField] private float _transitionRange = 1;
    [SerializeField] private float _rangeSpread = 1;

    private void Start()
    {
        _transitionRange += Random.Range(- _rangeSpread, _rangeSpread);
    }

    private void Update()
    {
        if (Target == null)
        {
            return;
        }
        
        if (Vector2.Distance(transform.position, Target.transform.position) > _transitionRange)
        {
            NeedTransit = true;
        }
    }
}