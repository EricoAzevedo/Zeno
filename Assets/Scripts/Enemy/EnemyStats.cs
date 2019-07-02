using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour {

	public int iD;
	public int health;
	public int damage;
	public int moveSpeed;
	public int gold;

	DatabaseLoader Database;
	QuestManager questManager;
	EnemyManager enemyManager;


	public float currentHealth;
	public GameObject healthBar;
	public GameObject OverHHealth;

	float deathTimer;
	bool dead;
	public Animator anim;


	public GameObject CoinsGO;


	// Use this for initialization
	void Start () {
		Database = GameObject.FindGameObjectWithTag ("Database").GetComponent<DatabaseLoader> ();
		questManager = GameObject.FindGameObjectWithTag ("QuestManager").GetComponent<QuestManager>();
		enemyManager = GameObject.FindGameObjectWithTag ("EnemyManager").GetComponent<EnemyManager>();
		currentHealth = health;
		SetUpEnemy ();
		currentHealth = health;
	}

	// Update is called once per frame
	void Update () {
		Timer ();
	}

	public void TakeDamage(float damage){
		if (dead == false) {
			gameObject.GetComponent<SkelStateMachine> ().currentState = gameObject.GetComponent<SkelStateMachine> ().attackingState;
			currentHealth -= damage;
			float cHealth = currentHealth / health;
			if (cHealth >= 0) {
				SetHealthBar (cHealth);
			}
			if (currentHealth <= 0) {
				anim.SetBool ("Dead", true);
				dead = true;
				gameObject.SendMessage ("Dead");
				DropGold();
				OverHHealth.SetActive (false);
				questManager.AddQuestObjective (iD);
			} 
			Attack ();
		}
	}

	public void SetHealthBar(float health){
		healthBar.transform.localScale = new Vector3 (health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
		
	void Timer(){
		if (dead == true){
			deathTimer += Time.deltaTime;
		if (deathTimer > 4) {
				GameObject.Destroy (gameObject);
			}
		}
	}

	public void EnableOverHeadHealth(){
		if (OverHHealth.activeInHierarchy == false) {
			OverHHealth.SetActive (true);
		}
	}

	public void Attack(){
		float healthNorm = (currentHealth / health) * 10;
		int chance = Random.Range (0, 15);
		if (Mathf.RoundToInt(healthNorm) >= chance) {
			gameObject.SendMessage ("AttackPlayer");
		} 
	}

	public void DropGold(){
		Vector3 CoinPos = new Vector3 (transform.position.x, 3.36f, transform.position.z);
		GameObject coins = Instantiate (CoinsGO, CoinPos, CoinsGO.transform.rotation);
		coins.SendMessage ("AddValue", gold);
	}

	public void SetUpEnemy(){
		for (int i = 0; i < enemyManager.enemiesList.Count; i++) {
			if (enemyManager.enemiesList [i].iD == iD) {
				gameObject.name = enemyManager.enemiesList [i].name;
				health = enemyManager.enemiesList [i].health;
				damage = enemyManager.enemiesList [i].damage;
				moveSpeed = enemyManager.enemiesList [i].moveSpeed;
				gold = enemyManager.enemiesList [i].gold;
			}
		}
	}
}
