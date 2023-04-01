using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
   public float attractionForce = 5f;
    public float maxDistance = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private CircleCollider2D coinCollider;

    private void Start() {
        // get a reference to the player's transform
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // get a reference to the coin's rigidbody component
        rb = GetComponent<Rigidbody2D>();

        // get a reference to the coin's circle collider
        coinCollider = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate() {
        // calculate the distance between the coin and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // check if the player is inside the coin's collider
        if (distance < coinCollider.radius && coinCollider.enabled)
        {
            // calculate the direction towards the player
            Vector3 direction = player.position - transform.position;

            // apply an attraction force towards the player
            rb.AddForce(direction.normalized * attractionForce);

            // clamp the velocity to a maximum distance
            if (rb.velocity.magnitude > maxDistance)
            {
                rb.velocity = rb.velocity.normalized * maxDistance;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // check if the coin collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // destroy the coin
            Destroy(gameObject);
        }
    }
}
