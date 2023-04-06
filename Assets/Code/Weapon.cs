using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Pistol currentWeapon;
    public GameObject pistolScript;

    void Start()
    {
        currentWeapon = pistolScript.GetComponent<Pistol>();
    }

    void Update()
    {
        // fire1 == mouse 0 (left click)
        if (Input.GetButton("Fire1")) {
            currentWeapon.Shoot();
        }
    }

    public void EquipPistol() {
        currentWeapon = pistolScript.GetComponent<Pistol>();
    }
}