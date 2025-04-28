using TMPro;
using UnityEngine;

public class InGame : MonoBehaviour
{
    public TMP_Text livesText;
    public TMP_Text collectText;

    private void Start()
    {
        livesText.text = $"Lives: {GameManager.Instance.lives}";

        GameManager.Instance.OnLifeValueChanged.AddListener(LifeValueChanged);

        LifeValueChanged(GameManager.Instance.lives);
    }

    private void LifeValueChanged(int value) => livesText.text = $"Lives: {value}";}
