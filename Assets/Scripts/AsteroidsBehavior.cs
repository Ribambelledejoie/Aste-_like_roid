using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsBehavior : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool shouldStop = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            shouldStop = true;
        }
    }

    private void FixedUpdate()
    {
        if (shouldStop)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
