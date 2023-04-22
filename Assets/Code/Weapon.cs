using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// menu of functions that it's expecting the class to have, polymorphism
public interface IWeapon {
    void Shoot();
}

public class Weapon : MonoBehaviour
{
    public IWeapon currentWeapon;
    public GameObject pistolScript;
    public GameObject smgScript;
    public GameObject shotgunScript;
    public GameObject godGunScript;

    public bool canInput;

    void Start() {
        // set pistol as default weapon
        currentWeapon = EquipPistol();

        // enable shooting
        canInput = true;
    }

    void Update() {
        if (canInput == true) {
            PlayerInput();
        }
    }

    public void PlayerInput() {
        // fire1 == mouse 0 (left click)
        if (Input.GetButton("Fire1")) {
            currentWeapon.Shoot();
        } else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            currentWeapon = EquipPistol();
            Debug.Log("pistol equipped");
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            currentWeapon = EquipSmg();
            Debug.Log("smg equipped");
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            currentWeapon = EquipShotgun();
            Debug.Log("shotgun equipped");
        } else if (Input.GetKeyDown(KeyCode.Alpha9)) {
            currentWeapon = EquipGodGun();
            Debug.Log("god gun equipped");
        }
    }

    // public void ChangeWeapon(GameObject newWeaponObject) {
    //     currentWeapon = newWeaponObject.GetComponent<IWeapon>();
    // }

    public IWeapon EquipPistol() {
        return pistolScript.GetComponent<IWeapon>();
    }

    public IWeapon EquipSmg() {
        return smgScript.GetComponent<IWeapon>();
    }

    public IWeapon EquipShotgun() {
        return shotgunScript.GetComponent<IWeapon>();
    }

    public IWeapon EquipGodGun() {
        return godGunScript.GetComponent<IWeapon>();
    }
}
