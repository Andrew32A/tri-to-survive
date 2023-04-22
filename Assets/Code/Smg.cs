using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smg : MonoBehaviour, IWeapon
{
    public Transform smgFirePoint;
    public GameObject smgBulletPrefab;
    public Animator smgFireSquish;

    public float bulletSpread = 40f;
    private bool canShoot = true;
    public float fireRate = 0.08f;

    public void Shoot() {
        if (canShoot)
        {
            float randomSpread = Random.Range(-bulletSpread / 2f, bulletSpread / 2f);
            Quaternion spreadRotation = Quaternion.Euler(0f, 0f, randomSpread);
            Instantiate(smgBulletPrefab, smgFirePoint.position, smgFirePoint.rotation * spreadRotation);
            smgFireSquish.SetTrigger("Fire");
            canShoot = false;
            StartCoroutine(ResetShootDelay());
        }
    }

    private IEnumerator ResetShootDelay() {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}