using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeData_Bookshelf : MapEventBase
{
    MenuManager m;
    public MeData_Bookshelf(int n) : base(n)
    {
        m = GameObject.Find("Manager").GetComponent<MenuManager>();
        events = new EventCode[]
        {
            new EventCode("ヘルプ","Help")
        };
    }
    public override void OnLeftClickRun()
    {
        MapEventManager.GetInstance().Facilities.SetData("本棚", "家に置いてあった本棚。\nなんかいろいろな本が置いてある。","ヘルプ画面");
        m.State = MenuState.EventSelect;
        MenuButtonMake();
    }
    public override void OnRightClickRun()
    {
        EventStart("Help");
    }
    public override void OnHoverRun(Vector3Int pos)
    {
        MapEventManager.GetInstance().Bar.SetHotBar("", "ヘルプ");
    }
    public override void EventStart(string text)
    {
        if (text == "None")
        {
            MenuClose();
            MapEventManager.GetInstance().Bar.SetHideHotBar(false);
            m.State = MenuState.None;
            MapEventManager.GetInstance().Facilities.SetHideHotBar(false);
            return;
        }
        else if(text == "Help")
        {
            MenuClose();
            MapEventManager.GetInstance().Bar.SetHideHotBar(false);
            m.State = MenuState.Help;
            m.MenusGet[MenuState.Help].Open();
            MapEventManager.GetInstance().Facilities.SetHideHotBar(false);
        }
    }
    protected override void MenuButtonMake()
    {
        base.MenuButtonMake();
        ButtonTextSet(events[0].objText, events[0].eventText, buttonList[0]);
        ButtonTextSet("何もしない", "None", buttonList[1]);
    }
}

