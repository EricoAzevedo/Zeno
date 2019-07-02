using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreToolTip : MonoBehaviour {

	ItemManager itemManager;
	public GameObject name;
	public GameObject description;
	public GameObject damage;
	public GameObject value;

	public weapon weapon;
	// Use this for initialization
	void Awake () {


		itemManager = GameObject.FindGameObjectWithTag ("ItemManager").GetComponent<ItemManager> ();

	}

	// Update is called once per frame
	void Update () {

	}

	public void ActivateSword(int ID){
		for (int i = 0; i < itemManager.weaponList.Count ; i++){
			if (itemManager.weaponList [i].ID == ID) {
				name.GetComponent<Text>().text = itemManager.weaponList [i].name;
				description.GetComponent<Text>().text = itemManager.weaponList [i].description;
				damage.GetComponent<Text>().text = itemManager.weaponList [i].attackDamage.ToString();
				value.GetComponent<Text>().text = itemManager.weaponList [i].value.ToString();
			}	
		}
	}

	public void ActivateShield(int ID){
		for (int i = 0; i < itemManager.shieldList.Count ; i++){
			if (itemManager.shieldList [i].ID == ID) {
				name.GetComponent<Text>().text = itemManager.shieldList [i].name;
				description.GetComponent<Text>().text = itemManager.shieldList [i].description;
				damage.GetComponent<Text>().text = itemManager.shieldList [i].shieldDefence.ToString();
				value.GetComponent<Text>().text = itemManager.shieldList [i].value.ToString();
			}	
		}
	}
}
