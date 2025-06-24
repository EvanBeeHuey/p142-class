using UnityEngine;

public class PowerupHealth : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.playerHealth += 2.0f;
            player.audioSource.clip = player.pickup;
            player.audioSource.Play();
            Debug.Log("Player picked up health powerup");
            Destroy(gameObject);
        }
    }
}
