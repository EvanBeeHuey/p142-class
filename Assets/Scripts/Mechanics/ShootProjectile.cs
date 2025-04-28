using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSource audioSource;

    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;

    [SerializeField] private Transform projSpawn;

    [SerializeField] private ProjectileBehaviour projectilePrefab;
    [SerializeField] private AudioClip bushProj;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (initShotVelocity == Vector2.zero)
        {
            Debug.Log("Init Shot Velocity has been changed to a default value.");
            initShotVelocity.x = 7.0f;
        }

        if (!projSpawn || !projectilePrefab)
        {
            Debug.Log($"Please set default values on {gameObject.name}.");
        }
    }

    // Update is called once per frame
    public void Fire()
    {
        ProjectileBehaviour curProjectile;
        if (projectilePrefab != null)
        {
            curProjectile = Instantiate(projectilePrefab, projSpawn);
            curProjectile.SetVelocity(new Vector2(-initShotVelocity.x, initShotVelocity.y));
        }

        audioSource.PlayOneShot(bushProj);
    }
}
