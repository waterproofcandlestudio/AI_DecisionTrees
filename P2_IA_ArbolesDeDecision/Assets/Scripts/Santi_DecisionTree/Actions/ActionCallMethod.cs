using UnityEngine;
using System.Collections;

/// <summary>
/// Calls a method on the selected GameObject, with an optional parameter.
/// </summary>
public class ActionCallMethod : IAction
{
	/// <summary>
	/// GameObject to call the method on.
	/// </summary>
	public GameObject eventReceiver;

	/// <summary>
	/// Method to call (by exact name).
	/// </summary>
	public string message;

	/// <summary>
	/// Optional parameter to pass to the object.
	/// </summary>
	public GameObject objectParameter;

	
	public override void Act ()
	{
		GameObject target = eventReceiver;
		if (eventReceiver == null)
		{
			target = gameObject;
		}
		
		if (objectParameter != null)
		{
			target.SendMessage(message, objectParameter, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			target.SendMessage(message, SendMessageOptions.DontRequireReceiver);
		}
	}
}
