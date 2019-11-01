//----------------------------------------------------
//畑部分
//----------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Field:MapEventBase
{
    FieldManager field;
    PlantDataView view;
    public Field(int num) : base(num)
    {
        eventStr = "Field";
        field = FieldManager.GetInstance();
        view = FieldManager.GetInstance().View;
        events = new EventCode[] 
        {
            new EventCode("種を植える", "Seed"),
            new EventCode("肥料を与える", "Fertilizer"),
            new EventCode("水を与える", "Water"),
            new EventCode("収穫する", "Harvest")
        };
    }
    public override void OnHoverRun(Vector3Int pos)
    {
        //field.ShowPlantData(pos);
    }
    public override void OnLeftClickRun()
    {
        menu.state = MenuState.EventSelect;
        field.ShowPlantData(TileManager.GetInstance().MousePosToCell());
        pos = TileManager.GetInstance().MousePosToCell();
        MenuButtonMake();
    }
    public override void OnRightClickRun()
    {
        if (menu.state == MenuState.None)
        {
            field.ShowPlantData(TileManager.GetInstance().MousePosToCell());
        }
    }
    protected override void MenuButtonMake()
    {
        base.MenuButtonMake();
        int buttnCount = 0;
        switch (field.GetPlantData(pos).plantState)
        {
            case PlantState.None:
                ButtonTextSet(events[0].objText, events[0].eventText, buttonList[buttnCount++]);
                break;
            case PlantState.Growth:
                ButtonTextSet(events[2].objText, events[2].eventText, buttonList[buttnCount++]);
                break;
            case PlantState.Harvest:
                ButtonTextSet(events[3].objText, events[3].eventText, buttonList[buttnCount++]);
                break;
            default:
                break;
        }
        ButtonTextSet(events[1].objText, events[1].eventText, buttonList[buttnCount++]);
        ButtonTextSet("何もしない", "None", buttonList[buttnCount++]);
    }
    public override void EventStart(string text)
    {
        //種を植える
        if (text == events[0].eventText)
        {
            menu.SendMenuButton("ItemSet");
            menu.MenuManagerB.Open(pos);
            view.PlantVSetActive(false);
            ItemSetManager setM = (ItemSetManager)menu.MenuManagerB;
            setM.TypeSet(ItemType.Seed);
        }
        //肥料をあげる
        if (text == events[1].eventText)
        {
            menu.SendMenuButton("ItemSet");
            view.PlantVSetActive(false);
            ItemSetManager setM = (ItemSetManager)menu.MenuManagerB;
            setM.TypeSet(ItemType.Fertilizer);
        }
        //水やり
        if (text == events[2].eventText)
        {
            field.GetPlantData(pos).soilState = Soil.Moist;
            view.PlantVSetActive(false);
            menu.state = MenuState.None;
        }
        //収穫
        if (text == events[3].eventText)
        {
            field.Harvest(pos);
            menu.state = MenuState.None;
        }
        if(text == "None")
        {
            view.PlantVSetActive(false);
            menu.state = MenuState.None;
        }
        MenuClose();
    }
}
