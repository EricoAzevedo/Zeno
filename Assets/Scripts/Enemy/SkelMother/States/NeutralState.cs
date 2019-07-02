using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralState1 : SkelMState {

	private readonly SkelMStateMachine AI;
	Vector3 searchPoint;
	float idleTimer;
	bool alert;



	public NeutralState1 (SkelMStateMachine skelMStateMachine)
	{
		AI = skelMStateMachine;
	}

	public void UpdateState()
	{

		Patrol (AI.StartPos);
		if (alert == false) {
		Patrol (AI.StartPos);		} else {
			Alert ();
		}
		Timers ();
	}


	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			AI.navMeshAgent.Stop ();
			alert = true;
		} 
	}

	public void OnTriggerStay (Collider other)
	{
		Debug.Log (other.gameObject.name + " is in the trigger");
	}

	public void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			alert = false;
			AI.navMeshAgent.Stop ();
		} 
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
		alert = false;
		AI.pointReached = false;
		AI.anim.SetBool ("Walking", false);

	}

	void Patrol(Vector3 patrolCenter){

		if (AI.navMeshAgent.destination != searchPoint) {
			searchPoint = AI.RandomNavSphere (patrolCenter, AI.distanceFromTarget, -1);
			AI.anim.SetBool ("Walking", true);
		}
		if (AI.navMeshAgent.enabled) {
			AI.navMeshAgent.destination = searchPoint;
			AI.pointReached = false;
			AI.navMeshAgent.Resume ();
			if (AI.navMeshAgent.remainingDistance <= AI.navMeshAgent.stoppingDistance  && !AI.navMeshAgent.pathPending) {
				AI.anim.SetBool ("Walking", false);
				AI.navMeshAgent.Stop ();
				AI.pointReached = true;
			}
		}
	}

	void Alert(){				
		if (AI.navMeshAgent.enabled) {
			if (AI.playerCanBeSeen == false) {
				AI.navMeshAgent.destination = AI.player.transform.position;
				AI.pointReached = false;
				AI.navMeshAgent.Resume ();
			} else {
				AI.navMeshAgent.Stop ();
				AI.navMeshAgent.enabled = false;
				Quaternion targetRotation = Quaternion.LookRotation (AI.player.transform.position - AI.transform.position);
				float str = Mathf.Min (AI.turnSpeed * Time.deltaTime, 1);
				AI.transform.rotation = Quaternion.Lerp (AI.transform.rotation, targetRotation, str);	
			}

		} else {
			AI.navMeshAgent.enabled = true;
		}

	}


	void Timers(){
		if (AI.pointReached == true) {
			idleTimer += Time.fixedDeltaTime;
			if(idleTimer > AI.idleTime){
				searchPoint = Vector3.zero;
				idleTimer = 0f;	
			}
		}

	}

}