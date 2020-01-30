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
    Sleep,
    Help,
}

public class MenuManager : MonoBehaviour
{
    bool MenuOpen = false;
    GameObject openButton;
    MenuState state;
    
    Dictionary<MenuState, MenuManagerBase> Menus;
    public MenuManagerBase MenuManagerB
    {
        get { return Menus[State]; }
    }
    [SerializeField] EventSystem eventSystem = null;
    public EventSystem eventS
    {
        get { return eventSystem; }
    }
    public GameObject Cullent
    {
        get { return eventSystem.currentSelectedGameObject; }
    }
    public MenuState State
    {
        get { return state; }
        set
        {
            foreach (KeyValuePair<MenuState, MenuManagerBase> m in Menus)
            {
                if (m.Value.Obj.activeSelf)
                {
                    m.Value.Obj.SetActive(false);
                }
            }
            state = value;
        }
    }
    public Dictionary<MenuState, MenuManagerBase> MenusGet
    {
        get { return Menus; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        openButton = GameObject.Find("MenuButton");
        Menus = new Dictionary<MenuState, MenuManagerBase>();
        Menus[MenuState.Item] = new ItemManager(this);
        Menus[MenuState.Shop] = new ShopManager(this);
        Menus[MenuState.ItemSet] = new ItemSetManager(this);
        Menus[MenuState.Save] = new SaveManager(this);
        Menus[MenuState.Config] = new ConfigManager(this);
        Menus[MenuState.Help] = new HelpManager(this);
        Menus[MenuState.Peformance] = new PeforManceManager(this);
        State = MenuState.None;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        ((ConfigManager)Menus[MenuState.Config]).UpDate();
    }
    public void MenuButton()
    {
        if (TimeManager.GetInstance().sleep)
        {
            return;
        }
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
        if(State != MenuState.None && State != MenuState.EventSelect)
        {
            ButtonToMain();
        }
        MenuState transst = (MenuState)Enum.Parse(typeof(MenuState), st);
        Array menustA = Enum.GetValues(typeof(MenuState));
        foreach(MenuState s in menustA)
        {
            if(transst == s)
            {
                if(State != s)
                {
                    State = transst;
                    if (Menus[State] != null)
                    {
                        Menus[State].Open();
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
        if (State == MenuState.None)
        {
            return;
        }
        Menus[State].Submit();
    }
    public void ButtonCancel()
    {
        if (State == MenuState.None)
        {
            return;
        }
        Menus[State].Cancel();
    }
    public void ButtonEx(string st)
    {
        if(State == MenuState.None)
        {
            return;
        }
        Menus[State].Button(st);
    }
    public void ButtonItem()
    {
        GameObject bt = eventSystem.currentSelectedGameObject;
        Menus[State].PlessItemButton(bt.GetComponent<ItemButton>().item);
    }
    public void SliderValue(float f)
    {
        Menus[State].SliderChange(f);
    }
    public void ButtonToMain()
    {
        if (State == MenuState.None || State == MenuState.EventSelect)
        {
            return;
        }
        if (MenuOpen)
        {
            MenuButton();
        }
        //Menus[State].Close();
        MainManager.GetInstance.Key.shortcutActive = true;
        State = MenuState.None;
    }
}
