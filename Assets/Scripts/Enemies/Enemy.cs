using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform target;
    Animator animEnemy;
    AudioSource audioSource;
    public AudioClip hitsound;
    public AudioClip death;

    //enemy movement
    public float moveSpeed = 4.0f;

    //enemy health
    public float enemyHealth = 10.0f;

    Vector3 attackCenter = new (0f, 0f, 0.75f);
    Vector3 attackSize = new (1.0f, 2.0f, 0.5f);
    public LayerMask attackMask;

    //drops
    public GameObject powerup; 

    Player player;

    protected virtual void Start()
    {
        powerup = Resources.Load<GameObject>("EnemyDrop");

        target = GameManager.player.transform;
        animEnemy = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        player = FindAnyObjectByType<Player>();
    }

    private void Update()
    {
        AnimatorStateInfo animState = animEnemy.GetCurrentAnimatorStateInfo(0);
        if (enemyHealth <= 0f && !animState.IsName("Dying"))
        {
            animEnemy.Play("Dying");
            audioSource.clip = death;
            audioSource.Play();
            ScoringSystem.score += 1.0f;
            Instantiate(powerup, transform.position + Vector3.up, Quaternion.identity);
            Debug.Log("Enemy died");
            return;
        }

        if (enemyHealth <= 0f)
            return;

        //enemy attack
        float distToPlayer = (transform.position - player.transform.position).magnitude;
        if (distToPlayer < 1.0f)
        {
            animEnemy.SetFloat("EnemyVelocity", 0f);
            if (animState.IsName("Locomotion") && player.playerHealth > 0 && !animEnemy.IsInTransition(0))
            {
                animEnemy.SetTrigger("EnemyAttack");
                Vector3 attackOverlap = transform.position + transform.rotation * attackCenter;
                Collider[] attackColliders = Physics.OverlapBox(attackOverlap, attackSize, transform.rotation, attackMask);
                foreach (Collider col in attackColliders)
                {
                    Player p = col.GetComponent<Player>();
                        p.HandleAttackCollision();
                }
                Debug.Log("Enemy attacked player");
            }
            return;
        }

        Vector3 directionToTarget = (transform.position - player.transform.position).normalized;
        float dotProduct = Vector3.Dot(player.transform.forward, directionToTarget);

        float thresh = 0.5f;
        if (dotProduct > thresh || player.playerHealth <= 0)
        {
            animEnemy.SetFloat("EnemyVelocity", 0f);
            return;
        }

        transform.LookAt(player.transform);

        Vector3 position = transform.position;

        Vector3 difference = target.position - position;
        Vector3 direction = difference.normalized;

        position += moveSpeed * Time.deltaTime * direction;

        transform.position = position;

        //animations
        animEnemy.SetFloat("EnemyVelocity", 1.0f);
    }

    public void HandleAttackCollisionPunch()
    {
        enemyHealth -= 3.0f;
        audioSource.clip = hitsound;
        audioSource.Play();
        animEnemy.Play("Taking Punch", 0, 0f);
        Debug.Log("Enemy lost 3 health from punch");
    }

    public void HandleAttackCollisionKick()
    {
        enemyHealth -= 4.0f;
        audioSource.clip = hitsound;
        audioSource.Play();
        animEnemy.Play("Kicked", 0, 0f);
        Debug.Log("Enemy lost 4 health from kick");
    }
}
