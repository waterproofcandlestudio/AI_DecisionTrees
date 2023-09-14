using UnityEngine;
using System.Collections;

/// <summary>
/// "Negates" the "condition" result when calling to Test
/// </summary>
public class ConditionNOT : ICondition
{
	public ICondition condition;
	
	public override bool Test ()
	{
		return !condition.Test();
	}
	
	public override void InitializeCondition ()
	{
		condition.InitializeCondition();
	}
	
	public override void FinalizeCondition ()
	{
		condition.FinalizeCondition();
	}
}
