using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

	public int value;
	public GameObject inventoryManager;

	// Use this for initialization
	void Start () {
		
		inventoryManager = GameObject.FindGameObjectWithTag ("InventoryManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddValue(int Value){
		value = Value;
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			inventoryManager.SendMessage ("AddGold", value);
			Destroy (gameObject);
		}
	}
}
