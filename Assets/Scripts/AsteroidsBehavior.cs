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

    private Animator anim;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float maxSpeed;

    private static int enemyNumber;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnHit()
    {

        anim.SetTrigger("hit");

        ChangeSpeed(0.1f);
    }

    private void OnDestroy()
    {
        enemyNumber--;

        if(enemyNumber <= 0)
        {
            GameObject.FindGameObjectWithTag("GameManagere").GetComponent<ChangeLevel>().ChangeScene();
        }
    }

    private void Start()
    {
        enemyNumber++;

        GetComponent<HPBehavior>().onHit.AddListener(OnHit);

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

    private void ChangeSpeed(float speed)
    {
        rb.velocity *= speed;
    }

}
