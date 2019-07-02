using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class DatabaseLoader : MonoBehaviour {

	public static DatabaseLoader database{ get; set;}
	private string jsonString;
	public JsonData QuestData;
	public JsonData DialogueData;
	public JsonData ItemData;
	public JsonData EnemiesData;

	void Awake(){
		DontDestroyOnLoad (gameObject);
		if (database == null) {
			database = this;
		} else if (database != this) {
			Destroy (gameObject);
		}

	}
	// Use this for initialization
	void Start () {
		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/Quests.json");
		QuestData = JsonMapper.ToObject(jsonString);

		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/Dialogue.json");
		DialogueData = JsonMapper.ToObject(jsonString);

		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/Items.JSON");
		ItemData = JsonMapper.ToObject(jsonString);

		jsonString = File.ReadAllText (Application.dataPath + "/Scripts/Json/Enemies.JSON");
		EnemiesData = JsonMapper.ToObject(jsonString);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
