using System;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionUIManager : MonoBehaviour
{
	public event Action OnShowNextCar;
	public event Action OnShowPreviousCar;
	public event Action OnShowNextWeapon;
	public event Action OnShowPreviousWeapon;
	public event Action OnShowNextTimeLimit;
	public event Action OnShowPreviousTimeLimit;



	private void Start() {
		
	}

  public void ShowNextCar() {
		OnShowNextCar?.Invoke();
	}

	public void ShowPreviousCar() {
		OnShowPreviousCar?.Invoke();
	}

	public void ShowNextWeapon() {
		OnShowNextWeapon?.Invoke();
	}

	public void ShowPreviousWeapon() {
		OnShowPreviousWeapon?.Invoke();
	}

	public void ShowNextTimeLimit() {
		OnShowNextTimeLimit?.Invoke();
	}

	public void ShowPreviousTimeLimit() {
		OnShowPreviousTimeLimit?.Invoke();
	}
}
