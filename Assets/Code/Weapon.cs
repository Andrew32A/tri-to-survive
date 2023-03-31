using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator FireSquish;

    void Update()
    {
        // fire1 == mouse 0 (left click)
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
            FireSquish.SetTrigger("Fire");
        }
    }

    void Shoot() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
