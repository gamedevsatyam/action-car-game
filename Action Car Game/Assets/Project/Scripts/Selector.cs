using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
	[SerializeField] CarSelectionUIManager carSelectionUIManager;

	[SerializeField] string carName;
	[SerializeField] string weaponName;
	[SerializeField] string timeLimit;

	private void Start() {
		carSelectionUIManager.OnShowNextCar += ShowNextCar;
		carSelectionUIManager.OnShowPreviousCar += ShowPreviousCar;
		carSelectionUIManager.OnShowNextWeapon += ShowNextWeapon;
		carSelectionUIManager.OnShowPreviousWeapon += ShowPreviousWeapon;
		carSelectionUIManager.OnShowNextTimeLimit += ShowNextTimeLimit;
		carSelectionUIManager.OnShowPreviousTimeLimit += ShowPreviousTimeLimit;

		ShowThisCar(currentCarIndex);
		ShowThisWeapon(currentWeaponIndex);
		TimerCheck();
	}

	#region Cars

	[SerializeField] int currentCarIndex;
	[SerializeField] GameObject activeCar;

	private void ShowNextCar() {
		if(currentCarIndex == transform.childCount - 1 ) {
			currentCarIndex = 0;
		} else {
			currentCarIndex += 1;
		}
		ShowThisCar(currentCarIndex);
	}

	private void ShowPreviousCar() {
		if(currentCarIndex == 0 ) {
			currentCarIndex = transform.childCount - 1;
		} else {
			currentCarIndex -= 1;
		}
		ShowThisCar(currentCarIndex);
	}

  private void ShowThisCar(int index) {
		for(int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(i == index);
		}
		activeCar = FindObjectOfType<CarStat>().gameObject;
		carName = activeCar.name;
		ShowThisWeapon(currentWeaponIndex);
	}

	#endregion

	#region Weapons

	[SerializeField] int currentWeaponIndex;
	[SerializeField] GameObject activeWeapon;

	private void ShowNextWeapon() {
		if(currentWeaponIndex == activeCar.transform.GetChild(1).childCount - 1 ) {
			currentWeaponIndex = 0;
		} else {
			currentWeaponIndex += 1;
		}
		ShowThisWeapon(currentWeaponIndex);
	}

	private void ShowPreviousWeapon() {
		if(currentWeaponIndex == 0 ) {
			currentWeaponIndex = activeCar.transform.GetChild(1).childCount - 1;
		} else {
			currentWeaponIndex -= 1;
		}
		ShowThisWeapon(currentWeaponIndex);
	}

  private void ShowThisWeapon(int index) {
		for(int i = 0; i < activeCar.transform.GetChild(1).childCount; i++) {
			activeCar.transform.GetChild(1).GetChild(i).gameObject.SetActive(i == index);
		}
		activeWeapon = FindObjectOfType<WeaponStat>().gameObject;
		weaponName = activeWeapon.name;
	}



	#endregion

	#region TimeLimit

	[SerializeField] int count;

	private enum TimeLimits {
		NoLimit,
		HalfMinute,
		FullMinute
	}

	private TimeLimits timer;

	private void ShowNextTimeLimit() {
		if(count < 2) {
			count +=1;
			timer++;
			TimerCheck();
		}
	}

	private void ShowPreviousTimeLimit() {
		if(count > 0) {
			count -= 1;
			timer--;
			TimerCheck();
		}
	}

	private void TimerCheck() {
		switch (timer)
		{
			case TimeLimits.NoLimit:
				timeLimit = "No Limit";
				break;
			case TimeLimits.HalfMinute:
				timeLimit = "30 Seconds";
				break;
			case TimeLimits.FullMinute:
				timeLimit = "60 Seconds";
				break;
			default:
				timeLimit = "No Limit";
				break;
		}

	}


	#endregion

public void SaveData() {
	PlayerCarData.instance.carName = carName;
	PlayerCarData.instance.weaponName = weaponName;
	PlayerCarData.instance.timeLimit =timeLimit;
}

private void OnDestroy() {
	SaveData();
	carSelectionUIManager.OnShowNextCar -= ShowNextCar;
	carSelectionUIManager.OnShowPreviousCar -= ShowPreviousCar;
	carSelectionUIManager.OnShowNextWeapon -= ShowNextWeapon;
	carSelectionUIManager.OnShowPreviousWeapon -= ShowPreviousWeapon;
	carSelectionUIManager.OnShowNextTimeLimit -= ShowNextTimeLimit;
	carSelectionUIManager.OnShowPreviousTimeLimit -= ShowPreviousTimeLimit;
}

}
