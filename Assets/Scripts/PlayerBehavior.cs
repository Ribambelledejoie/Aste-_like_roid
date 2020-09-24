﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 direction;
    //private Mouse mouse;
    private Vector2 mousePos;
    private Vector2 lookDir;


    private PlayerInputs playerinputs;

    [SerializeField] private float fireRate = 0.0f;
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float maxSpeed = 0.0f;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 20.0f;
    [SerializeField] private Camera cam;
    [SerializeField, Range(0,360)] private int angleCorrection;

    // Start is called before the first frame update

    private void OnMoveCancelled(InputAction.CallbackContext obj)
    {
        direction = new Vector2(0.0f, 0.0f);
        anim.SetBool("IsMoving", false);
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        //direction = stickDirection 

        //récupère valeur input

        direction = obj.ReadValue<Vector2>();

        anim.SetBool("IsMoving", true);

    }

    private void OnLookPerformed(InputAction.CallbackContext obj)
    {

        mousePos = obj.ReadValue<Vector2>();

        mousePos = cam.ScreenToWorldPoint(mousePos);


        //calcule le vecteur entre position souris et joueur

    }

    private void OnShootPerformed (InputAction.CallbackContext obj)
    {

        GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);

        Destroy(bullet, 1);

    }

    private void OnShootCanceled (InputAction.CallbackContext obj)

    {

    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {

    }


    /// <summary>
    /// blablablablabla
    /// </summary>
    private void OnEnable()
    {
        //permet d'appeler les inputs (?)
        //fonction TODO (peut etre) permet de mieux lire les commentaires
        //va faire en sorte d'assigner une fonction de retour à un input : important !!! pas oublier !
        playerinputs = new PlayerInputs();
        playerinputs.Enable();
        playerinputs.Player.Shoot.performed += OnShootPerformed;
        playerinputs.Player.Shoot.canceled += OnShootCanceled;
        playerinputs.Player.Move.performed += OnMovePerformed;
        playerinputs.Player.MousePosition.performed += OnLookPerformed;
        playerinputs.Player.Move.canceled += OnMoveCancelled;
        playerinputs.Player.Jump.performed += OnJumpPerformed;

    }

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(0.0f, -2.0f);
        anim = GetComponent<Animator>();
    }

    //Magnitude = x et y
    private void Update()
    {

        anim.SetFloat("Speed", rb.velocity.magnitude);

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - angleCorrection;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    // Update is called once per frame

        //addforce ===== rigibody obligatoire !
    void FixedUpdate()
    {

        lookDir = (mousePos - rb.position).normalized;

        if (Mathf.Abs(rb.velocity.magnitude) <= maxSpeed)
        {
            rb.AddForce(speed * direction);
        }
    }
}
