using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUIManager : MonoBehaviour {

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
		inventoryManager = GameObject.FindGameObjectWithTag ("InventoryManager").GetComponent<InventoryManager> ();
		inventoryPanel = GameObject.Find ("Inventory");
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

	public void SetUpInventory(){
		
		if (weaponsSlots.Count > 0) {
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

		for (int i = 0; i < inventoryManager.weapons.Count; i++) {
			weaponsSlots.Add (Instantiate (inventorySlot));
			weaponsSlots [i].transform.SetParent (weaponSlotPanel.transform);
			weaponsSlots [i].GetComponent<SlotItem> ().AddWeaponID (inventoryManager.weapons[i].ID);
		}

		for (int i = 0; i < inventoryManager.shields.Count; i++) {
			shieldsSlots.Add (Instantiate (inventorySlot));
			shieldsSlots [i].transform.SetParent (shieldSlotPanel.transform);
			shieldsSlots [i].GetComponent<SlotItem> ().AddShieldID (inventoryManager.shields[i].ID);
		}
	
		goldCount.GetComponent<Text> ().text = inventoryManager.gold.ToString ();
	}


}
