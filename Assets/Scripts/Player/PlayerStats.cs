using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	// Use this for initialization
	public float health;
	public float currentHealth;
	public float stamina;
	public float currentStamina;
	public GameObject healthBar;
	public GameObject staminaBar;
	public bool blocking;

	void Start () {
		
		currentHealth = health;
		currentStamina = stamina;
		healthBar = GameObject.FindGameObjectWithTag("HealthGUI");
		staminaBar = GameObject.FindGameObjectWithTag ("StaminaGUI");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(float damage){
		if (blocking == true && stamina > 0) {
			currentStamina -= damage;
			float cStamina = currentStamina / stamina;
			if (cStamina >= 0) {
				SetStaminaBar (cStamina);
			}
		} else {
			currentHealth -= damage;
			float cHealth = currentHealth / health;
		}

		if (currentHealth <= 0) {
			currentHealth = 0;
			GameManager.gameManager.DeathScreen ();
		}
		UpdateHealthBar ();
	}

	public void SetHealthBar(float health){
		healthBar.transform.GetChild(1).localScale = new Vector3 (health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}

	public void SetStaminaBar(float stamina){
		staminaBar.transform.GetChild(1).localScale = new Vector3 (stamina, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}

	public void UpdateHealthBar(){
		float cHealth = currentHealth / health;
		if (cHealth >= 0) {
			SetHealthBar (cHealth);
		}
		
	}
}
