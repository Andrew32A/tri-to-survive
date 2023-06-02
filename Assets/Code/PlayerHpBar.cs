using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpBar : MonoBehaviour
{
    // high == 3 hp, medium == 2 hp, low == 1 hp
    public GameObject high;
    public GameObject medium;
    public GameObject low;

    public void UpdateHpBar(int hp) {
        // TODO: add an explosion animation with unity's particle system and screenshake

        if (hp < 1) {
            low.SetActive(false);
        } else if (hp < 2) {
            medium.SetActive(false);
        } else if (hp < 3) {
            high.SetActive(false);
        }
    }

    public void resetHpBar(int hp) {
        if (hp <= 1) {
            low.SetActive(true);
        } else if (hp <= 2) {
            low.SetActive(true);
            medium.SetActive(true);
        } else if (hp <= 3) {
            low.SetActive(true);
            medium.SetActive(true);
            high.SetActive(true);
        }
    }
}
