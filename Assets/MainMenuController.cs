using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour
{
    private MainMenu menu;

    void Awake()
    {
        menu = GetComponent<MainMenu>();
    }

    public void Press( InputAction.CallbackContext ctx )
    {
        if ( !ctx.performed ) return;
        menu.PressHovered();
    }

    public void SelectNext( InputAction.CallbackContext ctx )
    {
        if ( !ctx.performed ) return;
        menu.SelectNextButton();
    }

    public void SelectPrevious( InputAction.CallbackContext ctx )
    {
        if ( !ctx.performed ) return;
        menu.SelectPreviousButton();
    }

    public void Back( InputAction.CallbackContext ctx )
    {
        if ( !ctx.performed ) return;
        menu.Back();
    }
}
