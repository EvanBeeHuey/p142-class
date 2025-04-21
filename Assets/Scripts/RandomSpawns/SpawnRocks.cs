using UnityEngine;
using UnityEngine.UIElements;

public class SpawnRocks : MonoBehaviour
{
    public GameObject[] randomRocks;
    public int numOf = 10;

    void Start()
    {
        for (int i=0; i < numOf; i++)
        {
            int randomIndex = Random.Range(0, randomRocks.Length);
            Instantiate(randomRocks[randomIndex], new Vector3(Random.Range(220, 279), Random.Range(83, 84), Random.Range(133, 172)), Quaternion.identity);
        }
    }

}

