using System.Collections;
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

    private bool isShooting = false;
    private float timeRemaining = 1.0f;


    private PlayerInputs playerinputs;


    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float maxSpeed = 0.0f;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 20.0f;
    [SerializeField] private Camera cam;
    [SerializeField, Range(0, 360)] private int angleCorrection;

    [SerializeField] private float fireRate = 1.0f;




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


    private void OnShootPerformed(InputAction.CallbackContext obj)
    {

        isShooting = true;

        if (timeRemaining == fireRate)
        {
            Shoot();
        }



    }


    private void OnShootCanceled(InputAction.CallbackContext obj)

    {
        isShooting = false;
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);

        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

        rbBullet.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);

        Destroy(bullet, 1);
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

    private void OnValidate()
    {
        // on veut faire en sorte que la valeur de la puissance de feu ne descende pas sous zéro
        if (fireRate < 0)
        {
            fireRate = 0;
        }
    }

    void Start()
    {
        //ici, le temps qu'il reste est égale à la vitesse à laquelle le joueur va pouvoir tirer
        timeRemaining = fireRate;

        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(0.0f, -2.0f);
        anim = GetComponent<Animator>();
    }

    //Magnitude = x et y
    private void Update()
    {
        //Si on est entrain de tirer
        if (isShooting)
        {
            //Et s'il reste du temps
            if (timeRemaining > 0)
            {
                //On enlève du temps au temps qu'il reste - Time.deltaTime = temps en seconde depuis la dernière frame
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                //et sinon, 
                timeRemaining = fireRate;
                Shoot();
            }
        }


        anim.SetFloat("Speed", rb.velocity.magnitude);

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - angleCorrection;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }


    //addforce ===== rigibody obligatoire !
    void FixedUpdate()
    {

        lookDir = (mousePos - rb.position).normalized;

        if (Mathf.Abs(rb.velocity.magnitude) <= maxSpeed)
        {
            rb.AddForce(speed * direction);
        }
    }

    private void OnDestroy()
    {
        //Lorsque le joueur meurt, on lui désactive ses inputs
        playerinputs.Disable();
    }
}
