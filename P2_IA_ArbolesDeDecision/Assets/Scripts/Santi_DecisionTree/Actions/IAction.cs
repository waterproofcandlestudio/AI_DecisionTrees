using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for (very) simple actions for the decision systems
/// </summary>
public class IAction : MonoBehaviour
{
	/// <summary>
	/// Called to make the action do its stuff. This depends on the
	/// type of action, and the default implementation does
	/// nothing.
	/// </summary>
	public virtual void Act ()
	{
	    // Overridden by subclasses
	    return;
	}


	/// <summary>
	/// Actions performed when the action is going to start.
	/// </summary>
	public virtual void InitializeAction ()
	{
		return;
	}
	
	
	/// <summary>
	/// Actions performed when the action will stop executing.
	/// </summary>
	public virtual void FinalizeAction ()
	{
		return;
	}

}
