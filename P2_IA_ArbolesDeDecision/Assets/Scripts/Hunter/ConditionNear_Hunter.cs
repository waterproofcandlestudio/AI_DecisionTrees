using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNear_Hunter : ICondition
{
    TargetDetector_Hunter targetDetector;

    [SerializeField] GameObject target;

    void Awake()
    {
        targetDetector = GetComponentInParent<TargetDetector_Hunter>();
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
