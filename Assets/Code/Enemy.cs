using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string EnemyType;
    public int maxHealth = 100;
    private int currentHealth;
    public EnemyHpBar HpBar;
    public int rotationOffset = 90;
    private float timeSinceLastFire = 0f;
    public float fireRate = 1.5f;
    public float movementSpeed = 5f;

    public GameObject coinPrefab;
    public float dropRadius = 0.5f;
    public int numCoins = 5;

    public Transform player;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator EnemyFireSquish;

    // TODO: add death explosion here and in Die method
    // public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (EnemyType == "chaser") {
            movementSpeed = 3f;
        } else if (EnemyType == "gunner") {
            movementSpeed = 0.5f;
        }
    }

    void Update()
    {
        if (player != null) {
            // look at player
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationOffset;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // move towards player
            transform.position += direction.normalized * movementSpeed * Time.deltaTime;
        }

        if (EnemyType == "gunner") {
            Gunner();
        }
    }

    private void Gunner() {
        if (timeSinceLastFire >= fireRate) {
            // play shoot animation squish
            EnemyFireSquish.SetTrigger("Fire");

            // spawn enemy bullet prefab
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            timeSinceLastFire = 0f;
        } else {
            timeSinceLastFire += Time.deltaTime;
        }

    }

    public void TakeDamage(int damage) {
        // update hp
        currentHealth -= damage;
        HpBar.UpdateHpBar(currentHealth);

        // check if enemy died
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        // TODO: add death explosion
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        // drop coins
        for (int i = 0; i < numCoins; i++) {
            Vector2 randomPosition = Random.insideUnitCircle.normalized * dropRadius;
            Instantiate(coinPrefab, transform.position + new Vector3(randomPosition.x, randomPosition.y, 0f), Quaternion.identity);
        }
    }
}
