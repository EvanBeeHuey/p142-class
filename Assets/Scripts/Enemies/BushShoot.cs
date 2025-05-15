using System;
using UnityEngine;

public class BushShoot : Enemy
{
    [SerializeField] private float radar = 5;
    [SerializeField] private float fireRate = 2.0f;

    private Transform playerTransform;
    private float timeSinceLastShot = 0;

    public float detectionAngle = 60f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        if (fireRate < -0)
            fireRate = 2;

        if (radar <= 0)
            radar = 5;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerSpawned += OnPlayerSpawnedCallback;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerSpawned -= OnPlayerSpawnedCallback;
    }

    private void OnPlayerSpawnedCallback(Player controller) => playerTransform = controller.transform;

    // Update is called once per frame
    void Update()
    {
        if (!playerTransform) return;

        CheckDistance(Mathf.Abs(playerTransform.position.x - transform.position.x));
    }

    void CheckDistance(float distance)
    {
        if (distance <= radar)
        {
            if (IsPlayerLookingAtMe()) 
                CheckFire();
        }
    }

    void CheckFire()
    {
        if (Time.time >= timeSinceLastShot + fireRate)
        {
            timeSinceLastShot = Time.time;
        }
    }

    private bool IsPlayerLookingAtMe()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player")?.transform;
        }

        Vector3 toEnemy = (transform.position - playerTransform.position).normalized;
        Vector3 playerForward = playerTransform.forward;

        float angle = Vector3.Angle(playerForward, toEnemy);

        return angle < detectionAngle;
    }
}
