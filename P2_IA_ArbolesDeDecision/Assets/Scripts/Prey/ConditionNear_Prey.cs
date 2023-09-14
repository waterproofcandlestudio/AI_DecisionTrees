using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNear_Prey : ICondition
{
    TargetDetector_Prey targetDetector;

    [SerializeField] GameObject target;

    void Awake()
    {
        targetDetector = GetComponentInParent<TargetDetector_Prey>();
    }

    public override bool Test()
    {
        target = targetDetector.target;

        if (target != null)
            return true;

        else
            return false;
    }
}
