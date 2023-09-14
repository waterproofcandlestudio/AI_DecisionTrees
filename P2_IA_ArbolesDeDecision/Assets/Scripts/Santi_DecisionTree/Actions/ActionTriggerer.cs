using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;


/// <summary>
/// Triggers a list of actions when "Act" is called
/// </summary>
public class ActionTriggerer : IAction
{
	/// <summary>
	/// The action, that will Act upon NO agent
	/// </summary>
	public IAction[] actions = new IAction[0];
	
	/// <summary>
	/// Easy-to-attach method calls via inspector. Use them when the trigger will not be called
	/// so frequently
	/// </summary>
	public UnityEvent actionEvents;
	
	
	public override void Act ()
	{
		for (int i = 0; i < actions.Length; ++i)
		{
			actions[i].Act();
		}
		
		actionEvents.Invoke();
	}
	
}
