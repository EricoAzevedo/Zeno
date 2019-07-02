using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePointEnemy : MonoBehaviour {

	public NavMeshAgent parentAgent;
	public Transform enemyTransform;
	public float lengthToThisPoint;
	public bool reachable;
	public bool canSeeTarget;
	public Transform Target;
	public Transform lastTarget;

	// Use this for initialization
	void Start () {
		CanMoveToCheck ();
		CanSeeTarget (Target);
	}

	// Update is called once per frame
	void Update () {
	}
		
	public void CanMoveToCheck(){
		if (parentAgent != null) {
		Vector3 size = new Vector3 (0.3f, 0.3f, 0.3f); 
		if (Physics.CheckBox (transform.position, size)) {
			reachable = false;
		} else {
			NavMeshPath path = new NavMeshPath ();

				parentAgent.CalculatePath (transform.position, path);
			if (path.status == NavMeshPathStatus.PathInvalid) {
				reachable = false;
				return;
			} else {
				reachable = true;
				lengthToThisPoint = GetPathLength (path);
				return;
			}
		}
		} else {
			return;
		}

	}

	void CanSeeTarget(Transform target){
		lastTarget = target;
		if (lastTarget != null) {
			RaycastHit hit;
			Vector3 rayDirection = target.position - transform.position;
			Debug.DrawRay (transform.position, rayDirection);
			if (Physics.Raycast (transform.position, rayDirection, out hit)) {
				if (hit.transform.tag == "Player") {
					canSeeTarget = true;
				} else {
					canSeeTarget = false;

				}
			}
		} else {
			return;
		}
	}
	public void RefreshState(){
		CanMoveToCheck ();
		CanSeeTarget(Target);
	}




	public static float GetPathLength( NavMeshPath path )
	{
		float lng = 0.0f;

		for ( int i = 1; i < path.corners.Length; ++i )
		{
			lng += Vector3.Distance( path.corners[i-1], path.corners[i] );
		}

		return lng;
	}
}
