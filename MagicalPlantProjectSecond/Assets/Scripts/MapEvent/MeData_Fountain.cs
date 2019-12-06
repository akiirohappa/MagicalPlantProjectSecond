using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeData_Fountain:MapEventBase
{
    public MeData_Fountain(int num):base(num)
    {
        data = new FaData_Fountain();
        events = new EventCode[]
        {
            new EventCode("レベルアップ","levelup"),
            new EventCode("水をあげる","water"),
        };
    }
    public override void OnHoverRun(Vector3Int pos)
    {
        if(data.nowLevel == 0)
        {
            MapEventManager.GetInstance().Bar.SetHotBar("施設メニュー", "");
        }
        else
        {
            MapEventManager.GetInstance().Bar.SetHotBar("施設メニュー", "水をあげる");
        }
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
        if (data.nowLevel == 1)
        {
            EventStart(events[1].eventText);   
        }
    }
    public override void EventStart(string text)
    {
        if(text == events[0].eventText)
        {
            data.nowLevel++;
            menu.State = MenuState.None;
        }
        if (text == events[1].eventText)
        {
            for (int i = 0; i < FieldManager.GetInstance().myField.Length; i++)
            {
                if (FieldManager.GetInstance().myField[i].plantState != PlantState.DontUse)
                {
                    Vector3 vec = TileManager.GetInstance().CellToWorldPos(TileManager.GetInstance().PlantField[i]);
                    vec.y += 0.5f;
                    vec.x += 0.5f;
                    DontDestroyManager.my.Sound.PlaySE("Water");
                    MainManager.GetInstance.Particle.PaticleMake(MainManager.GetInstance.Particle.Particle[1], vec);
                    FieldManager.GetInstance().myField[i].soilState = Soil.VeryMoist;
                    FieldManager.GetInstance().myField[i].soilWaterValue = 100;
                }
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
        if (data.nowLevel == 0)
        {
            ButtonTextSet(events[0].objText, events[0].eventText, buttonList[buttnCount++]);
        }
        else
        {
            ButtonTextSet(events[1].objText, events[1].eventText, buttonList[buttnCount++]);
        }
        ButtonTextSet("何もしない", "None", buttonList[buttnCount]);
       
    }
}
