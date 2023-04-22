using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 75;
    private int currentHealth;
    public PlayerHpBar HpBar;
    public float moveSpeed = 12f;
    public int rotationOffset = 90;

    private Vector2 movement;
    private Vector2 mousePos;

    private Camera cam;
    private Rigidbody2D rb;

    public TimeManager timeManager;

    void Start()
    {
        currentHealth = maxHealth;
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // normalize vector so player doesn't move faster than intended diagonally
        if (movement.magnitude > 1f) { 
            movement.Normalize();
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // trigger bullet time
        if (Input.GetButtonDown("Fire2")) {
            timeManager.BulletTime();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - rotationOffset;

        rb.rotation = angle;
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
