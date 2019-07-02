using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;


	Vector3 offset;
	// Use this for initialization
	void Start () {

		offset = transform.position - target.position;

	}
	void Update () {
			Vector3 targetCamPos = target.position + offset;
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.fixedDeltaTime);		
	}
}
