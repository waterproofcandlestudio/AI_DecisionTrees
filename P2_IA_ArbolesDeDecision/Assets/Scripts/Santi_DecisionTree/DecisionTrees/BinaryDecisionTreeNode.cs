using UnityEngine;
using System.Collections;

/// <summary>
/// Node that makes a decision with only two possible resolutions
/// </summary>
public class BinaryDecisionTreeNode : DecisionTreeNode
{
	/// <summary>
	/// The condition to test
	/// </summary>
	public ICondition condition;
	
	/// <summary>
	/// The options available as output
	/// </summary>
	public DecisionTreeNode trueNode;
	public DecisionTreeNode falseNode;
	
	
	public override IAction Decide ()
	{
		if (condition.Test())
		{
			if (trueNode != null)
			{
				return trueNode.Decide();
			}
		}
		else if (falseNode != null)
		{
			return falseNode.Decide();
		}
		
		return null;
	}
}
