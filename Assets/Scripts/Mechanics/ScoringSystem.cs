using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    private float elapsedTime = 0f;
    private float score = 0f;
    private float scoreMultiplier = 10f;
    void Start()
    {
        
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        Debug.Log("Score: " + score);
    }
}
