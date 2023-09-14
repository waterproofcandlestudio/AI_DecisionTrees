using UnityEngine;
using System.Collections;

/// <summary>
/// Makes a decision each frame to know what to do next, requesting the decision to
/// a decision tree. Once it gets the action from the decision, it executes it.
/// </summary>
public class DecisionTreeAgent : MonoBehaviour
{
	/// <summary>
	/// The decision tree root
	/// </summary>
	public DecisionTreeNode rootNode;

	
	// Update is called once per frame
	void Update ()
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
