using UnityEngine;
using System.Collections;

/// <summary>
/// Interface for conditions in the decision system.
/// </summary>
public class ICondition : MonoBehaviour
{
	/// <summary>
	/// Test the condition that represent that instance, returning
	/// true or false depending on the result
	/// </summary>
	public virtual bool Test ()
	{
		return false;
	}
	
	
	public virtual void InitializeCondition ()
	{
		return;
	}
	
	public virtual void FinalizeCondition ()
	{
		return;
	}

}
