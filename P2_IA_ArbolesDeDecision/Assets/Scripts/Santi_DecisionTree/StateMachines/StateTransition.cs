using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

/// <summary>
/// Transition that leads from one state from another if the condition
/// associated is triggered. It may optionally have an action that is
/// executed when the transition is triggered.
/// </summary>
public class StateTransition : MonoBehaviour
{
	/// <summary>
	/// The condition that triggers the transition
	/// </summary>
	public ICondition condition;
	
	/// <summary>
	/// The action taken when the transition is fired.
	/// </summary>
	public IAction[] actions = new IAction[0];
	
	/// <summary>
	/// The state this transition leads to.
	/// </summary>
	public State targetState;
	
#if UNITY_EDITOR
	/// <summary>
	/// The name of the trigger associated with this transition in the AnimatorController
	/// "animatorToTrigger" (if any).
	/// </summary>
	[HideInInspector] public string triggerName;
#endif
	
	[HideInInspector] public int hashedTriggerName;

	/// <summary>
	/// If this is not null, a trigger may be specified to allow this AnimatorController
	/// to change its state whenever the transition is triggered.
	/// </summary>
	[HideInInspector] public Animator animatorToTrigger;
	
	
	//void OnEnable ()
	public virtual void EnableTransition ()
	{
		enabled = true;

		if (condition != null)
		{
			condition.InitializeCondition();
		}

		for (int i = 0; i < actions.Length; ++i)
		{
			actions[i].InitializeAction();
		}
	}
	
	//void OnDisable ()
	public virtual void DisableTransition ()
	{
		enabled = false;

		if (condition != null)
		{
			condition.FinalizeCondition();
		}
		
		
		for (int i = 0; i < actions.Length; ++i)
		{
			actions[i].FinalizeAction();
		}
	}

	
	/// <summary>
	/// Determines whether this transition is triggered, moving to a new state.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance is triggered; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='state'>
	/// State that is testing the transition. It will be disabled .
	/// </param>
	public bool IsTriggered (State state)
	{
		if (condition != null && condition.Test())
		{
			// Disable previous state
			if (state != null)
			{
				//state.enabled = false;
				state.DisableState();
				
				// Take the actions of the transition
				for (int i = 0; i < actions.Length; ++i)
				{
					IAction action = actions[i];
					action.InitializeAction();
					action.Act();
					action.FinalizeAction();
				}

				// Trigger the animator state
				if (animatorToTrigger != null)
				{
					animatorToTrigger.SetTrigger(hashedTriggerName);
				}
			}
			
			// Enable target state
			if (targetState != null)
			{
				//targetState.enabled = true;
				targetState.EnableState();
			}
			else
			{
				Debug.LogError("[" + gameObject.name + "] StateTransition null targetState");
			}
			
			return true;
		}
		
		return false;
	}

}



#if UNITY_EDITOR
[CustomEditor(typeof(StateTransition))]
public class StateTransitionEditor : Editor
{
	
	public override void OnInspectorGUI ()
	{
		StateTransition transition = target as StateTransition;
		if (transition == null)
		{
			return;
		}

		base.OnInspectorGUI();

		transition.animatorToTrigger = EditorGUILayout.ObjectField("Animator", transition.animatorToTrigger, typeof(Animator), true) as Animator;

		if (transition.animatorToTrigger != null)
		{
			transition.triggerName = EditorGUILayout.TextField("Trigger name", transition.triggerName);
			transition.hashedTriggerName = Animator.StringToHash(transition.triggerName);
		}
	}
	
}
#endif
