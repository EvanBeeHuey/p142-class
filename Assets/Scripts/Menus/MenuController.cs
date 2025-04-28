using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public BaseMenu[] allMenus;
    public MenuStates initState = MenuStates.MainMenu;

    public BaseMenu CurrentState => currentState;
    private BaseMenu currentState;

    Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();
    Stack<MenuStates> menuStack = new Stack<MenuStates>();

    void Start()
    {
        if (allMenus.Length <= 0) allMenus = gameObject.GetComponentsInChildren<BaseMenu>(true);

        foreach (BaseMenu menu in allMenus)
        {
            if (menu == null) continue;
            menu.Init(this);

            if (menuDictionary.ContainsKey(menu.state)) continue;

            menuDictionary.Add(menu.state, menu);
        }

        SetActiveState(initState);

        GameManager.Instance.SetMenuController(this);
    }

    public void JumpBack()
    {
        if (menuStack.Count <= 0) return;

        menuStack.Pop();
        SetActiveState(menuStack.Peek(), true);
    }

    public void SetActiveState(MenuStates newState, bool isJumpingBack = false)
    {
        if (!menuDictionary.ContainsKey(newState)) return;

        if (currentState == menuDictionary[newState]) return;

        if (currentState != null)
        {
            currentState.ExitState();
            currentState.gameObject.SetActive(false);
        }
        currentState = menuDictionary[newState];
        currentState.gameObject.SetActive(true);
        currentState.EnterState();

        if (!isJumpingBack) menuStack.Push(newState);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
