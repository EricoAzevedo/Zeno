using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SkelMStateMachine : MonoBehaviour {



	[HideInInspector] public SkelMState currentState;
	[HideInInspector] public AttackState attackingState;
	[HideInInspector] public NeutralState1 neutralState;
	[HideInInspector] public AggresiveState aggresiveState;
	[HideInInspector] public NavMeshAgent navMeshAgent;


	public Animator anim;
	public Vector3 StartPos;
	public bool pointReached;
	public float idleTime;
	public float distanceFromTarget;
	public bool playerCanBeSeen;
	public GameObject player;
	public bool playerInSight;
	public float turnSpeed;
	public float angle;

	private void Awake()
	{
		attackingState = new AttackState (this);
		neutralState = new NeutralState1 (this);
		aggresiveState = new AggresiveState (this);
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}


	// Use this for initialization
	void Start () {

		currentState = neutralState;
		StartPos = transform.position;
		player = GameObject.FindGameObjectWithTag("Player").gameObject;

		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Mother is on state" + currentState.ToString ());
		currentState.UpdateState ();
		float velocity = navMeshAgent.velocity.magnitude; 
		FovCheck (player, 360);
		if(playerInSight == true && currentState != attackingState){
			currentState.ToAggresiveState ();
		}

		if (velocity > 0.5f) { 						      
			anim.SetBool ("Moving", true);

		} else {
			anim.SetBool ("Moving", false);
		}	}

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
			Debug.DrawRay (transform.position, direction);
			if (Physics.Raycast (transform.position, direction, out hit, 5f)) {
				playerCanBeSeen = true;
				if (angle < 60f) {
					playerInSight = true;
					navMeshAgent.enabled = true;
					playerCanBeSeen = true;
				} else {
					playerInSight = false;
				}

			} else {
				playerCanBeSeen = false;
				playerInSight = false;

			}

		}
	}

	public float CalculateAngle(Vector3 from, Vector3 to)
	{
		return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
	}
}
