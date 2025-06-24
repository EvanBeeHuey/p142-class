using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitButton : MonoBehaviour
{
    protected Button button;

    protected virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    protected void OnDestroy() => button.onClick.RemoveAllListeners();

    protected void OnClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
