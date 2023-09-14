using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Objects that represent a state in a state machine.
/// The state is responsible for checking every transition
/// ordered by their priority every Update cycle, if enabled.
/// </summary>
public class State : MonoBehaviour
{
	/// <summary>
	/// The parent state, if it is a hierarchical FSM. If there is a parent,
	/// the execution of this state is governed by the parent
	/// </summary>
	[HideInInspector] public HierarchicalState parentState;

	[System.Serializable]
	public struct PriorityToTransition
	{
		public float priority;
		public StateTransition transition;
	}

	/// <summary>
	/// Transitions that emerge from this state to others. They
	/// are tested in the same order as they appear in this array.
	/// </summary>
	public PriorityToTransition[] transitions = new PriorityToTransition[0];
	
	/// <summary>
	/// Action taken on the OnEnable method.
	/// </summary>
	public IAction[] startActions = new IAction[0];
	
	/// <summary>
	/// The action taken on every update when this script is enabled
	/// </summary>
	public IAction[] actions = new IAction[0];
	
	/// <summary>
	/// Action taken on OnDisable method.
	/// </summary>
	public IAction[] endActions = new IAction[0];


	protected virtual void Awake ()
	{
		// Sort transitions by priority
		List<PriorityToTransition> transitionsList = new List<PriorityToTransition>(transitions);

		// Sort transitions by probability
		transitionsList.Sort(delegate(PriorityToTransition a, PriorityToTransition b) 
		                       {
			float difference = a.priority - b.priority;
			if (difference == 0)	return 0;
			
			return (difference < 0 ? 1 : -1);
		});

		transitions = transitionsList.ToArray();
	}

	
	/// <summary>
	/// Disables all the transitions if this state is not active
	/// </summary>
	protected virtual void Start ()
	{
		if (parentState != null && parentState.initialSubstate != this)
		{
			DisableState();
		}
		else
		{
			EnableState();
		}
	}
	
	/// <summary>
	/// Triggers the startActions, enables the transitions and initializes the actions,
	/// and notifies the parent state
	/// </summary>
	//protected virtual void OnEnable ()
	public virtual void EnableState ()
	{
		enabled = true;

		for (int i = 0; i < startActions.Length; ++i)
		{
			IAction action = startActions[i];
			action.InitializeAction();
			action.Act();
		}

		for (int i = 0; i < transitions.Length; ++i)
		{
			//transitions[i].transition.enabled = true;
			transitions[i].transition.EnableTransition();
		}

		// Initialize the cycle actions
		for (int i = 0; i < actions.Length; ++i)
		{
			actions[i].InitializeAction();
		}
		
		// If this state becomes active, change parent's lastActiveSubstate
		if (parentState != null)
		{
			parentState.OnSubstateEnabled(this);
		}
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		// If there is a parent state, then the execution of this state
		// depends on the execution of the parent's execute cycle
		if (parentState == null)
		{
			ExecuteCycle();
		}
	}
	
	public virtual void ExecuteCycle ()
	{
		// First, check if the transitions are triggered
		for (int i = 0; i < transitions.Length; ++i)
		{
			if (transitions[i].transition.IsTriggered(this))
			{
				return;
			}
		}
		
		// If no transition was triggered, perform the action
		for (int i = 0; i < actions.Length; ++i)
		{
			actions[i].Act();
		}
	}
	
	
	/// <summary>
	/// Finalizes the actions, triggers the endActions, disables the
	/// transitions and notifies the parent state (if any)
	/// </summary>
	//protected virtual void OnDisable ()
	public virtual void DisableState ()
	{
		enabled = false;
		
		for (int i = 0; i < startActions.Length; ++i)
		{
			startActions[i].FinalizeAction();
		}

		for (int i = 0; i < actions.Length; ++i)
		{
			actions[i].FinalizeAction();
		}

		for (int i = 0; i < endActions.Length; ++i)
		{
			IAction action = endActions[i];
			action.InitializeAction();
			action.Act();
			action.FinalizeAction();
		}
		
		for (int i = 0; i < transitions.Length; ++i)
		{
			transitions[i].transition.DisableTransition();
		}
		
		// If this state becomes deactivated, change parent's lastActiveSubstate
		if (parentState != null)
		{
			parentState.OnSubstateDisabled(this);
		}
	}
	
}
