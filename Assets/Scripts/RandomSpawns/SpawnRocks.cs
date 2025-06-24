using UnityEngine;

public class SpawnRocks : MonoBehaviour
{
    [SerializeField] float spawnXMin;
    [SerializeField] float spawnXMax;
    [SerializeField] float spawnYMin;
    [SerializeField] float spawnYMax;
    [SerializeField] float spawnZMin;
    [SerializeField] float spawnZMax;

    public GameObject[] randomRocks;
    public int numOf = 10;

    void Start()
    {
        for (int i=0; i < numOf; i++)
        {
            int randomIndex = Random.Range(0, randomRocks.Length);
            Instantiate(randomRocks[randomIndex], new Vector3(Random.Range(spawnXMin, spawnXMax), Random.Range(spawnYMin, spawnYMax), Random.Range(spawnZMin, spawnZMax)), Quaternion.identity);
        }
    }

}

