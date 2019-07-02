using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


	public class AttackingState : SkelState {
	public bool attackPos;
	public float angleTimer;
	private readonly SkelStateMachine AI;
	// Use this for initialization

	public AttackingState (SkelStateMachine skelStateMachine)
	{
		AI = skelStateMachine;
	}

	public void UpdateState()
	{
		if (AI.attack == false && AI.dead == false) {
			ReadyingToAttack ();

		} else if (AI.attack == true && AI.dead == false) {
			AttackPlayer ();
		} else {
			
		}
		Timers ();

	}
		

	public void OnTriggerEnter (Collider other)
	{
	}

	public void OnTriggerStay (Collider other)
	{
	}

	public void OnTriggerExit (Collider other)
	{
	}

	public void ToAttackState()
	{
	}

	public void ToNeutralState()
	{
		AI.currentState = AI.neutralState;

	}

	void ReadyingToAttack(){
		MovePoint (AttackPos (AI.tempAngle * Mathf.Deg2Rad));
		AI.anim.SetBool ("Attack", false);		
		if (AI.navMeshAgent.enabled) {
			if (AI.movePoint.GetComponent<MovePointEnemy> ().reachable && AI.movePoint.GetComponent<MovePointEnemy> ().canSeeTarget) {
				AI.navMeshAgent.destination = AttackPos (AI.tempAngle * Mathf.Deg2Rad);
				AI.navMeshAgent.Resume ();
				AI.navMeshAgent.updateRotation = false;
				AI.anim.SetBool ("Running", true);
				AI.anim.SetBool ("Moving", true);
				AI.retreating = false;
			} else {
					attackPos = false;
					AI.tempAngle += 10;
				if (AI.movePoint != null) {
					AI.movePoint.GetComponent<MovePointEnemy> ().RefreshState ();
					}
				}		
			}


		Quaternion targetRotation = Quaternion.LookRotation (AI.player.transform.position - AI.transform.position);
		float str = Mathf.Min (AI.turnSpeed * Time.deltaTime, 1);
		AI.transform.rotation = Quaternion.Lerp (AI.transform.rotation, targetRotation, str);
		AI.movePoint.GetComponent<MovePointEnemy> ().RefreshState ();

	}

		


	void AttackPlayer(){
		if (AI.navMeshAgent.enabled) {
			AI.navMeshAgent.updateRotation = true;
			AI.navMeshAgent.destination = AI.player.transform.position;
			AI.navMeshAgent.Resume ();
		}
		if (AI.navMeshAgent.remainingDistance <= AI.navMeshAgent.stoppingDistance && !AI.navMeshAgent.pathPending) {
			AI.navMeshAgent.Stop ();
			AI.anim.SetBool ("Attack", true);
			AI.retreating = true;
		} else {
			AI.anim.SetBool ("Attack", false);		
		}

		
	}




	public Vector3 AttackPos(float angle){

		Vector3 attackPos;
		float x = AI.distanceFromTarget * Mathf.Cos(angle) + AI.player.transform.position.x;
		float z = AI.distanceFromTarget * Mathf.Sin(angle) + AI.player.transform.position.z;
		return attackPos = new Vector3 (x, AI.transform.position.y, z);
		
	}
		
	public void MovePoint(Vector3 Pos){

		if (AI.movePoint != null) {
			if (AI.movePoint.GetComponent<MovePointEnemy> ().transform.position == Pos) {
				AI.movePoint.GetComponent<MovePointEnemy> ().CanMoveToCheck ();
			} else {
				AI.movePoint.GetComponent<MovePointEnemy> ().transform.position = Pos;
				AI.movePoint.GetComponent<MovePointEnemy> ().enemyTransform = AI.transform;
				AI.movePoint.GetComponent<MovePointEnemy> ().Target = AI.player.transform;
				AI.movePoint.GetComponent<MovePointEnemy> ().parentAgent = AI.navMeshAgent;
				AI.movePoint.GetComponent<MovePointEnemy> ().CanMoveToCheck ();
			}
		} else {
			AI.movePoint = GameObject.Instantiate(Resources.Load("MovePointEnemy") as GameObject);
		}	
	}

	void Timers(){
		angleTimer += Time.deltaTime;
		if(angleTimer > 5f){
			AI.tempAngle = Random.Range (AI.tempAngle - 20f, AI.tempAngle + 20f);
			angleTimer = 0f;
			}
		}
}
