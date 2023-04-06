using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smg : MonoBehaviour
{
    public Transform smgFirePoint;
    public GameObject smgBulletPrefab;
    public Animator smgFireSquish;

    public void Shoot()
    {
        Instantiate(smgBulletPrefab, smgFirePoint.position, smgFirePoint.rotation);
        smgFireSquish.SetTrigger("Fire");
    }
}