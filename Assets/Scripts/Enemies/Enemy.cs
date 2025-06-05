using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Detects manually if obj is being seen by the main camera
    GameObject EnemyM;
    Collider enemyMcollider;

    Camera cam;
    Plane[] planes;

    protected int health;
    [SerializeField] protected int maxHealth;

    protected virtual void Start()
    {
        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        enemyMcollider = GetComponent<Collider>();

        if (maxHealth <= 0) maxHealth = 5;

        health = maxHealth;
    }

    public virtual void TakeDamage(int DamageValue, DamageType damageType = DamageType.Default)
    {
        health -= DamageValue;

        if (health <= 0)
        {
            if (transform.parent != null) Destroy(transform.parent.gameObject, 0.5f);
            else Destroy(gameObject, 0.8f);
        }
    }

    private void Update()
    {
        if (GeometryUtility.TestPlanesAABB(planes, enemyMcollider.bounds))
        {
            Debug.Log(EnemyM.name + " has been detected!");
        }
        else
        {
            Debug.Log("Nothing has been detected");
        }
    }
}

public enum DamageType
{
    Default
}
