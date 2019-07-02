using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggresiveState : SkelMState {

	private readonly SkelMStateMachine AI;
	// Use this for initialization

	public AggresiveState (SkelMStateMachine skelMStateMachine)
	{
		AI = skelMStateMachine;
	}

	public void UpdateState()
	{

		ReadyingToAttack ();
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

	void ReadyingToAttack(){
		if (AI.navMeshAgent.enabled) {
			AI.navMeshAgent.destination = AttackPos(AI.angle * Mathf.Deg2Rad);
			AI.navMeshAgent.Resume ();
			AI.navMeshAgent.updateRotation = false;
			AI.anim.SetBool ("Running", true);
			AI.anim.SetBool ("Idle", true);

			if (AI.navMeshAgent.remainingDistance <= AI.navMeshAgent.stoppingDistance  && !AI.navMeshAgent.pathPending) {
				AI.anim.SetBool ("Running", false);
				AI.anim.SetBool ("Idle", true);
				AI.navMeshAgent.Stop ();
				AI.pointReached = true;
			}
		}


		Quaternion targetRotation = Quaternion.LookRotation (AI.player.transform.position - AI.transform.position);
		float str = Mathf.Min (AI.turnSpeed * Time.deltaTime, 1);
		AI.transform.rotation = Quaternion.Lerp (AI.transform.rotation, targetRotation, str);	
	}

	public Vector3 AttackPos(float angle){
		Vector3 attackPos;
		float z = AI.distanceFromTarget * Mathf.Cos(angle) + AI.player.transform.position.z;
		float x = AI.distanceFromTarget * Mathf.Sin(angle) + AI.player.transform.position.x;
		return attackPos = new Vector3 (x, AI.transform.position.y, z);

	}
}