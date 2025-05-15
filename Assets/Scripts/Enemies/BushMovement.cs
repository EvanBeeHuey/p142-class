using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BushMovement : Enemy
{
    Rigidbody rb;

    [Header("Movement Variables")]
    [SerializeField] private float initBushSpeed = 0.2f;
    [SerializeField] private float maxBushSpeed = 0.35f;
    [SerializeField] private float moveBushAccel = 0.05f;
    private float curBushSpeed = 0.2f;

    Vector3 bushDirection;
    Vector3 bushVelocity;
    float bVel;

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody>();
    }

    void UpdateBushVelocity()
    {
        if (bushDirection == Vector3.zero) curBushSpeed = initBushSpeed;

        bushVelocity.x = bushDirection.x * curBushSpeed;
        bushVelocity.z = bushDirection.z * curBushSpeed;

        curBushSpeed += moveBushAccel * Time.fixedDeltaTime;
        curBushSpeed = Mathf.Clamp(curBushSpeed, 0f, maxBushSpeed);
    }

    private void Update()
    {
        float hInput = bushDirection.x;
        float vInput = bushDirection.z;

        UpdateBushVelocity();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("PlayerSight"))
        {
            if (gameObject.CompareTag("BushEyes"))
            {
                gameObject.SetActive(false);
                bushVelocity.x = 0;
                bushVelocity.z = 0;
            }
        }
    }
}
