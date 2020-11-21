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
    private Animator anim;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private int healthPoint = 10;

    private void Start()
    {
        //ici on Get le Rigidbody2D dans le start
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");

        spawner = GameObject.FindWithTag("Spawn");

        rbPlayer = player.GetComponent<Rigidbody2D>();

        spawn = spawner.GetComponent<Spawn>();
 

        anim = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {

        var playerDirection = (rbPlayer.position - rb.position).normalized;

        //https://www.youtube.com/watch?v=4Wh22ynlLyk&ab_channel=PressStart 

        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;


        if (Mathf.Abs(rb.velocity.magnitude) <= maxSpeed)
        {
            rb.AddForce(enemySpeed * playerDirection);

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //healthPoint -= 1; pareil avec ++

        if (collision.CompareTag("Spawn"))
        {
            return;
        }

        healthPoint--;

        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        spawn.EnemyDestroyed();
    }

}
