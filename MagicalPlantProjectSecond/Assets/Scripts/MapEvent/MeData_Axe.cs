using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeData_Axe : MapEventBase
{
    public MeData_Axe(int n):base(n)
    {
        data = new FaData_Axe();
        events = new EventCode[]
        {
            new EventCode("レベルアップ","levelup"),
        };
    }
    public override void OnLeftClickRun()
    {
        menu.ButtonToMain();
        MapEventManager.GetInstance().Facilities.SetData(data);
        menu.State = MenuState.EventSelect;
        MenuButtonMake();
    }
    public override void OnRightClickRun()
    {
    }
    public override void OnHoverRun(Vector3Int pos)
    {
        MapEventManager.GetInstance().Bar.SetHotBar("施設メニュー", "");
    }
    public override void EventStart(string text)
    {
        if (text == events[0].eventText)
        {
            if (data.LevelUPPriceCheck())
            {
                data.nowLevel++;
                FieldManager.GetInstance().FieldUnlock(data.nowLevel);
            }
            menu.State = MenuState.None;
        }
        if (text == "None")
        {
            menu.State = MenuState.None;
        }
        MenuClose();
        MapEventManager.GetInstance().buttonPressd = false;
    }
    protected override void MenuButtonMake()
    {
        base.MenuButtonMake();
        int buttnCount = 0;
        if(data.nowLevel != data.maxLevel) 
        {
            ButtonTextSet(events[0].objText, events[0].eventText, buttonList[buttnCount++]);
        }
        ButtonTextSet("何もしない", "None", buttonList[buttnCount++]);
    }
}
