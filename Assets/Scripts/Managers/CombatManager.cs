using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {


	public List<GameObject> fightEnemies = new List<GameObject>();

	public List<GameObject> waitingEnemies = new List<GameObject>();


	// Use this for initialization
	void Start () {
		
		InvokeRepeating("Attack", 5f, 5);
	}
	
	// Update is called once per frame
	void Update () {
		if (fightEnemies.Count < 3 && waitingEnemies.Count > 0) {
			
			AddToList (waitingEnemies [0]);
			waitingEnemies.Remove (waitingEnemies [0]);
		}

	}

	public void AddToList(GameObject enemy){
		enemy.GetComponent<EnemyStats> ().EnableOverHeadHealth ();
		enemy.SendMessage ("SetAngle", Random.Range (0, 360));
		if (fightEnemies.Count >= 3) {
			waitingEnemies.Add (enemy);
			enemy.GetComponent<SkelStateMachine> ().distanceFromTarget = 9f;
		} else {
			fightEnemies.Add (enemy);
			enemy.GetComponent<SkelStateMachine> ().distanceFromTarget = 4f;
		}
	}

	public void RemoveFromList(GameObject enemy){
		for (int i = 0; i < fightEnemies.Count; i++) {
			if (fightEnemies [i] == enemy) {
				fightEnemies.Remove (fightEnemies [i]);
				return;
			}
		}

		for (int i = 0; i < waitingEnemies.Count; i++) {
			if (fightEnemies [i] == enemy) {
				fightEnemies.Remove (fightEnemies [i]);
				return;
			}
		}
	}

	public void Attack(){

		if (fightEnemies.Count > 0) {
			for (int i = 0; i < fightEnemies.Count; i++) {
				fightEnemies [i].SendMessage ("Attack");
			}
		}
	
	}
		
}
