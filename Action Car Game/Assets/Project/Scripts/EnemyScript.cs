using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	[SerializeField] WeaponStat weaponStat;
	[SerializeField] GameObject weaponBodyToRotate;
  private int maxHp = 10;

	private GameObject bullet;
	
	public bool canShoot;
	public float fireRate;
	public int damage;

	private bool startShooting;
	private bool playerDetected;

	private void Start() {
		startShooting = false;
		weaponStat = transform.GetChild(0).GetComponentInChildren<WeaponStat>();
		weaponBodyToRotate = weaponStat.transform.GetChild(0).GetChild(0).gameObject;
		canShoot = true;
		fireRate = weaponStat.fireRate;
		damage = weaponStat.damage;
	}

	private void Update() {
		if(maxHp <= 0) {
			Destroy(this.gameObject);
		}

		if(!canShoot) {
			fireRate -= Time.deltaTime;
			if(fireRate < 0) {
				canShoot = true;
				fireRate = weaponStat.fireRate;
			}
		}

		if(startShooting){
			Shoot();
		}

		if(playerDetected) {
			LookTowardsPlayer();
		}
	}

	private void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == "PlayerBullet") {
			maxHp -= other.gameObject.GetComponent<BulletScript>().damage;
			Destroy(other.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			Debug.Log(transform.position - other.transform.position);
			playerDetected = true;
			startShooting = true;
		}	
	}

	private void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "Player") {
			playerDetected = false;
			startShooting = false;
		}	
	}

	public void LookTowardsPlayer() {
		Vector3 directionToFace = FindObjectOfType<PlayerController>().transform.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(directionToFace);
		Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
		transform.rotation = Quaternion.Euler(0, rotation.y, 0);
	}

	public void Shoot() {
		if(canShoot) {
			canShoot = false;
			bullet = Instantiate(weaponStat.bullet, weaponStat.bulletPoint.transform.position, Quaternion.identity);
			bullet.gameObject.GetComponent<BulletScript>().damage = damage;
			bullet.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
			bullet.tag = "EnemyBullet";
		}
	}
}
