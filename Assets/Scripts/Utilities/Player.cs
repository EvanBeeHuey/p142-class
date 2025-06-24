using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class Player : MonoBehaviour, ProjectActions.IOverworldActions
{
    ProjectActions input;
    CharacterController cc;
    Rigidbody rb;
    Animator anim;
    public AudioSource audioSource;
    public AudioClip hitsound;
    public AudioClip pickup;
    public AudioClip jump;
    public AudioClip death;

    [Header("Movement Variables")]
    [SerializeField] private float initSpeed = 5.0f;
    [SerializeField] private float maxSpeed = 15.0f;
    [SerializeField] private float moveAccel = 0.1f;
    private float curSpeed = 5.0f;

    [Header("Jump Variables")]
    [SerializeField] private float jumpHeight = 0.1f;
    [SerializeField] private float jumpTime = 0.7f;

    //values calculated using our jump height and jump time
    private float timeToJumpApex; //jumpTime divided by 2
    private float initJumpVelocity;

    //Character Movement
    Vector3 direction;
    Vector3 velocity;
    float vel;

    //Character Health
    public float playerHealth = 20.0f;

    //Player Attacks
    Vector3 attackCenter = new(0f, 0f, 0.75f);
    Vector3 attackSize = new(1.0f, 2.0f, 0.5f);
    public LayerMask attackMask;

    //calculated based on our jump values
    private float gravity;
    private bool isJumpPressed = false;

    public GameObject playerCamera;

    private float cameraRotation;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //playerCamera = GetComponentInChildren<Camera>();
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        timeToJumpApex = jumpTime / 2;
        gravity = (-2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        initJumpVelocity = -(gravity * timeToJumpApex);

        Cursor.lockState = CursorLockMode.Locked;

        GameManager.player = this;
    }

    void OnEnable()
    {
        input = new ProjectActions();
        input.Enable();
        input.Overworld.SetCallbacks(this);
    }

    void OnDisable()
    {
        input.Disable();
        input.Overworld.RemoveCallbacks(this);
    }

    #region InputFunctions
    public void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed) direction = context.ReadValue<Vector3>();
        if (context.canceled)
        {
            rb.linearVelocity = Vector3.zero;
            direction = Vector3.zero;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed) moveAccel = moveAccel + 0.1f;
        if (context.canceled)
        {
            moveAccel = 0f;
            curSpeed = 0.2f;
        }
    }
    #endregion

    void Update()
    {
        Vector2 groundVel = new Vector2(velocity.x, velocity.z);
        anim.SetFloat("Velocity", velocity.magnitude);
    }

    void FixedUpdate()
    {
        AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);
        if (playerHealth <= 0f && !animState.IsName("Dying"))
        {
            anim.Play("Dying");
            audioSource.clip = death;
            audioSource.Play();
            Debug.Log("Player died");
            return;
        }

        if (playerHealth <= 0f)
                return;

        UpdateMouseLook();
        UpdateDirection();

        float hInput = direction.x;
        float vInput = direction.y;

        UpdateCharacterVelocity();

        cc.Move(velocity);

        //animations
        if (Input.GetMouseButtonDown(0)) // 0 = left click, 1 = right click, 2 = middle click
        {
            if (animState.IsName("Locomotion"))
            {
                anim.SetTrigger("PlayerPunch");
                Vector3 attackOverlap = transform.position + transform.rotation * attackCenter;
                Collider[] attackColliders = Physics.OverlapBox(attackOverlap, attackSize, transform.rotation, attackMask);
                foreach (Collider col in attackColliders)
                {
                    Enemy e = col.GetComponent<Enemy>();
                    if (e.enemyHealth > 0f)
                    {
                        e.HandleAttackCollisionPunch();
                    }
                }
                Debug.Log("Player punched enemy");
            }
            Debug.Log("Pressed right click");
            return;
        }

        if (Input.GetMouseButtonDown(1)) // right click
        {
            if (animState.IsName("Locomotion"))
            {
                anim.SetTrigger("PlayerKick");
                Vector3 attackOverlap = transform.position + transform.rotation * attackCenter;
                Collider[] attackColliders = Physics.OverlapBox(attackOverlap, attackSize, transform.rotation, attackMask);
                foreach (Collider col in attackColliders)
                {
                    Enemy e = col.GetComponent<Enemy>();
                    if (e.enemyHealth > 0f)
                    {
                        e.HandleAttackCollisionKick();
                    }
                }
                Debug.Log("Player kicked enemy");
            }
            Debug.Log("Pressed right click");
            return;
        }
    }

    private void UpdateCharacterVelocity()
    {
        if (direction == Vector3.zero) curSpeed = initSpeed;

        velocity.x = direction.x * curSpeed;
        velocity.z = direction.z * curSpeed;

        curSpeed += moveAccel * Time.fixedDeltaTime;
        curSpeed = Mathf.Clamp(curSpeed, 0f, maxSpeed);

        if (!cc.isGrounded) velocity.y += gravity * Time.fixedDeltaTime;
        else velocity.y = CheckJump();
    }

    private float CheckJump()
    {
        if (isJumpPressed)
        {
            audioSource.clip = jump;
            audioSource.Play();
            return initJumpVelocity;
        }
        else return -cc.minMoveDistance;
    }

    private void UpdateMouseLook()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * x);

        cameraRotation = Mathf.Clamp(cameraRotation - y, -30f, 30f);
        if (playerCamera)
            playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);
    }

    private void UpdateDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = new Vector3(x, 0f, z).normalized;

        direction = (transform.right * direction.x + transform.forward * direction.z);
    }

    public void HandleAttackCollision()
    {
        playerHealth -= 2.0f;
        audioSource.clip = hitsound;
        audioSource.Play();
        anim.Play("Taking Punch", 0, 0f);
        Debug.Log("Player lost 2 health");
    }
}
