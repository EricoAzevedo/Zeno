using UnityEngine;
using System.Collections.Generic;


public class EnemyManager : MonoBehaviour
{
	public static EnemyManager enemyManager;
	public DatabaseLoader Database;
	public List <Enemy> enemiesList = new List<Enemy> ();

	void Awake(){

		if (enemyManager == null) {
			enemyManager = this;
		} else if (enemyManager != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		Database = GameObject.FindGameObjectWithTag ("Database").GetComponent<DatabaseLoader> ();
		ReadEnemiesData();
	}

	public void ReadEnemiesData(){
		for (int i = 0; i < Database.EnemiesData ["Enemies"].Count; i++) {
			Enemy newEnemy = new Enemy ();
			newEnemy.name = (string)Database.EnemiesData ["Enemies"][i]["name"];
			newEnemy.health = (int)Database.EnemiesData ["Enemies"] [i] ["health"];
			newEnemy.iD = (int)Database.EnemiesData ["Enemies"] [i] ["iD"];
			newEnemy.damage = (int)Database.EnemiesData ["Enemies"] [i] ["damage"];
			newEnemy.moveSpeed = (int)Database.EnemiesData ["Enemies"] [i] ["movementSpeed"];
			newEnemy.gold = (int)Database.EnemiesData ["Enemies"] [i] ["gold"];
			enemiesList.Add (newEnemy);
		}
	}
}
