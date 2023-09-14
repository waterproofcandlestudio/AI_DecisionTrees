using UnityEngine;
using System.Collections;


/// <summary>
/// Triggers an action when a condition is met. It allows for simple
/// condition-action pairs, for generic purposes.
/// </summary>
public class ActionTriggererOnCondition : ActionTriggerer
{
	/// <summary>
	/// Condition that will trigger the actions.
	/// </summary>
	public ICondition condition;


	public override void InitializeAction ()
	{
		enabled = true;
	}
	
	public override void FinalizeAction ()
	{
		enabled = false;
	}
	
	
	// Use this for initialization
	void OnEnable ()
	{
		if (condition != null)
		{
			condition.InitializeCondition();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (condition.Test())
		{
			ForceAct();
		}
	}

	/// <summary>
	/// The "Act" method can only be called when the conditions are met.
	/// </summary>
	public override void Act ()
	{
		return;
	}

	/// <summary>
	/// This is called when the conditions are met.
	/// </summary>
	protected void ForceAct ()
	{
		base.Act();
	}
	
	void OnDisable ()
	{
		if (condition != null)
		{
			condition.FinalizeCondition();
		}
	}
	
}
