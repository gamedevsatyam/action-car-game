using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
	[SerializeField] Joystick joystick;
  [SerializeField] Rigidbody carRigidbody;

	private bool isAccelerating;
	[SerializeField] float acceleration, maxSpeed, turnStrength, force;

	private void Start() {
		carRigidbody.transform.parent = null;
		acceleration = 3;
		// maxSpeed = 1200;
	}

	private void Update() {
		transform.position = carRigidbody.transform.position;
		force = 0f;
		if(isAccelerating) {
			force = acceleration * maxSpeed;
		}


// For Testing on Desktop
		if(Input.GetKeyDown(KeyCode.Space)) {
			Accelerate(true);
		} else if(Input.GetKeyUp(KeyCode.Space)) {
			Accelerate(false);
		}
	}

	private void FixedUpdate() {
		// Movement
		carRigidbody.AddForce(transform.forward * force);

		Rotation();
	}

	public void Accelerate(bool test) {
		isAccelerating = test;
	}

	public void Brake() {
		carRigidbody.Sleep();
	}

	private void Rotation() {
		Vector3 rotationAngle = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);
		Quaternion lookRotation = Quaternion.LookRotation(rotationAngle);
		if (joystick.Horizontal != 0 || joystick.Vertical != 0)
		{
			Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnStrength).eulerAngles;
			if(force > 0) {
				transform.rotation = Quaternion.Euler(0, rotation.y, 0);
			}
		}
	}
}
