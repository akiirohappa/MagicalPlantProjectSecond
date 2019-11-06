using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public enum MenuState
{
    None,
    Item,
    Shop,
    Peformance,
    Save,
    Config,
    ItemSet,
    EventSelect,
}

public class MenuManager : MonoBehaviour
{
    bool MenuOpen = false;
    GameObject openButton;
    public MenuState state;
    Dictionary<MenuState, MenuManagerBase> Menus;
    public MenuManagerBase MenuManagerB
    {
        get { return Menus[state]; }
    }
    [SerializeField] EventSystem eventSystem;
    public GameObject Cullent
    {
        get { return eventSystem.currentSelectedGameObject; }
    }
    // Start is called before the first frame update
    void Start()
    {
        state = MenuState.None;
        openButton = GameObject.Find("MenuButton");
        Menus = new Dictionary<MenuState, MenuManagerBase>();
        Menus[MenuState.Item] = new ItemManager(this);
        Menus[MenuState.Shop] = new ShopManager(this);
        Menus[MenuState.ItemSet] = new ItemSetManager(this);
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
        if(state != MenuState.None && state != MenuState.EventSelect)
        {
            ButtonToMain();
        }
        MenuState transst = (MenuState)Enum.Parse(typeof(MenuState), st);
        Array menustA = Enum.GetValues(typeof(MenuState));
        foreach(MenuState s in menustA)
        {
            if(transst == s)
            {
                if(state != s)
                {
                    state = transst;
                    if (Menus[state] != null)
                    {
                        Menus[state].Open();
                    }
                }
            }
        }
        if (MenuOpen)
        {
            MenuButton();
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
    public void ButtonItem()
    {
        GameObject bt = eventSystem.currentSelectedGameObject;
        Menus[state].PlessItemButton(bt.GetComponent<ItemButton>().item);
    }
    public void ButtonToMain()
    {
        if (state == MenuState.None || state == MenuState.EventSelect)
        {
            return;
        }
        if (MenuOpen)
        {
            MenuButton();
        }
        Menus[state].Close();
        state = MenuState.None;
    }
}
