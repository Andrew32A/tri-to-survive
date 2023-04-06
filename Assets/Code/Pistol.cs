using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Transform pistolFirePoint;
    public GameObject pistolBulletPrefab;
    public Animator pistolFireSquish;

    private bool canShoot = true;
    private float shootDelay = 0.2f;

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
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
