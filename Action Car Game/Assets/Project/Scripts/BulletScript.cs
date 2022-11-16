using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public int damage;
	[SerializeField] Rigidbody rb;
	[SerializeField] BoxCollider bcollider;

	private void Start() {
		Destroy(this.gameObject, 2);
		//Time.timeScale = 0;
/* 		rb = GetComponent<Rigidbody>();
		rb.AddForce(transform.forward *50, ForceMode.Impulse) ; */

		bcollider = GetComponent<BoxCollider>();
	}

  private void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Time.timeScale = 1;
		}
	}

	private void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") {
			bcollider.isTrigger = false;
		}
	}

}
