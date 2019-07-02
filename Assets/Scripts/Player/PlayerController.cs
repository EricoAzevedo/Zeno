using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour
{
	public float speed;
	private bool canMove;
	Vector3 movement;                   
	protected Rigidbody playerRigidbody;
	private int floorMask;                     
	float camRayLength = 100f;   
	public List<GameObject> interactableObjects;
	public GameObject Jornal;
	public GameObject InventoryGUI;

	public PlayerShield playerShield;
	public PlayerSword playerSword;
	public Animator swordAnim;
	public Animator shieldAnim;
	public bool attacking;
	public bool canInteract;



	void Awake ()
	{
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void Start(){


	}
	void Update(){

		if (!GameManager.gameManager.GamePaused) {
			canInteract = true;	
		} else {
			canInteract = false;
		}

		if (canMove) {
			if (Input.GetKeyDown (KeyCode.E) && canInteract) {
				Interact ();
			}

			if (Input.GetKeyDown (KeyCode.J)) {
				OpenJornal ();
			}

			if (Input.GetKeyDown (KeyCode.I)) {
				OpenInventory ();
			}

			if (Input.GetMouseButtonDown (0)) {

				Attack ();
			}

			if (Input.GetMouseButton (1)) {
				Block ();
			} else {
				StopBlock ();
			}

			CheckBlockAnimation ();
			CheckAttackAnimation ();
		}
	}

	void FixedUpdate ()
	{
		if (canMove == true) {
			floorMask = LayerMask.GetMask ("Floor");

			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");
//			if (Input.GetButton)

			Move (h, v);


			Turning ();
		}

		if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0){
			canMove = true;
		}


		//Animating (h, v);



	}

	void Move (float h, float v)
	{
		
		movement.Set (h, 0f, v);


		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning ()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);


		RaycastHit floorHit;


		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {


		
				Vector3 playerToMouse = floorHit.point - transform.position;


				playerToMouse.y = 0f;


				Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			if (canMove == true) {
				playerRigidbody.MoveRotation (newRotation);
			}
		}
	}

	void Animating (float h, float v)
	{
		
		bool walking = h != 0f || v != 0f;


		//anim.SetBool ("IsWalking", walking);
	}

	void Attack(){
		if (gameObject.GetComponent<PlayerStats> ().blocking == false && !GameManager.gameManager.GamePaused && playerSword.currentWeapon != null) {
			swordAnim.SetTrigger ("Attack");
		}
	}

	void Block(){
		if(playerShield.currentShield != null)
		shieldAnim.SetBool ("ShieldUp", true);
	}

	void StopBlock(){
		shieldAnim.SetBool ("ShieldUp", false);
	}

	void Interact(){

		interactableObjects.Clear ();
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f, 13,QueryTriggerInteraction.Collide);
		for (int i = 0; i < hitColliders.Length; i++) {
			if(hitColliders[i].gameObject.tag == "Interactable"){
			interactableObjects.Add (hitColliders [i].gameObject);
			}
		}
		if (interactableObjects.Count > 0) {
			Sort ();
			interactableObjects [0].SendMessage ("Interact");
		}
	}

	void Sort ()
	{
		interactableObjects.Sort (delegate (GameObject a, GameObject b) {
			return Vector3.Distance (transform.position, a.transform.position).CompareTo (Vector3.Distance (transform.position, b.transform.position));
		});
	}

	void OpenJornal(){
		if (Jornal.activeInHierarchy == true) {
			Jornal.SetActive (false);
		} else {
			Jornal.SetActive (true);
			Jornal.GetComponent<JornalManager>().SetUpJornal();
		}
		GameManager.gameManager.CheckUI ();

	}

	void OpenInventory(){
		if (InventoryGUI.activeInHierarchy == true) {
			InventoryGUI.SetActive (false);
		} else {
			InventoryGUI.SetActive (true);
			InventoryGUI.GetComponent<InventoryGUIManager>().SetUpInventory();
		}
		GameManager.gameManager.CheckUI ();
	}

	void CheckBlockAnimation(){
		if (shieldAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= shieldAnim.GetCurrentAnimatorStateInfo(0).length && shieldAnim.GetCurrentAnimatorStateInfo(0).IsName("Blocking"))
		{
			gameObject.GetComponent<PlayerStats>().blocking = true;
		} else {
			gameObject.GetComponent<PlayerStats>().blocking = false;
		}
	}

	void CheckAttackAnimation(){
		if (swordAnim.GetCurrentAnimatorStateInfo(0).IsName("AttackSword") && swordAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= swordAnim.GetCurrentAnimatorStateInfo(0).length)
		{
			attacking = true;
		} else {
			attacking = false;
		}
	}
}
	