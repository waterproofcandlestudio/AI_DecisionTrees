using UnityEngine;
using System.Collections;

/// <summary>
/// Performs a decision like a DecisionTreeAgent, and calls the Act method
/// of its output
/// </summary>
public class ActionDecideAndAct : IAction
{
	/// <summary>
	/// The root node to decide with
	/// </summary> 
	public DecisionTreeNode rootNode;

	
	public override void Act ()
	{
		if (rootNode == null)
		{
			return;
		}
		
		IAction action = rootNode.Decide();

		if (action != null)
		{
			action.Act();
		}
	}
}
