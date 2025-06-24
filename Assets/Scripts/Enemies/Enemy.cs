using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform target;
    Animator animEnemy;

    //enemy movement
    protected float moveSpeed = 4.0f;

    //enemy health
    private float enemyHealth = 10.0f;

    Player player;

    protected virtual void Start()
    {
        target = GameManager.player.transform;
        animEnemy = GetComponent<Animator>();

        player = FindAnyObjectByType<Player>();
    }

    private void Update()
    {
        float distToPlayer = (transform.position - player.transform.position).magnitude;
        if (distToPlayer < 1.0f)
        {
            AnimatorStateInfo animState = animEnemy.GetCurrentAnimatorStateInfo(0);
            if (animState.IsName("Locomotion"))
            {
                animEnemy.SetTrigger("EnemyAttack");
            }
            return;
        }

        Vector3 directionToTarget = (transform.position - player.transform.position).normalized;
        float dotProduct = Vector3.Dot(player.transform.forward, directionToTarget);

        float thresh = 0.5f;
        if (dotProduct > thresh)
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
}
