using UnityEngine;
using System.Collections;
using System;

public class GunController : MonoBehaviour {
    public Transform weaponHold;
    public Gun startingGun;
    private Gun equipedGun;

	// Use this for initialization
	void Start () {
	    if(startingGun != null) {
            EquipGun(startingGun);
        }
	}

    public void EquipGun(Gun gun) {
        if(equipedGun != null) {
            Destroy(equipedGun.gameObject);
        }
        equipedGun = Instantiate(gun, weaponHold.position, weaponHold.rotation) as Gun;
        equipedGun.transform.parent = weaponHold;
    }

    public void OnTriggerHold() {
        if(equipedGun != null) {
            equipedGun.OnTriggerHold();
        }
    }

    public void OnTriggerRelease() {
        if (equipedGun != null) {
            equipedGun.OnTriggerRelease();
        }
    }
}
