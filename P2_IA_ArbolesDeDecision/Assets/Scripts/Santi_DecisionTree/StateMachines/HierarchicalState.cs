using UnityEngine;
using System.Collections;

/// <summary>
/// State that represents a hierarchical state with its substates
/// and transitions to other states
/// </summary>
public class HierarchicalState : State
{
	/// <summary>
	/// Substates of this state, that will be executed if this
	/// is active and the state is active
	/// </summary>
	public State[] substates;
	
	/// <summary>
	/// The initial substate. If none specified, it will be the first of the
	/// substates
	/// </summary>
	public State initialSubstate;
	
	/// <summary>
	/// The last active substate, changed by the substates when they become
	/// enabled/disabled
	/// </summary>
	public State lastActiveSubstate;
	
	
	protected override void Awake ()
	{
		base.Awake();

		Configure(null);
	}
	
	protected void Configure (HierarchicalState parentState_)
	{
		if (parentState != null && parentState == parentState_)		return;
		parentState = parentState_;
		
		// Set the initial substate, if there is no one assigned
		if (initialSubstate == null && substates.Length > 0)
		{
			initialSubstate = substates[0];
		}


		for (int i = 0; i < substates.Length; ++i)
		{
			State state = substates[i];
			state.parentState = this;

			HierarchicalState hierarchicalState = state as HierarchicalState;
			if (hierarchicalState != null)
			{
				hierarchicalState.Configure(this);
			}
		}
	}

	protected override void Start ()
	{
		base.Start();

		if (initialSubstate != null)
		{
			if (enabled)		initialSubstate.EnableState();
			else				initialSubstate.DisableState();
		}
	}

	public override void EnableState ()
	{
		base.EnableState();

		if (lastActiveSubstate != null)
		{
			lastActiveSubstate.EnableState();
		}
	}
	
	public void OnSubstateEnabled (State substate)
	{
		if (substate != null && lastActiveSubstate != substate)
		{
			lastActiveSubstate = substate;
			EnableState();
		}
		
		enabled = true;
	}
	
	public void OnSubstateDisabled (State substate)
	{
		if (substate != null && substate == lastActiveSubstate)
		{
			if (enabled)
			{
				DisableState();
			}
		}
	}
	
	public override void DisableState ()
	{
		base.DisableState();
		
		for (int i = 0; i < substates.Length; ++i)
		{
			substates[i].DisableState();
		}
	}
	
	
	public override void ExecuteCycle ()
	{
		// First, check if the transitions are triggered
		for (int i = 0; i < transitions.Length; ++i)
		{
			if (transitions[i].transition.IsTriggered(this))
			{
				// If a transition is triggered, then there is no need to keep
				// checking transitions or doing actions
				return;
			}
		}
		
		// If no transition was triggered, execute the cycle of the current substate
		if (lastActiveSubstate != null)
		{
			lastActiveSubstate.ExecuteCycle();
		}
	}

}
