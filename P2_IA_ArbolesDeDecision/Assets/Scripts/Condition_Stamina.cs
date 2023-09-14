using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Condition_Stamina : ICondition
{
    public Transform target;

    EntityStats stats;

    void Awake()
    {
        stats = GetComponentInParent<EntityStats>();
    }

    public override bool Test()
    {
        if (target == null) return false;

        if (stats.GetStamina() <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
