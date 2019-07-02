using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	public static ItemManager itemManager;
	public DatabaseLoader Database;
	public List <Shield> shieldList = new List<Shield> ();
	public List <weapon> weaponList = new List<weapon> ();

	void Awake(){

		if (itemManager == null) {
			itemManager = this;
		} else if (itemManager != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}


	// Use this for initialization
	void Start () {
		Database = GameObject.FindGameObjectWithTag ("Database").GetComponent<DatabaseLoader> ();
		ReadQuestData ();
	}

	// Update is called once per frame
	void Update () {
	}
		

	public void ReadQuestData(){
		for (int i = 0; i < Database.ItemData ["Weapons"].Count; i++) {
			weapon newWeapon = new weapon ();
			newWeapon.name = (string)Database.ItemData ["Weapons"][i]["name"];
			newWeapon.ID = (int)Database.ItemData ["Weapons"] [i] ["itemID"];
			newWeapon.attackDamage = (int)Database.ItemData ["Weapons"] [i] ["attackDamage"];
			newWeapon.attackSpeed = (int)Database.ItemData ["Weapons"] [i] ["attackSpeed"];
			newWeapon.description = (string)Database.ItemData ["Weapons"] [i] ["description"];
			newWeapon.slug = (string)Database.ItemData ["Weapons"] [i] ["slug"];
			newWeapon.stackable = (bool)Database.ItemData ["Weapons"] [i] ["stackable"];
			newWeapon.value = (int)Database.ItemData ["Weapons"] [i] ["value"];
			weaponList.Add (newWeapon);
		}

		for (int i = 0; i < Database.ItemData ["Shield"].Count; i++) {
			Shield newShield = new Shield ();
			newShield.name = (string)Database.ItemData ["Shield"][i]["name"];
			newShield.ID = (int)Database.ItemData ["Shield"] [i] ["itemID"];
			newShield.shieldDefence= (int)Database.ItemData ["Shield"] [i] ["armorDefence"];
			newShield.shieldHealth = (int)Database.ItemData ["Shield"] [i] ["armorHealth"];
			newShield.description = (string)Database.ItemData ["Shield"] [i] ["description"];
			newShield.stackable = (bool)Database.ItemData ["Shield"] [i] ["stackable"];
			newShield.slug = (string)Database.ItemData ["Shield"] [i] ["slug"];
			newShield.value = (int)Database.ItemData ["Shield"] [i] ["value"];
			shieldList.Add (newShield);
		}
	}
}
