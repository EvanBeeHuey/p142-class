using UnityEditor.Build.Content;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField, Range(1, 20)] private float lifespan = 3.0f;

    private Rigidbody p_Rigidbody;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody>().linearVelocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("bushProj") && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.lives -= 1;
            Debug.Log(GameManager.Instance.lives);
            Destroy(gameObject);
        }
    }
}
