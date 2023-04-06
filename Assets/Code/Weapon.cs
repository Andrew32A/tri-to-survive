using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Smg currentWeapon;
    // public GameObject pistolScript;
    public GameObject smgScript;

    void Start()
    {
        currentWeapon = EquipSmg();
    }

    void Update()
    {
        // fire1 == mouse 0 (left click)
        if (Input.GetButton("Fire1")) {
            currentWeapon.Shoot();
        }
    }

    // public Pistol EquipPistol() {
    //     return pistolScript.GetComponent<Pistol>();
    // }
    
    public Smg EquipSmg() {
        return smgScript.GetComponent<Smg>();
    }
}
