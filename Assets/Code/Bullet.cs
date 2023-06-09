using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public Rigidbody2D rb;

    void Start() {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {
        Debug.Log(hitInfo.name);
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        } else if (hitInfo.name == "BorderTop" || hitInfo.name == "BorderBottom" || hitInfo.name == "BorderLeft" || hitInfo.name == "BorderRight") {
            Destroy(gameObject);
        }
    }
}
