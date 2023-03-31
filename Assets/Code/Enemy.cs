using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public EnemyHpBar HpBar;
    public int rotationOffset = 90;
    private float timeSinceLastFire = 0f;
    public float fireRate = 1.5f;

    public Player player;
    public Transform firePoint;
    public GameObject bulletPrefab;

    // TODO: add death explosion here and in Die method
    // public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player != null) {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationOffset;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (timeSinceLastFire >= fireRate) {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            timeSinceLastFire = 0f;
        } else {
            timeSinceLastFire += Time.deltaTime;
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        HpBar.UpdateHpBar(currentHealth);
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
