using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : BaseMenu
{
    public Button backBtn;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.Credits;

        backBtn.onClick.AddListener(JumpBack);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
