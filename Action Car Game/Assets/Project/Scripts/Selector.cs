using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
	[SerializeField] GameObject[] cars;
	[SerializeField] GameObject[] weapons;

	[SerializeField] int currentCarIndex;
	[SerializeField] int currentWeaponIndex;

	[SerializeField] GameObject activeCar;

	private void Start() {
		ShowThisCar(currentCarIndex);
		ShowThisWeapon(currentWeaponIndex);
	}

	#region Cars

	public void ShowNextCar() {
		if(currentCarIndex == transform.childCount - 1 ) {
			currentCarIndex = 0;
		} else {
			currentCarIndex += 1;
		}
		ShowThisCar(currentCarIndex);
	}

	public void ShowPreviousCar() {
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
		GetActiveCar();
	}

	#endregion

	#region Weapons

	public void ShowNextWeapon() {
		if(currentWeaponIndex == activeCar.transform.GetChild(1).childCount - 1 ) {
			currentWeaponIndex = 0;
		} else {
			currentWeaponIndex += 1;
		}
		ShowThisWeapon(currentWeaponIndex);
	}

	public void ShowPreviousWeapon() {
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
	}

	private void GetActiveCar() {
		activeCar = FindObjectOfType<CarStat>().gameObject;
	}

	#endregion


}
