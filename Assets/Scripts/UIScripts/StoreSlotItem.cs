using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreSlotItem : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

	public InventoryManager inventoryManager;
	public GameObject InventoryGUI;
	public GameObject shopToolTip;
	public GameObject weaponSlot;
	public GameObject shieldSlot;
	public int weaponID = 999;
	public int shieldID = 999;
	// Use this for initialization

	void Awake(){
		inventoryManager = GameObject.FindGameObjectWithTag ("InventoryManager").GetComponent<InventoryManager>();

	}
	void Start () {
		
		InventoryGUI = GameObject.FindGameObjectWithTag ("ShopGUI");
		weaponSlot = GameObject.FindGameObjectWithTag ("Player").transform.Find ("SwordSlot").gameObject;
		shopToolTip = InventoryGUI.transform.Find ("Shop Tool Tip").gameObject;


	}

	// Update is called once per frame
	void Update () {

	}


	public void AddWeaponID(int ID){
		weaponID = ID;
		transform.GetChild (0).GetComponent<Image> ().sprite = inventoryManager.weaponSprites [weaponID];
	}

	public void AddShieldID(int ID){
		shieldID = ID;
		transform.GetChild (0).GetComponent<Image> ().sprite = inventoryManager.shieldSprites [shieldID];

	}

	public void OnPointerEnter (PointerEventData evenData){
		if (weaponID < 999) {
			if (shopToolTip.activeInHierarchy) {
				shopToolTip.GetComponent<StoreToolTip> ().ActivateSword (weaponID);
			} else {
				shopToolTip.SetActive (true);
				shopToolTip.GetComponent<StoreToolTip> ().ActivateSword (weaponID);

			}
		} else if (shieldID < 999) {
			if (shopToolTip.activeInHierarchy) {
				shopToolTip.GetComponent<StoreToolTip> ().ActivateShield (shieldID);
			} else {
				shopToolTip.SetActive (true);
				shopToolTip.GetComponent<StoreToolTip> ().ActivateShield (shieldID);

			}
			
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (weaponID < 999) {
			inventoryManager.AddWeapon (weaponID);
		} else if (shieldID < 999){
			inventoryManager.AddShield (shieldID);
					}
	}

	public void OnPointerExit (PointerEventData evenData){
		
			shopToolTip.SetActive (false);

	}
}
