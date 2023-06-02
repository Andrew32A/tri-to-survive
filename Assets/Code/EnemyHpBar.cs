using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    // high == 4 hp, medium == 3 hp, low == 2 hp, critical == 1 hp
    public GameObject high;
    public GameObject medium;
    public GameObject low;
    public GameObject critical;

    public void UpdateHpBar(int hp) {        
        if (hp <= 1) {
            Destroy(critical);
        } else if (hp <= 2) {
            Destroy(low);
        } else if (hp <= 3) {
            Destroy(medium);
        } else if (hp <= 4) {
            Destroy(high);
        }
    }
}
