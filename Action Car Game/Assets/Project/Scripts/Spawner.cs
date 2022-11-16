using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  [SerializeField] GameObject[] objectsToSpawn;
	[SerializeField] Transform spawnPoint;

	[SerializeField] UIManager uiManger;

	private void Start() {
		uiManger = FindObjectOfType<UIManager>();

		float  y = Random.Range(0,360);
		GameObject spawnedObject = Instantiate(objectsToSpawn[Random.Range(0,objectsToSpawn.Length)], spawnPoint.position, Quaternion.identity);
		spawnedObject.transform.parent = spawnPoint;
		if(spawnedObject.GetComponent<WeaponStat>() == true) {
			Destroy(GetComponent<CrateScript>());
			transform.tag = "Enemy";
			transform.GetComponent<EnemyScript>().enabled = true;
			uiManger.enemies.Add(transform.gameObject);
		} else if(spawnedObject.name == "TEMP" || spawnedObject.name == "TEMP(Clone)"){
			transform.tag = "Obstacle";
			Destroy(GetComponent<EnemyScript>());
			Destroy(GetComponent<SphereCollider>());
			Destroy(GetComponent<CrateScript>());
		} else {
			transform.tag = "Crate";
			transform.GetComponent<CrateScript>().enabled = true;
			Destroy(GetComponent<EnemyScript>());
			Destroy(GetComponent<SphereCollider>());
		}
	}

	private void OnDestroy() {
		uiManger.enemies.Remove(transform.gameObject);
	}
}
