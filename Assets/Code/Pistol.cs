using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    public Transform pistolFirePoint;
    public GameObject pistolBulletPrefab;
    public Animator pistolFireSquish;

    public float fireRate = 0.2f;
    private bool canShoot = true;

    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1")) {
            if (canShoot)
            {
                Instantiate(pistolBulletPrefab, pistolFirePoint.position, pistolFirePoint.rotation);
                pistolFireSquish.SetTrigger("Fire");
                canShoot = false;
                StartCoroutine(ResetShootDelay());
            }
        }
    }

    private IEnumerator ResetShootDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
