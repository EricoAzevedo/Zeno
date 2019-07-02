using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest {

	public enum QuestProgress{AVAILABLE, NOT_AVAILABLE, ACCEPTED, COMPLETE, DONE}

	public string questName;
	public int id;
	public QuestProgress progess;
	public string description;
	public string type;

	public int questObjective;
	public int questObjectiveCount;
	public int questObjectiveRequirement;

	public int expReward;
	public int goldReward;
	public int itemReward;

}
