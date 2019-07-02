using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {


	public static DialogSystem Instance{ get; set;}
	public GameObject dialoguePanel;
	public ShopGUIManager shopGUI;
	public string npcName;
	public int dialogueIndex;
	public List<string> dialogueLines = new List<string>();
	public List<string> tempLines = new List<string> ();

	public bool saveBool;
	public int interactiveLine;
	public int questID;
	public GameObject playerGO;

	Button nextButton;
	Button YesButton;
	Button NoButton;
	Text dialogueText, nameText;

	// Use this for initialization
	void Awake(){
		YesButton = dialoguePanel.transform.Find ("Agree").GetComponent<Button> ();
		NoButton = dialoguePanel.transform.Find ("Disagree").GetComponent<Button> ();
		nextButton = dialoguePanel.transform.Find ("Next").GetComponent<Button>();
		dialogueText = dialoguePanel.transform.Find ("Text").GetComponent<Text>();
		playerGO = GameObject.FindGameObjectWithTag ("Player");
		nameText = dialoguePanel.transform.Find ("Name").GetChild (0).GetComponent<Text> ();
		nextButton.onClick.AddListener (delegate {
			ContinueDialog ();
		});

		NoButton.onClick.AddListener (delegate {
			ContinueDialog ();
		});
		dialoguePanel.SetActive(false);



		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}
	}
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		
	}

	public void AddNewDialogue(List<string> lines, string npcName){
		dialogueIndex = 0;
		dialogueLines.Clear();
		dialogueLines.AddRange (lines);
		this.npcName = npcName;
		CreateDialogue ();
	
	}

	public void AddNewDialogue(List<string> lines, string npcName, int line){
			saveBool = true;
			interactiveLine = line;
			dialogueIndex = 0;
			dialogueLines.Clear ();
			dialogueLines.AddRange (lines);
			this.npcName = npcName;
			CreateDialogue ();
	}

	public void AddNewDialogue(List<string> lines, string npcName, int line, int questid){
			saveBool = false;
			interactiveLine = line;
			dialogueIndex = 0;
			questID = questid;
			dialogueLines.Clear ();
			dialogueLines.AddRange (lines);
			this.npcName = npcName;
			CreateDialogue ();

		}

	public void CreateDialogue(){
		ButtonController ();
		dialogueText.text = dialogueLines[dialogueIndex];
		nameText.text = npcName;
		dialoguePanel.SetActive (true);
		GameManager.gameManager.CheckUI ();

	}

	public void ContinueDialog(){
		dialogueIndex++;
		if (dialogueIndex <= dialogueLines.Count - 1) {
			if (dialogueIndex != dialogueLines.Count) {
				dialogueText.text = dialogueLines [dialogueIndex];
				} else {
				ClearSystem ();
				dialoguePanel.SetActive (false);
			}
		} else {
			ClearSystem ();
			dialoguePanel.SetActive (false);

		}
		GameManager.gameManager.CheckUI ();
		ButtonController ();
	}



	public void HealPlayer(){

		if (playerGO.GetComponent<PlayerStats> ().currentHealth < 100f && InventoryManager.inventoryManager.gold >= 100) {
			Debug.Log ((InventoryManager.inventoryManager.gold >= 100) + "Does player have enough gold");
			playerGO.GetComponent<PlayerStats> ().currentHealth = 100f;
			playerGO.GetComponent<PlayerStats> ().UpdateHealthBar ();
			InventoryManager.inventoryManager.gold -= 100;
		} else if(InventoryManager.inventoryManager.gold < 100){
			AddNewDialogue (tempLines, "Computer");
			dialogueIndex = 0;
		}else {
			AddNewDialogue (tempLines, "Computer");
			dialogueIndex = 0;
		}
		YesButton.gameObject.SetActive (false);
		NoButton.gameObject.SetActive (false);
		nextButton.gameObject.SetActive (true);
	}

	public void OpenStore(){
		shopGUI.gameObject.SetActive (true);
		shopGUI.SetUpShop ();
		}

	public void ButtonController(){
		if (npcName == "ShopKeeper"){
			if (dialogueIndex == interactiveLine) {
				YesButton.gameObject.SetActive (true);
				NoButton.gameObject.SetActive (true);
				nextButton.gameObject.SetActive (false);
				YesButton.onClick.RemoveAllListeners ();
				YesButton.onClick.AddListener (delegate {
					OpenStore ();
					ContinueDialog ();
				});
			}
			return;
		} else  if (saveBool == true){
			if (dialogueIndex == interactiveLine) {
				YesButton.gameObject.SetActive (true);
				NoButton.gameObject.SetActive (true);
				nextButton.gameObject.SetActive (false);
				YesButton.onClick.RemoveAllListeners ();
				YesButton.onClick.AddListener (delegate {
					HealPlayer ();
				});
				if (playerGO.GetComponent<PlayerStats> ().currentHealth >= 100f) {
					tempLines.Clear ();
					for (int o = 0; o < DatabaseLoader.database.DialogueData ["Computer"] [0] ["health"].Count; o++) {
						tempLines.Add ((string)DatabaseLoader.database.DialogueData ["Computer"] [0] ["health"] [o]);
					}
				} else if (InventoryManager.inventoryManager.gold < 100) {
					tempLines.Clear ();
					for (int o = 0; o < DatabaseLoader.database.DialogueData ["Computer"] [0] ["gold"].Count; o++) {
						tempLines.Add ((string)DatabaseLoader.database.DialogueData ["Computer"] [0] ["gold"] [o]);
					}
				}
			}
		} else if(saveBool == false) {
				YesButton.gameObject.SetActive (false);
				NoButton.gameObject.SetActive (false);
				nextButton.gameObject.SetActive (true);
				YesButton.onClick.RemoveAllListeners ();
				YesButton.onClick.AddListener (delegate {
					QuestManager.questManager.AcceptQuest (questID);
					ClearSystem ();
					dialoguePanel.SetActive (false);
					GameManager.gameManager.CheckUI ();
				});	
			}

		if (dialogueIndex == interactiveLine && QuestManager.questManager.questList [questID].progess == Quest.QuestProgress.AVAILABLE) {
			YesButton.gameObject.SetActive (true);
			NoButton.gameObject.SetActive (true);
			nextButton.gameObject.SetActive (false);

		} else if (dialogueIndex == interactiveLine) {
			YesButton.gameObject.SetActive (true);
			NoButton.gameObject.SetActive (true);
			nextButton.gameObject.SetActive (false);
		} else {
			YesButton.gameObject.SetActive (false);
			NoButton.gameObject.SetActive (false);
			nextButton.gameObject.SetActive (true);
		}
		if (QuestManager.questManager.currentQuestList.Count > 0) {
			if (QuestManager.questManager.questList [questID].progess == Quest.QuestProgress.COMPLETE) {
				QuestManager.questManager.CompleteQuest (questID);
				QuestManager.questManager.questList [questID].progess = Quest.QuestProgress.DONE;
			}
		}
	}

	public void ClearSystem(){
		saveBool = false;
		dialogueIndex = 0;
		npcName = null;
		dialogueLines.Clear ();
	}
}
