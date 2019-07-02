using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : SkelMState {
	
	private readonly SkelMStateMachine AI;
	// Use this for initialization

	public AttackState (SkelMStateMachine skelMStateMachine)
	{
		AI = skelMStateMachine;
	}

	public void UpdateState()
	{
		

	}


	public void OnTriggerEnter (Collider other)
	{
		Debug.Log (other.gameObject.name + " Has entered the trigger");
	}

	public void OnTriggerStay (Collider other)
	{
		Debug.Log (other.gameObject.name + " is in the trigger");
	}

	public void OnTriggerExit (Collider other)
	{
		Debug.Log (other.gameObject.name + " Has exited the trigger");
	}

	public void ToAttackingState()
	{
		Debug.Log ("Can't transition to same state");
	}

	public void ToNeutralState()
	{
		AI.currentState = AI.neutralState;

	}

	public void ToAggresiveState()
	{
		AI.currentState = AI.aggresiveState;

	}
		
}
