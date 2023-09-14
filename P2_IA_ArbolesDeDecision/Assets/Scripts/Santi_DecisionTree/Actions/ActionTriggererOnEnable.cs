using UnityEngine;
using System.Collections;

/// <summary>
/// Calls "Act" method in the OnEnable/OnDisable methods
/// </summary>
public class ActionTriggererOnEnable : ActionTriggerer
{
	public bool playOnDisable = false;


	void OnEnable ()
	{
		if (!playOnDisable)
		{
			Act();
		}
	}
	
	
	void OnDisable ()
	{
		if (playOnDisable)
		{
			Act();
		}
	}
	
}
