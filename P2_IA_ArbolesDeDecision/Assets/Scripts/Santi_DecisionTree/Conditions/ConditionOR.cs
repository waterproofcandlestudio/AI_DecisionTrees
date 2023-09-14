using UnityEngine;
using System.Collections;

/// <summary>
/// Returns the OR result from testing several conditions
/// </summary>
public class ConditionOR : ICondition
{
	/// <summary>
	/// The conditions tested.
	/// </summary>
	public ICondition[] conditions = new ICondition[0];

	
	public override bool Test ()
	{
		for (int i = 0; i < conditions.Length; ++i)
		{
			if (conditions[i].Test())	return true;
		}

		return false;
	}
	
	public override void InitializeCondition ()
	{
		for (int i = 0; i < conditions.Length; ++i)
		{
			conditions[i].InitializeCondition();
		}
	}
	
	public override void FinalizeCondition ()
	{
		for (int i = 0; i < conditions.Length; ++i)
		{
			conditions[i].FinalizeCondition();
		}
	}
	
}
