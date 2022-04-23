using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] float rotateSpeed;

  private void Update() {
		transform.Rotate(new Vector3(0,1,0) * Time.deltaTime * rotateSpeed);
	}
}
