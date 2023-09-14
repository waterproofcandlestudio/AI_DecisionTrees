using UnityEngine;
using System.Collections;

/// <summary>
/// Condition to check if X seconds have passed since OnInitializeCondition.
/// </summary>
public class ConditionWaitForSeconds : ICondition
{
	public float secondsToWait = 1.0f;
	protected float secondsSinceLastTime = 0.0f;
	
	/// <summary>
	/// True to reset the internal timer to 0 when OnInitializeCondition is called,
	/// false to only reset to 0 when secondsToWait have passed. If set to false, it
	/// only return "true" for the first call to "Test" after secondsToWait have passed
	/// </summary>
	public bool shouldRestartOnInitialize = true;
	
	
	public override bool Test ()
	{
		if (secondsSinceLastTime >= secondsToWait)
		{
			if (!shouldRestartOnInitialize)
			{
				secondsSinceLastTime = 0.0f;
			}
			
			return true;
		}
		
		return false;
	}
	
	
	void Update ()
	{
		secondsSinceLastTime += Time.deltaTime;
	}
	
	
	public override void InitializeCondition ()
	{
		enabled = true;

		if (shouldRestartOnInitialize)
		{
			secondsSinceLastTime = 0.0f;
		}
	}

	public override void FinalizeCondition ()
	{
		enabled = false;
	}
	
}
