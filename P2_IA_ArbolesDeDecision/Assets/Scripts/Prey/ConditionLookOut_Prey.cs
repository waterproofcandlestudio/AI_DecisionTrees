using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionLookOut_Prey : ICondition
{
    TargetDetector_Prey targetDetector;

    [SerializeField] GameObject target;

    void Awake()
    {
        targetDetector = GetComponentInParent<TargetDetector_Prey>();
    }

    public override bool Test()
    {
        target = targetDetector.watchingTarget;

        if (target != null)
            return true;

        else
            return false;
    }
}
