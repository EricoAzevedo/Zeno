using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour {

	public float shieldBlock;
	public float shieldHealth;
	public GameObject player;
	public GameObject currentShield;
	public  List<GameObject> shieldGO = new List<GameObject>(); 
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}



	public void EquiptShield(int ID, Shield shieldStats){
		if (currentShield == null) {
			currentShield = Instantiate (shieldGO [ID], this.transform);
		} else {
			Destroy (currentShield);
			currentShield = Instantiate (shieldGO [ID], this.transform);

		}
		shieldBlock = shieldStats.shieldDefence;
		shieldHealth = shieldStats.shieldHealth;
	}
}
