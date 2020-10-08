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
    private GameObject spawner;
    private Spawn spawn;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float maxSpeed;

    private void Start()
    {
        //ici on Get le Rigidbody2D dans le start
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");

        spawner = GameObject.FindWithTag("Spawn");

        rbPlayer = player.GetComponent<Rigidbody2D>();

        spawn = spawner.GetComponent<Spawn>();
        
    }

    private void FixedUpdate()
    {
        //On précise ici que si shouldStop est true, la velocity de l'asteroid passe à zéro

        var playerDirection = (rbPlayer.position - rb.position).normalized;

        if (Mathf.Abs(rb.velocity.magnitude) <= maxSpeed)
        {
            rb.AddForce(enemySpeed * playerDirection);

        }


    }

    private void OnDestroy()
    {
        spawn.EnemyDestroyed();
    }

}
