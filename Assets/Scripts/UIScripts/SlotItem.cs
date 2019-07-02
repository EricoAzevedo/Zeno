using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotItem : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

	public InventoryManager inventoryManager;
	public GameObject InventoryGUI;
	public GameObject weaponToolTip;
	public GameObject weaponSlot;
	public PlayerSword playerSwordSlot;
	public PlayerShield playerShieldSlot;

	public GameObject shieldSlot;
	public int weaponID = 999;
	public int shieldID = 999;
	// Use this for initialization

	void Awake(){
		inventoryManager = GameObject.FindGameObjectWithTag ("InventoryManager").GetComponent<InventoryManager>();

	}

	void Start () {
		InventoryGUI = GameObject.FindGameObjectWithTag ("InventoryGUI");
		weaponSlot = GameObject.FindGameObjectWithTag ("Player").transform.Find ("SwordSlot").gameObject;
		weaponToolTip = InventoryGUI.transform.Find ("Weapon Tool Tip").gameObject;
		playerSwordSlot = GameObject.FindGameObjectWithTag ("SwordSlot").GetComponent<PlayerSword>();
		playerShieldSlot = GameObject.FindGameObjectWithTag ("ShieldSlot").GetComponent<PlayerShield>();

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
			if (weaponToolTip.activeInHierarchy) {
				weaponToolTip.GetComponent<WeaponToolTip> ().Activate (weaponID);
			} else {
				weaponToolTip.SetActive (true);
				weaponToolTip.GetComponent<WeaponToolTip> ().Activate (weaponID);

			}
		} else if (shieldID < 999) {
			if (weaponToolTip.activeInHierarchy) {
				weaponToolTip.GetComponent<WeaponToolTip> ().Activate (shieldID);
			} else {
				weaponToolTip.SetActive (true);
				weaponToolTip.GetComponent<WeaponToolTip> ().Activate (shieldID);

			}
			
			
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (weaponID < 999) {
			for (int i = 0; i < inventoryManager.weapons.Count; i++) {
				if (inventoryManager.weapons [i].ID == weaponID) {
					playerSwordSlot.EquiptWeapon (weaponID, inventoryManager.weapons [i]);
				}
			}
		} else if (shieldID < 999) {
			for (int i = 0; i < inventoryManager.shields.Count; i++) {
				if (inventoryManager.shields [i].ID == shieldID) {
					playerShieldSlot.EquiptShield (shieldID, inventoryManager.shields [i]);
				}
			}
		}
	}

	public void OnPointerExit (PointerEventData evenData){
		if (weaponID < 999) {
			weaponToolTip.SetActive (false);
		}
	}
}
