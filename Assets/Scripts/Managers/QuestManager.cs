using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

	public static QuestManager questManager;
	public DatabaseLoader Database;
	public InventoryManager inventory;
	public List <Quest> questList = new List<Quest> ();        
	public List <Quest> currentQuestList = new List<Quest>();


	void Awake(){

		if (questManager == null) {
			questManager = this;
		} else if (questManager != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	void Start(){
		Database = GameObject.FindGameObjectWithTag ("Database").GetComponent<DatabaseLoader> ();
		inventory = GameObject.FindGameObjectWithTag ("InventoryManager").GetComponent<InventoryManager> ();

		ReadQuestData ();

	}
	public void AcceptQuest(int questID){
		for (int i = 0; i < questList.Count; i++){
			if (questList [i].id == questID && questList [i].progess == Quest.QuestProgress.AVAILABLE) {
				currentQuestList.Add (questList [i]);
				questList [i].progess = Quest.QuestProgress.ACCEPTED;
			}
		}
		
	}

	public void CompleteQuest(int questID){
		for (int i = 0; i < currentQuestList.Count; i++){
			if (currentQuestList [i].id == questID && currentQuestList [i].progess == Quest.QuestProgress.COMPLETE) {
				currentQuestList [i].progess = Quest.QuestProgress.DONE;
				inventory.AddGold (currentQuestList [i].goldReward);
				currentQuestList.Remove (currentQuestList [i]);
			}
		}

	}
	public void AddQuestObjective(int questObjective){
		for (int i = 0; i < currentQuestList.Count; i++) {
			if (currentQuestList [i].questObjective == questObjective && currentQuestList [i].progess == Quest.QuestProgress.ACCEPTED) {
				currentQuestList [i].questObjectiveCount += 1;
				}
			if (currentQuestList [i].questObjectiveCount >= currentQuestList [i].questObjectiveRequirement && currentQuestList [i].progess == Quest.QuestProgress.ACCEPTED) {
				currentQuestList [i].progess = Quest.QuestProgress.COMPLETE;
				}
			}
		}

	public bool RequestAvaiableQuest(int questID){
		for (int i = 0; i < questList.Count; i++) {
			if (questList [i].id == questID && questList [i].progess == Quest.QuestProgress.AVAILABLE) {
			
				return true;
			}

		}
		return false;

	}

	public bool RequestAcceptedQuest(int questID){
		for (int i = 0; i < questList.Count; i++) {
			if (questList [i].id == questID && questList [i].progess == Quest.QuestProgress.ACCEPTED) {

				return true;
			}
		}
		return false;
	}

	public bool RequestCompleteQuest(int questID){
		for (int i = 0; i < questList.Count; i++) {
			if (questList [i].id == questID && questList [i].progess == Quest.QuestProgress.COMPLETE) {

				return true;
			}

		}
		return false;
	}

	public void ReadQuestData(){
		for (int i = 0; i < Database.QuestData["Quests"].Count; i++){
			Quest NewQuest = new Quest();
			NewQuest.description = (string) Database.QuestData ["Quests"][i]["description"];
			NewQuest.questName = (string) Database.QuestData ["Quests"][i]["name"];
			NewQuest.type = (string) Database.QuestData ["Quests"][i]["type"];
			NewQuest.questObjectiveRequirement = (int) Database.QuestData ["Quests"][i]["huntAmount"];
			NewQuest.questObjective = (int) Database.QuestData ["Quests"][i]["monsterID"];
			NewQuest.expReward = (int)  Database.QuestData ["Quests"][i]["expReward"];
			NewQuest.goldReward = (int)  Database.QuestData ["Quests"][i]["goldReward"];
			NewQuest.id = (int)  Database.QuestData ["Quests"][i]["questId"];
			questList.Add (NewQuest);
		}
				
	}
}
