using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsBehavior : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool shouldStop = false;


    private void Start()
    {
        //ici on Get le Rigidbody2D dans le start
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //quand l'Asteroid va "Entrer en collision" avec un autre objet, le bool shouldStop va passer true
        if(collision.gameObject.tag == "PlayerBullet")
        {
            shouldStop = true;
        }
    }

    private void FixedUpdate()
    {
        //On précise ici que si shouldStop est true, la velocity de l'asteroid passe à zéro
        if (shouldStop)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
