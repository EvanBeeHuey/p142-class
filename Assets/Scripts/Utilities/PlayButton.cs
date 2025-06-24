using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour
{
    protected Button button;
    public AudioSource audioSource;
    public AudioClip clickButton;

    protected virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        audioSource = GetComponent<AudioSource>();
    }

    protected void OnDestroy() => button.onClick.RemoveAllListeners();

    protected void OnClick()
    {
        audioSource.clip = clickButton;
        audioSource.Play();
        SceneManager.LoadScene("FlatScene");
    }
}
