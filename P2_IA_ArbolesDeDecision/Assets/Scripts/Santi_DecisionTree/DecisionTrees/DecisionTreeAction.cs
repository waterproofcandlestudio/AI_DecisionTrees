using UnityEngine;
using System.Collections;

/// <summary>
/// Node that represents a leaf: an action to be taken
/// </summary>
public class DecisionTreeAction : DecisionTreeNode
{
	public IAction action;
	
	public override IAction Decide ()
	{
		return action;
	}
}
