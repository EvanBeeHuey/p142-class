using UnityEngine;

public class SpawnHealth : MonoBehaviour
{
    [SerializeField] float spawnXMin;
    [SerializeField] float spawnXMax;
    [SerializeField] float spawnYMin;
    [SerializeField] float spawnYMax;
    [SerializeField] float spawnZMin;
    [SerializeField] float spawnZMax;

    public GameObject[] randomHealth;
    public int numOf = 2;
    void Start()
    {
        for (int i = 0; i < numOf; i++)
        {
            int randomIndex = Random.Range(0, randomHealth.Length);
            Instantiate(randomHealth[randomIndex], new Vector3(Random.Range(spawnXMin, spawnXMax), Random.Range(spawnYMin, spawnYMax), Random.Range(spawnZMin, spawnZMax)), Quaternion.identity);
        }
    }
}
