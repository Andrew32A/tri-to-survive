using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IWeapon
{
    public Transform shotgunFirePoint;
    public GameObject shotgunBulletPrefab;
    public Animator shotgunFireSquish;

    public float bulletSpread = 40f;
    public float pellets = 8;
    private bool canShoot = true;
    public float fireRate = 0.5f;

    public void Shoot() {
        if (canShoot)
        {
            for (int i = 0; i < pellets; i++)
            {
                float randomSpread = Random.Range(-bulletSpread / 2f, bulletSpread / 2f);
                Quaternion spreadRotation = Quaternion.Euler(0f, 0f, randomSpread);
                Instantiate(shotgunBulletPrefab, shotgunFirePoint.position, shotgunFirePoint.rotation * spreadRotation);
            }
            shotgunFireSquish.SetTrigger("Fire");
            canShoot = false;
            StartCoroutine(ResetShootDelay());
        }
    }

    private IEnumerator ResetShootDelay() {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
