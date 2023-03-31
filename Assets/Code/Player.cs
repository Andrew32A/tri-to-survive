using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 75;
    private int currentHealth;
    public PlayerHpBar HpBar;
    public float moveSpeed = 8.5f;
    public int rotationOffset = 90;

    private Vector2 movement;
    private Vector2 mousePos;

    private Camera cam;
    private Rigidbody2D rb;

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

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
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
