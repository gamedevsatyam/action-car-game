using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarSelectionUIManager : MonoBehaviour
{
	public event Action OnShowNextCar;
	public event Action OnShowPreviousCar;
	public event Action OnShowNextWeapon;
	public event Action OnShowPreviousWeapon;
	public event Action OnShowNextTimeLimit;
	public event Action OnShowPreviousTimeLimit;
	public event Action OnSaveData;

	[SerializeField] Selector selector;

	[SerializeField] TextMeshProUGUI carName, weaponName, timeLimit, statCarName;
	[SerializeField] Image maxHP, maxSpeed, handling;

	private void Start() {
		DisplayData();
	}

	private void DisplayData() {
		OnSaveData?.Invoke();
		carName.text = PlayerCarData.instance.carName;
		weaponName.text = PlayerCarData.instance.weaponName;
		timeLimit.text = PlayerCarData.instance.timeLimit;

		CarStat carStat = FindObjectOfType<CarStat>();
		statCarName.text = carName.text;
		maxHP.fillAmount = carStat.maxHp / 100;
		maxSpeed.fillAmount = carStat.maxSpeed / 30000;
		handling.fillAmount = carStat.handling;
	}

  public void ShowNextCar() {
		OnShowNextCar?.Invoke();
		DisplayData();
	}

	public void ShowPreviousCar() {
		OnShowPreviousCar?.Invoke();
		DisplayData();
	}

	public void ShowNextWeapon() {
		OnShowNextWeapon?.Invoke();
		DisplayData();
	}

	public void ShowPreviousWeapon() {
		OnShowPreviousWeapon?.Invoke();
		DisplayData();
	}

	public void ShowNextTimeLimit() {
		OnShowNextTimeLimit?.Invoke();
		DisplayData();
	}

	public void ShowPreviousTimeLimit() {
		OnShowPreviousTimeLimit?.Invoke();
		DisplayData();
	}

	public void StartGame() {
		SceneManager.LoadScene("GameScene");
	}
}
