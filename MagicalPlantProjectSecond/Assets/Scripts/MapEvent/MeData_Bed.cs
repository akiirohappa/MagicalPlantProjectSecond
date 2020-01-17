using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeData_Bed : MapEventBase
{
    public MeData_Bed(int n):base(n)
    {
        data = new FaData_Bed();
        events = new EventCode[]
        {
            new EventCode("レベルアップ","levelup"),
            new EventCode("寝る","sleep"),
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
        if (data.nowLevel != 0)
        {
            EventStart(events[1].eventText);
        }
        
    }
    public override void OnHoverRun(Vector3Int pos)
    {
        if (data.nowLevel == 0)
        {
            MapEventManager.GetInstance().Bar.SetHotBar("施設メニュー", "");
        }
        else
        {
            MapEventManager.GetInstance().Bar.SetHotBar("施設メニュー", "寝る");
        }
    }
    public override void EventStart(string text)
    {
        if (text == events[0].eventText)
        {
            if (data.LevelUPPriceCheck())
            {
                data.nowLevel++;
            }
            menu.State = MenuState.None;
        }
        if (text == events[1].eventText)
        {
            //寝るときの処理をここに
            TimeManager.GetInstance().AccelStart((FaData_Bed)data);
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
        if (data.nowLevel != 0)
        {
            ButtonTextSet(events[1].objText, events[1].eventText, buttonList[buttnCount++]);
        }
        if(data.nowLevel != data.maxLevel) 
        {
            ButtonTextSet(events[0].objText, events[0].eventText, buttonList[buttnCount++]);
        }
        ButtonTextSet("何もしない", "None", buttonList[buttnCount++]);
    }
}
