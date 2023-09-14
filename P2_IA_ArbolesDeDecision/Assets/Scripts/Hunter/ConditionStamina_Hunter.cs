// Miguel Rodríguez Gallego
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionStamina_Hunter : ICondition
{
    EntityStats stats;

    void Awake()
    {
        stats = GetComponentInParent<EntityStats>();
    }

    public override bool Test()
    {
        if (stats.GetCanRegenStamina())
            return false;

        else
            return true;
    }
}
