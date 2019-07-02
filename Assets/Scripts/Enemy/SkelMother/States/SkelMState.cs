using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkelMState {

	void UpdateState();

	void OnTriggerEnter (Collider other);

	void OnTriggerStay (Collider other);

	void OnTriggerExit (Collider other);

	void ToAttackingState();

	void ToNeutralState();

	void ToAggresiveState();

}