using UnityEngine;

public class PowerupYellow : MonoBehaviour //double the score
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
            ScoringSystem.score *= 2.0f;
            player.audioSource.clip = player.pickup;
            player.audioSource.Play();
            Debug.Log("Player picked up yellow powerup");
            Destroy(gameObject);
        }

    }
}
