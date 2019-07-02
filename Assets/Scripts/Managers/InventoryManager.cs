using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public static InventoryManager inventoryManager;
	public GameObject ShopGUI;
	public List<Sprite> weaponSprites = new List<Sprite>();
	public List<Sprite> shieldSprites = new List<Sprite>();


	ItemManager itemManager;

	public int gold;


	public List<weapon> weapons = new List<weapon>();
	public List<Shield> shields = new List<Shield>();
	public List<GameObject> slots = new List<GameObject>();


	void Awake(){
		
		if (inventoryManager == null) {
			inventoryManager = this;
		} else if (inventoryManager != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

		itemManager = GameObject.FindGameObjectWithTag ("ItemManager").GetComponent<ItemManager> ();

		
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AddWeapon(int ID){
		Debug.Log (itemManager.weaponList.Count);
		for (int i = 0; i < itemManager.weaponList.Count ; i++){
			if (itemManager.weaponList [i].ID == ID) {
				if (CheckCurrentWeapons (ID) == false) {
					if (itemManager.weaponList [ID].value <= gold) {
						weapons.Add (itemManager.weaponList [ID]);
						gold -= itemManager.weaponList [ID].value;
						ShopGUI.SendMessage ("UpdateGoldCount");

					} else {
						Debug.Log("Can't afford weapon");
					}
				} else {
					Debug.Log ("Already have weapon");
				}
			}
		}
	}

	public void AddShield(int ID){
		for (int i = 0; i < itemManager.shieldList.Count ; i++){
			if (itemManager.shieldList [i].ID == ID) {
				if (CheckCurrentShields (ID) == false) {
					if (itemManager.weaponList [ID].value <= gold) {
						shields.Add (itemManager.shieldList [ID]);
						gold -= itemManager.shieldList [ID].value;
						ShopGUI.SendMessage ("UpdateGoldCount");
					} else {
						Debug.Log("Can't afford shield");
						}
					} else {
					Debug.Log ("Already have shield");
				}
			}
		}
	}

	public void RemoveWeapon(int ID){

	}

	public void AddGold(int goldAmount){

		gold += goldAmount;
		
	}


	public bool CheckCurrentWeapons(int ID){
		for (int i = 0; i < weapons.Count; i++){
			if (weapons [i].ID == ID) {
				return true;			
			} 
		}
		return false;
	}

	public bool CheckCurrentShields(int ID){
		for (int i = 0; i < shields.Count; i++){
			if (shields [i].ID == ID) {
				return true;			
			} 
		}
		return false;
	}
}
