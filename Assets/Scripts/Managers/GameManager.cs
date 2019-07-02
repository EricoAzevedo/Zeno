using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public List<GameObject> uiElements = new List<GameObject>();
	public static GameManager gameManager;
	public bool GamePaused;

	// Use this for initialization
	void Awake(){

		if (gameManager == null) {
			gameManager = this;
		} else if (gameManager != this) {
			Destroy (gameManager.gameObject);
			gameManager = this;
		}
		DontDestroyOnLoad (gameObject);
	}


	void Start () {
		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckUI(){
		for (int i = 0; i < uiElements.Count; i++) {
				if (uiElements [i].activeInHierarchy == true) {
					PauseGame ();
				return;
				} else {
					UnpauseGame ();
				}
			}
	}

	public void DeathScreen(){
		uiElements [5].SetActive (true);
		CheckUI ();

	}

	public void Respawn(){
		if (InventoryManager.inventoryManager.gold >= 50) {
			InventoryManager.inventoryManager.gold -= 50;
		} else {
			InventoryManager.inventoryManager.gold = 0;
		}
		Application.LoadLevel (0);
	}

	public void PauseGame(){

		if (!GamePaused) {
			Time.timeScale = 0;
			GamePaused = true;
		}
	}

	public void UnpauseGame(){
		if (GamePaused) {
			Time.timeScale = 1;
			GamePaused = false;
		}
	}

	public void StartGame(){
		GamePaused = true;
		LoadGUI ();
		CheckUI ();
	}

	public void LoadGUI(){
		uiElements.Clear ();
		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
		for (int i = 0; i < canvas.transform.childCount; i++) {
			canvas.transform.GetChild (i).gameObject.SetActive (true);
		}
		uiElements.Add (GameObject.FindGameObjectWithTag ("DialogGUI"));
		uiElements.Add (GameObject.FindGameObjectWithTag("JournalGUI"));
		uiElements.Add (GameObject.FindGameObjectWithTag("InventoryGUI"));
		uiElements.Add (GameObject.FindGameObjectWithTag("ShopGUI"));
		uiElements.Add (GameObject.FindGameObjectWithTag("StartGUI"));
		uiElements.Add (GameObject.FindGameObjectWithTag("DeathGUI"));
		uiElements.Add (GameObject.FindGameObjectWithTag("HealthGUI"));
		uiElements.Add (GameObject.FindGameObjectWithTag("StaminaGUI"));
		for (int i = 0; i < canvas.transform.childCount; i++) {
			canvas.transform.GetChild (i).gameObject.SetActive (false);
		}
		uiElements [4].SetActive (true);
		uiElements [6].SetActive (true);
		uiElements [7].SetActive (true);
		uiElements.Remove (GameObject.FindGameObjectWithTag ("HealthGUI"));
		uiElements.Remove (GameObject.FindGameObjectWithTag ("StaminaGUI"));

	}
}
