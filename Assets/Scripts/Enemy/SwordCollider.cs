using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour {


	public GameObject Holder;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player" && Holder.GetComponent<SkelStateMachine> ().attack == true) {
			Holder.GetComponent<SkelStateMachine> ().attack = false;
			Holder.GetComponent<SkelStateMachine> ().anim.SetBool ("Attack", false);
			other.SendMessage ("TakeDamage", Holder.GetComponent<EnemyStats> ().damage);
		}
	}
}
