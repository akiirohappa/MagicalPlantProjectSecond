using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum MenuState
{
    None,
    Item,
    Shop,
    Peformance,
    Save,
    Config,

}

public class MenuManager : MonoBehaviour
{
    bool MenuOpen = false;
    GameObject openButton;
    public MenuState state;
    Dictionary<MenuState, MenuManagerBase> Menus;
    // Start is called before the first frame update
    void Start()
    {
        state = MenuState.None;
        openButton = GameObject.Find("MenuButton");
        Menus = new Dictionary<MenuState, MenuManagerBase>();
        Menus[MenuState.Shop] = new ShopManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MenuButton()
    {
        if (!MenuOpen)
        {
            openButton.GetComponent<Animator>().SetTrigger("Open");
            MenuOpen = true;
            openButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "閉じる";
        }
        else
        {
            openButton.GetComponent<Animator>().SetTrigger("Close");
            MenuOpen = false;
            openButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "メニュー";
        }
    }
    public void SendMenuButton(string st)
    {
        MenuState transst = (MenuState)Enum.Parse(typeof(MenuState), st);
        Array menustA = Enum.GetValues(typeof(MenuState));
        foreach(MenuState s in menustA)
        {
            if(transst == s)
            {
                state = transst;
            }
        }
    }
    public void ButtonSubmit()
    {
        if (state == MenuState.None)
        {
            return;
        }
        Menus[state].Submit();
    }
    public void ButtonCancel()
    {
        if (state == MenuState.None)
        {
            return;
        }
        Menus[state].Cancel();
    }
    public void ButtonEx(string st)
    {
        if(state == MenuState.None)
        {
            return;
        }
        Menus[state].Button(st);
    }
    public void ButtonToMain()
    {

    }
}
