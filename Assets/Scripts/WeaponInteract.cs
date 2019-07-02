using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteract : Interactable {

	public GameObject weaponInfoPrefab;
	public GameObject thisWeaponInfo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Interact(){

		Debug.Log ("Interacting with Weapon Class");
	}
	public override void ShowInfo (){

		Debug.Log ("Showing info");

		thisWeaponInfo = Instantiate<GameObject> (weaponInfoPrefab, gameObject.transform, false);
		
	}

	public override void RemoveInfo (){

		Destroy(thisWeaponInfo);

	}

}
