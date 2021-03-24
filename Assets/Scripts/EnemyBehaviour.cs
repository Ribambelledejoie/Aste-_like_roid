using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private static int enemyNumber;

    protected Rigidbody2D rb;
    protected GameObject player;
    protected Rigidbody2D rbPlayer;
    protected Vector2 distanceEP;

    private Animator anim;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float maxSpeed;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnHit()
    {
        anim.SetTrigger("hit");

        ChangeSpeed(0.1f);
    }

    private void ChangeSpeed(float speed)
    {
        rb.velocity *= speed;
    }

    private void Start()
    {
        enemyNumber++;

        GetComponent<HPBehavior>().onHit.AddListener(OnHit);

        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");

        rbPlayer = player.GetComponent<Rigidbody2D>();
    }

    protected void MoveTowardPlayer()
    {
        //https://www.youtube.com/watch?v=4Wh22ynlLyk&ab_channel=PressStart 

        

        var playerDirection = distanceEP.normalized;

        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;


        if (Mathf.Abs(rb.velocity.magnitude) <= maxSpeed)
        {
            rb.AddForce(enemySpeed * playerDirection);

        }
    }

    protected virtual void FixedUpdate()
    {
        distanceEP = rbPlayer.position - rb.position;
    }

    private void OnDestroy()
    {
        enemyNumber--;

        if (enemyNumber <= 0)
        {
            GameObject.FindGameObjectWithTag("GameManagere").GetComponent<ChangeLevel>().ChangeScene();
        }
    }

}
