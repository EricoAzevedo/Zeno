using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkelStateMachine : MonoBehaviour {

	 public SkelState currentState;
	[HideInInspector] public AttackingState attackingState;
	[HideInInspector] public NeutralState neutralState;
	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public Animator anim;
	[HideInInspector] public GameObject movePoint;
	public float distanceFromTarget;
	public bool pointReached;
	public float idleTime;
	public GameObject mother;
	public GameObject player;
	public GameObject eyes;
	public bool playerInSight;
	public bool playerCanBeSeen;
	public float turnSpeed;
	public float tempAngle;
	public bool attack;
	public float damage;
	public bool dead;
	public GameObject combatManager;
	public bool retreating;


	private void Awake()
	{
		attackingState = new AttackingState (this);
		neutralState = new NeutralState (this);
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	// Use this for initialization
	void Start () {


		currentState = neutralState;
		#region
		distanceFromTarget = 7f;
		player = GameObject.FindGameObjectWithTag("Player").gameObject;
		combatManager = GameObject.FindGameObjectWithTag("CombatManager").gameObject;
		#endregion
	}
	
	// Update is called once per frame
	void Update () {

		currentState.UpdateState ();
		#region
		FovCheck (player, 360);
		if(playerInSight == true && currentState != attackingState){
			currentState.ToAttackState ();
		}

		float velocity = navMeshAgent.velocity.magnitude; 
		if (velocity > 0.1f || navMeshAgent.updateRotation == false ) { 						      
			anim.SetBool ("Moving", true);

		} else {
			anim.SetBool ("Moving", false);
		}
		#endregion
	}

	public void ToAttackingState(){
		currentState.ToAttackState ();
	}

	public void ToNeutralState(){
		currentState.ToNeutralState ();
	}
	private void OnTriggerEnter(Collider other)
	{
			currentState.OnTriggerEnter (other);
	}

	private void OnTriggerStay(Collider other)
	{
		currentState.OnTriggerStay (other);
	}

	private void OnTriggerExit(Collider other)
	{
		currentState.OnTriggerExit (other);
	}

	public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {

		Vector3 randDirection = Random.insideUnitSphere * dist;
		randDirection += origin;
		NavMeshHit navHit;
		NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
		return navHit.position;
	}

	public void FovCheck(GameObject target, float fovAngle){
		
		Vector3 direction = target.transform.position - transform.position;
		float angle = Vector3.Angle (direction, transform.forward);
		if (angle < fovAngle * 0.5f) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, direction, out hit, 5f)) {
				if (hit.collider.tag == "Player") {
					playerCanBeSeen = true;
					if (angle < 60f) {
						playerInSight = true;
						navMeshAgent.enabled = true;
						playerCanBeSeen = true;
					} else {
						playerInSight = false;
					}
				}
			}  else {
			playerCanBeSeen = false;
			playerInSight = false;
		}

	} 
}

	public void Dead(){
		dead = true;
		navMeshAgent.Stop ();
		navMeshAgent.speed = 0;
		navMeshAgent.destination = transform.position;
		GameObject.Destroy (movePoint);
		combatManager.GetComponent<CombatManager> ().RemoveFromList (gameObject);

	}

	public void AttackPlayer(){
		attack = true;
	}

	public void SetAngle(float angle){
		tempAngle = angle;
	}
}
