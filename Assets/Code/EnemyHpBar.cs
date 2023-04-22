using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    // high == 80 hp, medium == 60 hp, low == 40 hp, critical == 20 hp
    public GameObject high;
    public GameObject medium;
    public GameObject low;
    public GameObject critical;

    public void UpdateHpBar(int hp) {        
        if (hp <= 20) {
            Destroy(critical);
        } else if (hp <= 40) {
            Destroy(low);
        } else if (hp <= 60) {
            Destroy(medium);
        } else if (hp <= 80) {
            Destroy(high);
        }
    }
}
