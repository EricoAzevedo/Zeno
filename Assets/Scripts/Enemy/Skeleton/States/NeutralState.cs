using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NeutralState : SkelState {
	private readonly SkelStateMachine AI;
	float idleTimer;

	Vector3 searchPoint;
	bool alert;

	public NeutralState (SkelStateMachine skelStateMachine)
	{
		AI = skelStateMachine;
	}

	public void UpdateState()
	{
		if (alert == false) {
			Patrol (AI.mother.transform.position);
		} else {
			Alert ();
		}
		Timers ();
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player" && AI.dead != true) {
			AI.navMeshAgent.Stop ();
			alert = true;
		} 
	}

	public void OnTriggerStay (Collider other)
	{

	}

	public void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player" && AI.dead != true) {
			alert = false;
			AI.navMeshAgent.Stop ();
		} 	
	}

	public void ToAttackState()
	{
		AI.currentState = AI.attackingState;
		searchPoint = Vector3.zero;
		AI.navMeshAgent.Resume ();
		alert = false;
		AI.pointReached = false;
		AI.anim.SetBool ("Walking", false);
		AI.combatManager.GetComponent<CombatManager> ().AddToList (AI.gameObject);

	}
		

	public void ToNeutralState()
	{
		Debug.Log ("State trying to switch to itself");

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
