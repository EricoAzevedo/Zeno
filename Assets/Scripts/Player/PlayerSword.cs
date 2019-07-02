using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour {

	public float swordDamage;
	public GameObject player;
	public GameObject currentWeapon;
	public  List<GameObject> weaponsGO = new List<GameObject>(); 
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy" && player.gameObject.GetComponent<PlayerController>().attacking == true) {
			if (Vector3.Distance(other.transform.position, transform.position) < 3f){
			player.gameObject.GetComponent<PlayerController> ().attacking = false;
			other.SendMessage ("TakeDamage", swordDamage);
			}
		}
	}

	public void EquiptWeapon(int ID, weapon weaponStats){
		if (currentWeapon == null) {
			currentWeapon = Instantiate (weaponsGO [ID], this.transform);
		} else {
			Destroy (currentWeapon);
			currentWeapon = Instantiate (weaponsGO [ID], this.transform);

		}
		swordDamage = weaponStats.attackDamage;
	}
}
