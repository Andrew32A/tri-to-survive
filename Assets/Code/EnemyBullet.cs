using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public Rigidbody2D rb;

    void Start() {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {
        Debug.Log(hitInfo.name);
        Player player = hitInfo.GetComponent<Player>();
        if (player != null) {
            player.TakeDamage(damage);
            Destroy(gameObject);
        } else if (hitInfo.name == "BorderTop" || hitInfo.name == "BorderBottom" || hitInfo.name == "BorderLeft" || hitInfo.name == "BorderRight") {
            Destroy(gameObject);
        }
    }
}
