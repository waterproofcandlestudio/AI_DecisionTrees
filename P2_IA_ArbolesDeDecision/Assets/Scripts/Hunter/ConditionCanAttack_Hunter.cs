using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionCanAttack_Hunter : ICondition
{
    TargetDetector_Hunter targetDetector;

    [SerializeField] GameObject attackableTarget;

    void Awake()
    {
        targetDetector = GetComponentInParent<TargetDetector_Hunter>();
    }

    public override bool Test()
    {
        attackableTarget = targetDetector.attackableTarget;

        if (attackableTarget == null)
            return false;

        else
            return false;
    }
}
