using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject[] UIScreens;
	[SerializeField] PlayerCarData playerCarData;
	[SerializeField] PlayerController playerController;

	[SerializeField] bool startTimer;
	[SerializeField] float timer;
	[SerializeField] TextMeshProUGUI timerText;

	[SerializeField] Image HP;
	[SerializeField] TextMeshProUGUI ammoText;

	public List<GameObject> enemies;

	private void Start() {
		SetTimer();
		SetScreen("InGameUI");
		startTimer = true;
		Invoke("SetHP", 0.1f);
	}

	private void Update() {
		SetHP();
		if(startTimer && timer != 0) {
			timer -= Time.deltaTime;
			timerText.text = timer.ToString("0");
			if(timer <= 0) {
				timer = 0;
				SetScreen("GameOverUI");
			}
		}

		if(enemies.Count <= 0) {
			SetScreen("GameWinUI");
		}
	}

	private void SetHP() {
		playerController = FindObjectOfType<PlayerController>();
		HP.fillAmount = playerController.maxHp/ 100;
		if(HP.fillAmount <= 0) {
			SetScreen("GameOverUI");
		}
		ammoText.text = playerController.maxAmmo.ToString();
		// Debug.Log(playerController.handling);
	}

  public void PauseGame() {
		Time.timeScale = 0;
		SetScreen("PauseUI");
	}

	public void ResumeGame() {
		Time.timeScale = 1;
		SetScreen("InGameUI");
	}

	public void BackToMainMenu() {
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}

	private void SetTimer() {
		playerCarData = FindObjectOfType<PlayerCarData>();
		if(playerCarData.timeLimit == "60 Seconds") {
			timer = 60;
		} else if(playerCarData.timeLimit == "30 Seconds") {
			timer = 30;
		} else {
			timer = 0;
		} 
	}

	public void SetScreen(string screenToActivate) {
		foreach (GameObject screen in UIScreens) {
			if(screenToActivate == screen.name) {
				screen.gameObject.SetActive(true);
			} else {
				screen.gameObject.SetActive(false);
			}
		}
	}



}
