using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface SkelState {

	void UpdateState();

	void OnTriggerEnter (Collider other);

	void OnTriggerStay (Collider other);

	void OnTriggerExit (Collider other);

	void ToAttackState();

	void ToNeutralState();

}