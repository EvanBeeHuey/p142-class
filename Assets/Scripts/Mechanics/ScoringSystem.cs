using UnityEngine;
using UnityEngine.UIElements;

public class ScoringSystem : MonoBehaviour
{
    private float elapsedTime = 0f;
    public float score = 0f;
    private float scoreMultiplier = 10f;
    Player player;

    //scoring
    public UIDocument uiDoc;
    public static float enemyKilled = 0f;
    private Label scoreText;
    private Label healthText;
    void Start()
    {
        scoreText = uiDoc.rootVisualElement.Q<Label>("PlayerHealth");
        healthText = uiDoc.rootVisualElement.Q<Label>("ScoreLabel");
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + player.playerHealth;
        score = Mathf.FloorToInt(enemyKilled * scoreMultiplier);
        Debug.Log("Score: " + score);
    }
}
