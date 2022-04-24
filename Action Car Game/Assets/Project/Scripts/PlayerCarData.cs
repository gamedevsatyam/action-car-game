using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCarData : MonoBehaviour
{
	public static PlayerCarData instance;

	public string carName;
	public string weaponName;
	public string timeLimit;

  private void Awake() {
		instance = this;
		DontDestroyOnLoad(this);
	}
}
