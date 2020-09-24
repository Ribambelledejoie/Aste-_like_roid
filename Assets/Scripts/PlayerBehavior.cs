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

    private PlayerInputs playerinputs;

    /*
    private float moveHorizontal = Input.GetAxisRaw("Horizontal");
    private float moveVertical = Input.GetAxisRaw("Vertical");
    */


    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float maxSpeed = 0.0f;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 20.0f;
    [SerializeField] private Camera cam;
    [SerializeField, Range(0,360)] private int angleCorrection;
    //[SerializeField] private float rotationSpeed = 0.0f;

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

        /*
        direction.x = stickDirection.x;
        direction.y = stickDirection.y;
        */

    }

    private void OnLookPerformed(InputAction.CallbackContext obj)
    {

        mousePos = obj.ReadValue<Vector2>();

        mousePos = cam.ScreenToWorldPoint(mousePos);

        //mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //calcule le vecteur entre position souris et joueur



        
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);


        // transform.LookAt(mouse, Vector3.forward);

        // Debug.Log("NIQUETAMERE : " + mouse);


        /*
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //Vector3.Normalize(mouse);

        var angle = Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg;

         transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

         //Debug.Log("NIQUE");
         */
    }

    private void OnShootPerformed (InputAction.CallbackContext obj)
    {
        GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);

        Destroy(bullet, 1);

        
        //Destroy(bullet, 0f);
    }

   /* private void OnShootPerformed(InputAction.CallbackContext obj)
    {
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

        Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

        Destroy(Temporary_Bullet_Handler, 2.0f);
    }
    */
    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {

    }

   /* private void OnLookPerformed(InputAction.CallbackContext obj)
    {

    }
    */

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
        playerinputs.Player.Move.performed += OnMovePerformed;
        playerinputs.Player.MousePosition.performed += OnLookPerformed;
        playerinputs.Player.Move.canceled += OnMoveCancelled;
        playerinputs.Player.Jump.performed += OnJumpPerformed;
        //playerinputs.Player.MousePosition.performed += OnMovePerformed; DE LA MERDE
        // playerinputs.Player.MousePosition.performed += OnLookPerformed; DE LA MERDE
    }

    void Start()
    {
        //rb = gameObject.AddComponent<Rigidbody2D>();
        //rigibody calcule la vitesse 
        //mouse = new Vector2();
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(0.0f, -2.0f);
        anim = GetComponent<Animator>();
    }

    //Magnitude = x et y
    private void Update()
    {
        //mouse = new Vector2();
        //Mouse.current.WarpCursorPosition(mouse);
        anim.SetFloat("Speed", rb.velocity.magnitude);

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - angleCorrection;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //mouse = Mouse.current; PAS OUF
        //transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition)); DE LA MERDE
    }

    // Update is called once per frame

        //addforce ===== rigibody obligatoire !
    void FixedUpdate()
    {
        /*
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        */
        lookDir = (mousePos - rb.position).normalized;

        if (Mathf.Abs(rb.velocity.magnitude) <= maxSpeed)
        {
            rb.AddForce(speed * direction);
        }
    }
}
