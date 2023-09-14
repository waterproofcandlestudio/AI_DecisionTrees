using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all the nodes of a decision tree
/// </summary>
public class DecisionTreeNode : MonoBehaviour
{	
	/// <summary>
	/// Decide upon the information relevant to this instance.
	/// </summary>
	/// <returns>
	/// The action to take, or null if it doesn't decide
	/// (i.e: should keep doing the same thing as previously)
	/// </returns>
	public virtual IAction Decide ()
	{
		return null;
	}
}
