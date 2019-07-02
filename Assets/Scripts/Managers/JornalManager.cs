using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JornalManager : MonoBehaviour {

	public QuestManager questManager;
	public GameObject questButton;
	public RectTransform currentPanel;
	public RectTransform completedPanel;

	public List<GameObject> buttons = new List<GameObject> ();
	public Text questName;
	public Text questDescription;
	public Text QuestReward;
	public Text QuestProgress;


	// Use this for initialization

	void Awake(){
		questManager = GameObject.FindGameObjectWithTag ("QuestManager").GetComponent<QuestManager> ();
	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetUpJornal(){
		if (buttons.Count > 0) {
			for (int i = 0; i < buttons.Count; i++) {
				Debug.Log ("Destorying buttons");
				Destroy(buttons [i]);
			}
		}
		buttons.Clear ();
		questName.text = "Select a quest";
		questDescription.text = "";
		QuestProgress.text = "";
		for(int i = 0; i < questManager.currentQuestList.Count; i++)
		{
			GameObject tempQuestButtonGO = (GameObject)Instantiate(questButton);
			if (questManager.currentQuestList [i].progess == Quest.QuestProgress.COMPLETE) {
				tempQuestButtonGO.transform.SetParent (completedPanel, false);
					} else if (questManager.currentQuestList [i].progess == Quest.QuestProgress.ACCEPTED){
						tempQuestButtonGO.transform.SetParent (currentPanel, false);
					}

			tempQuestButtonGO.transform.localScale = new Vector3(1, 1, 1);

			Button tempQuestButton = tempQuestButtonGO.GetComponent<Button>();
			buttons.Add (tempQuestButtonGO);
			tempQuestButton.GetComponentInChildren<Text> ().text = questManager.currentQuestList [i].questName;
			int tempQuestID = questManager.currentQuestList [i].id;
			tempQuestButton.onClick.AddListener(() => ButtonClicked(tempQuestID));
	
		}


	}

	void ButtonClicked(int ID)
	{
		for(int i = 0; i < questManager.currentQuestList.Count; i++){
			if (ID == questManager.currentQuestList[i].id){
				questName.text = questManager.currentQuestList [i].questName;
				questDescription.text = questManager.currentQuestList [i].description;
				QuestProgress.text = questManager.currentQuestList [i].questObjectiveCount.ToString () + "/" + questManager.currentQuestList [i].questObjectiveRequirement.ToString ();
			}
		}
	}

}
