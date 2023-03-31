using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    private int currentHealth;

    // TODO: add death explosion here and in Die method
    // public GameObject deathEffect;

    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        // TODO: add death explosion
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
