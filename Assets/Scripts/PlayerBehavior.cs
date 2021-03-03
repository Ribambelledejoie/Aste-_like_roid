using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 mousePos;
    private Vector2 lookDir;

    private bool isShooting = false;
    private float timeRemaining = 1.0f;

    private PlayerInputs playerinputs;
    
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [Header("Misc")]
    [SerializeField] private Transform FirePoint;
    [SerializeField] private Camera cam;
    [SerializeField, Range(0, 360)] private int angleCorrection;
    [SerializeField] private Weapon actualGun;

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = new Vector2(0.0f, 0.0f);
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
    }

    private void OnLookPerformed(InputAction.CallbackContext obj)
    {    
        mousePos = obj.ReadValue<Vector2>();

        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);

        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);

        mousePos /= new Vector2(Screen.width, Screen.height);

        var mousePos3D = new Vector3(mousePos.x, mousePos.y, Mathf.Abs(cam.transform.position.z));

        mousePos = cam.ViewportToWorldPoint(mousePos);
    }


    private void OnShootPerformed(InputAction.CallbackContext obj)
    {
        isShooting = true;

        if (timeRemaining == actualGun.fireRate)
        {
            actualGun.Shoot(FirePoint);
        }
    }


    private void OnShootCanceled(InputAction.CallbackContext obj)
    {
        isShooting = false;
    }

    private void OnEnable()
    {
        playerinputs = new PlayerInputs();
        playerinputs.Enable();
        playerinputs.Player.Shoot.performed += OnShootPerformed;
        playerinputs.Player.Shoot.canceled += OnShootCanceled;
        playerinputs.Player.Move.performed += OnMovePerformed;
        playerinputs.Player.MousePosition.performed += OnLookPerformed;
        playerinputs.Player.Move.canceled += OnMoveCanceled;
    }


    void Start()
    {
        timeRemaining = actualGun.fireRate;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Shoot();
        Look();   
    }

    private void Shoot()
    {
        if (isShooting)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = actualGun.fireRate;
                actualGun.Shoot(FirePoint);
            }
        }
    }

    private void Look()
    {
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - angleCorrection;

        var rotationForce = 0.01f;

        if (direction.sqrMagnitude == 0)
        {
            rotationForce = 1;
        }

        var targetAxis = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetAxis, rotationForce);
    }

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
        playerinputs.Disable();
    }
}
