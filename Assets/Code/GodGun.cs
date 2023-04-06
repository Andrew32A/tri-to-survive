using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGun : MonoBehaviour, IWeapon
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator fireSquish;

    public float bulletSpread = 20f;
    private bool canShoot = true;
    public float fireRate = 0f;

    public void Shoot()
    {
        if (canShoot)
        {
            float randomSpread = Random.Range(-bulletSpread / 2f, bulletSpread / 2f);
            Quaternion spreadRotation = Quaternion.Euler(0f, 0f, randomSpread);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * spreadRotation);
            fireSquish.SetTrigger("Fire");
            canShoot = false;
            StartCoroutine(ResetShootDelay());
        }
    }

    private IEnumerator ResetShootDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
