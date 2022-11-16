using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
	public event Action OnGotHit;

	[SerializeField] Joystick joystick;
  [SerializeField] Rigidbody carRigidbody;
	[SerializeField] CarStat car;
	[SerializeField] WeaponStat weapon;

	private bool isAccelerating;
	private bool isDeAccelerating;
	public float force, acceleration = 3, maxSpeed, maxHp, handling;

	private GameObject playerWeapon;
	private GameObject playerCar;
	private GameObject bullet;

	public bool canShoot;
	public bool isShooting;
	public float firerate;
	public int maxAmmo;
	public int damage;

	public int MaximumAmmoToHold;
	public float MaximumHpToHold;

	private void Start() {
		GetPlayerData();
		SetPlayerValues();
		SetWeaponValues();
		MaximumAmmoToHold = maxAmmo;
		MaximumHpToHold = maxHp;
	}

	private void Update() {
		transform.position = carRigidbody.transform.position;
		force = 0f;
		if(isAccelerating) {
			force = acceleration * maxSpeed;
		} if(isDeAccelerating) {
			force = acceleration * -maxSpeed;
		}

		if(!canShoot) {
			firerate -= Time.deltaTime;
			if(firerate < 0) {
				canShoot = true;
				firerate = weapon.fireRate;
			}
		}

		if(maxHp <= 0) {
			Destroy(this.gameObject);
		}

		if(isShooting) {
			Shoot();
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

	public void DeAccelerate(bool test) {
		isDeAccelerating = test;
	}

	private void Rotation() {
		Vector3 rotationAngle = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);
		Quaternion lookRotation = Quaternion.LookRotation(rotationAngle);
		if (joystick.Horizontal != 0 || joystick.Vertical != 0)
		{
			Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * handling).eulerAngles;
			if(force > 0) {
				transform.rotation = Quaternion.Euler(0, rotation.y, 0);
			}
		}
	}

	private void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == "EnemyBullet") {
			maxHp -= other.gameObject.GetComponent<BulletScript>().damage;
			Destroy(other.gameObject);
			OnGotHit?.Invoke();
		}
	}

	public void StartShoot(bool test) {
		isShooting = test;
	}

	public void Shoot() {
		if(canShoot && maxAmmo > 0) {
			canShoot = false;
			maxAmmo--;
			bullet = Instantiate(weapon.bullet, weapon.bulletPoint.transform.position, Quaternion.Euler(0,transform.forward.y,0));
			bullet.gameObject.GetComponent<BulletScript>().damage = damage;
			bullet.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
			bullet.tag = "PlayerBullet";
		}
	}

	private void SetPlayerValues() {
		car = playerCar.GetComponent<CarStat>();
		maxSpeed = car.maxSpeed;
		maxHp = car.maxHp;
		handling = car.handling;
		acceleration = 3;
		carRigidbody.transform.parent = null;
	}

	private void SetWeaponValues() {
		weapon = playerWeapon.GetComponent<WeaponStat>();
		firerate = weapon.fireRate;
		maxAmmo = weapon.maxAmmo;
		damage = weapon.damage;
	}

	private void GetPlayerData() {
		for (int i = 0; i < transform.childCount - 1; i++) {
			if (transform.GetChild(i).gameObject.name == PlayerCarData.instance.carName) {
				playerCar = transform.GetChild(i).gameObject;
				playerCar.SetActive(true);
				for (int j = 0; j < playerCar.transform.GetChild(1).childCount; j++) {
					if(playerCar.transform.GetChild(1).GetChild(j).gameObject.name == PlayerCarData.instance.weaponName) {
						playerWeapon = playerCar.transform.GetChild(1).GetChild(j).gameObject;
						playerWeapon.SetActive(true);
					}
				}
			}
		}
	}

}
