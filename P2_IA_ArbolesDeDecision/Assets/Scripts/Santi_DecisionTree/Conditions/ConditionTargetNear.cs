using UnityEngine;
using System.Collections;

/// <summary>
/// Checks if this transform is within "range" distance from "target"
/// </summary>
public class ConditionTargetNear : ICondition
{
	/// <summary>
	/// Target to detect
	/// </summary>
	public Transform target;
	
	/// <summary>
	/// The maximun range a target can be detected
	/// </summary>
	public float range = 5.0f;
	
	
	public override bool Test ()
	{
		if (target == null)		return false;

		return (transform.position - target.transform.position).magnitude <= range;
	}

}
