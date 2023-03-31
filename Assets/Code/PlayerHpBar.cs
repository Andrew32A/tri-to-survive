using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpBar : MonoBehaviour
{
    // high == 75 hp, medium == 50 hp, low == 25 hp
    public GameObject high;
    public GameObject medium;
    public GameObject low;

    public void UpdateHpBar(int hp) {
        if (hp <= 25) {
            Destroy(low);
        } else if (hp <= 50) {
            Destroy(medium);
        } else if (hp <= 75) {
            Destroy(high);
        }
    }
}
