using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform target;

    protected float moveSpeed = 4.0f;
    public LayerMask groundMask = -1;

    bool visible = true;


    protected virtual void Start()
    {
        target = GameManager.player.transform;

    }

    private void Update()
    {
        if (visible) 
            return;

        Debug.Log("Mannequin invisible!");

        Vector3 position = transform.position;

        Vector3 difference = target.position - position;
        Vector3 direction = difference.normalized;

        position += moveSpeed * Time.deltaTime * direction;

        //Vector3 origin = new(position.x, 100f, position.z);
        //Ray ray = new(origin, Vector3.down);
        //if (Physics.Raycast(ray, out RaycastHit hit, 200f, groundMask))
        //    position.y = Mathf.Max(hit.point.y, position.y);

        transform.position = position;
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }
}
