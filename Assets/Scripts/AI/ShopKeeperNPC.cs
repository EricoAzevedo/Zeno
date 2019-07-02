using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeperNPC : Interactable {

	public GameObject TalkDialogPrefab;
	public GameObject thisTalkDialog;
	public string objectName;
	public bool questGiver;
	public bool Terminal;
	public int questID;
	public int questionLine;
	public bool spokenToo;

	public List<string> dialogue = new List<string>();
	// Use this for initialization
	void Start () {
		objectName = gameObject.name;
	}

	// Update is called once per frame
	void Update () {
	}

	public override void Interact(){
		LoadDialogue ();

		DialogSystem.Instance.AddNewDialogue (dialogue, objectName, questionLine, questID);
		if (questGiver == true) {
			DialogSystem.Instance.AddNewDialogue (dialogue, objectName, questionLine, questID);
		} else if (Terminal == true) {
			DialogSystem.Instance.AddNewDialogue (dialogue, objectName, questionLine);
		}
	}

	public override void ShowInfo (){
		thisTalkDialog = Instantiate<GameObject> (TalkDialogPrefab, gameObject.transform, false);
	}

	public override void RemoveInfo (){
		Destroy(thisTalkDialog);

	}

	public void LoadDialogue(){
		dialogue.Clear ();
		if (questGiver == true) {
			questID = (int) DatabaseLoader.database.DialogueData [objectName.ToString()][0]["questID"];
			questionLine = (int)DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["questionLine"];
			for (int i = 0; i < QuestManager.questManager.questList.Count; i++){
				if (QuestManager.questManager.questList[i].id == questID && QuestManager.questManager.questList[i].progess == Quest.QuestProgress.AVAILABLE) {
					for (int o = 0; o < DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["questAvailable"].Count; o++) {
						dialogue.Add ((string)DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["questAvailable"] [o]);
					}
				} else if(QuestManager.questManager.questList[i].id == questID && QuestManager.questManager.questList[i].progess == Quest.QuestProgress.ACCEPTED){
					for (int o = 0; o < DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["questAccepted"].Count; o++) {
						dialogue.Add ((string)DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["questAccepted"] [o]);
					}
					questionLine = 10;
				} else if(QuestManager.questManager.questList[i].id == questID && QuestManager.questManager.questList[i].progess == Quest.QuestProgress.COMPLETE){
					for (int o = 0; o < DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["questCompleted"].Count; o++) {
						dialogue.Add ((string)DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["questCompleted"] [o]);
					}
					questionLine = 10;
				}
			}
		} else if(Terminal == true){
			if (spokenToo == false) {
				questionLine = (int)DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["questionLine"];
				for (int o = 0; o < DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["firstTime"].Count; o++) {
					dialogue.Add ((string)DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["firstTime"] [o]);
				}		spokenToo = true;
			} else if (spokenToo == true){
				questionLine = (int)DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["questionLine2"];
				for (int o = 0; o < DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["othertimes"].Count; o++) {
					dialogue.Add ((string)DatabaseLoader.database.DialogueData [objectName.ToString ()] [0] ["othertimes"] [o]);
				}
			}
		}
	}
}
