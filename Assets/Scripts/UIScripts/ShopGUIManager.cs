using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopGUIManager : MonoBehaviour {

	ItemManager itemManager;
	InventoryManager inventoryManager;
	public GameObject inventoryPanel;
	public GameObject weaponSlotPanel;
	public GameObject shieldSlotPanel;
	public GameObject potsSlotPanel;
	public GameObject inventorySlot;
	public GameObject inventoryItem;
	public GameObject goldCount;


	public List<GameObject> weaponsSlots = new List<GameObject>();
	public List<GameObject> shieldsSlots = new List<GameObject>();


	void Awake(){
		itemManager = GameObject.FindGameObjectWithTag ("ItemManager").GetComponent<ItemManager> ();
		inventoryManager = GameObject.FindGameObjectWithTag ("InventoryManager").GetComponent<InventoryManager> ();
		inventoryPanel = GameObject.Find ("Shop");
		weaponSlotPanel = inventoryPanel.transform.Find ("Weapon Slot Panel").gameObject;
		shieldSlotPanel = inventoryPanel.transform.Find ("Shield Slot Panel").gameObject;
		potsSlotPanel = inventoryPanel.transform.Find ("Pots Slot Panel").gameObject;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {



	}

	public void SetUpShop(){

		if (itemManager.weaponList.Count > 0) {
			for (int i = 0; i < weaponsSlots.Count; i++) {
				Destroy(weaponsSlots [i]);
			}
			weaponsSlots.Clear ();
		}

		if (shieldsSlots.Count > 0) {
			for (int i = 0; i < shieldsSlots.Count; i++) {
				
				Destroy(shieldsSlots [i]);
			}
			shieldsSlots.Clear();
		}

		for (int i = 0; i < itemManager.weaponList.Count; i++) {
			weaponsSlots.Add (Instantiate (inventorySlot));
			weaponsSlots [i].transform.SetParent (weaponSlotPanel.transform);
			weaponsSlots [i].GetComponent<StoreSlotItem> ().AddWeaponID (itemManager.weaponList[i].ID);
		}

		for (int i = 0; i < itemManager.shieldList.Count; i++) {
			shieldsSlots.Add (Instantiate (inventorySlot));
			shieldsSlots [i].transform.SetParent (shieldSlotPanel.transform);
			shieldsSlots [i].GetComponent<StoreSlotItem> ().AddShieldID (itemManager.shieldList[i].ID);
		}
		goldCount.GetComponent<Text> ().text = inventoryManager.gold.ToString ();

	}

	public void CloseShop(){
		gameObject.SetActive (false);
	}

	public void UpdateGoldCount(){
		goldCount.GetComponent<Text> ().text = inventoryManager.gold.ToString ();

	}

}
