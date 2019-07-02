using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Interactable : MonoBehaviour {


	public virtual void RotateToInteraction(Rigidbody playerRB){
		playerRB.gameObject.SendMessage ("TurnTo", this.gameObject);
		Interact ();
		}

	public virtual void Interact(){
			Debug.Log("Interacting with base class");
		} 

	public virtual void ShowInfo(){
			
		Debug.Log("asking base class to show info");

		}

	public virtual void RemoveInfo(){

		Debug.Log("asking base class to show info");

	}

	public virtual void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			ShowInfo ();
			}
		}

	public virtual void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			RemoveInfo ();
			}
		}
}
	
