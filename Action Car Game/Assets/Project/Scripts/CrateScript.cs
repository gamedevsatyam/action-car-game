using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
	public int count;

  private void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == "PlayerBullet") {
			count += 1;
			if(count == Random.Range(1,4) || count > 3){
				PlayerController pc = FindObjectOfType<PlayerController>();
				if(pc.maxAmmo < pc.MaximumAmmoToHold) {
					pc.maxAmmo += Random.Range(5, 11);
				} 
				if(pc.maxHp < pc.MaximumHpToHold) {
					pc.maxHp += Random.Range(10, 16);
				}
				Destroy(this.gameObject);
			}
			Destroy(other.gameObject);
		}
	}
}
