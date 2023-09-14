using UnityEngine;
using System.Collections;

/// <summary>
/// Returns the AND result from testing several conditions
/// </summary>
public class ConditionAND : ICondition
{
	/// <summary>
	/// The conditions tested.
	/// </summary>
	public ICondition[] conditions = new ICondition[0];

	
	public override bool Test ()
	{
		for (int i = 0; i < conditions.Length; ++i)
		{
			if (!conditions[i].Test())	return false;
		}
		
		return true;
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
