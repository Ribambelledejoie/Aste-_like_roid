using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 1 - récupérer la position du joueur : récupérer joueur + position
/// </summary>
public class AsteroidsBehavior : MonoBehaviour
{

    private Rigidbody2D rb;
    private GameObject player;
    private Rigidbody2D rbPlayer;

    public float enemySpeed;
    public float maxSpeed;


    private void Start()
    {

        //ici on Get le Rigidbody2D dans le start
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");

        rbPlayer = player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //https://www.youtube.com/watch?v=4Wh22ynlLyk&ab_channel=PressStart 

        var playerDirection = (rbPlayer.position - rb.position).normalized;

        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;


        if (Mathf.Abs(rb.velocity.magnitude) <= maxSpeed)
        {
            rb.AddForce(enemySpeed * playerDirection);

        }
    }

    public void ChangeSpeed(float speed)
    {
        rb.velocity *= speed;
    }

}
