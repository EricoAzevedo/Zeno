using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponToolTip : MonoBehaviour {

	ItemManager itemManager;
	public GameObject name;
	public GameObject description;
	public GameObject damage;
	public GameObject attackSped;

	public weapon weapon;
	// Use this for initialization
	void Awake () {


		itemManager = GameObject.FindGameObjectWithTag ("ItemManager").GetComponent<ItemManager> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate(int ID){
		for (int i = 0; i < itemManager.weaponList.Count ; i++){
			if (itemManager.weaponList [i].ID == ID) {
				name.GetComponent<Text>().text = itemManager.weaponList [i].name;
				description.GetComponent<Text>().text = itemManager.weaponList [i].description;
				damage.GetComponent<Text>().text = itemManager.weaponList [i].attackDamage.ToString();
				attackSped.GetComponent<Text>().text = itemManager.weaponList [i].attackSpeed.ToString();
			}	
		}
	}
}
