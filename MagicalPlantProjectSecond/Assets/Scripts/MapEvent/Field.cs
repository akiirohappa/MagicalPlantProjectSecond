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
    MapEventManager map;
    Vector3 pp;
    public Field(int num) : base(num)
    {
        eventStr = "Field";
        field = FieldManager.GetInstance();
        view = FieldManager.GetInstance().View;
        map = MapEventManager.GetInstance(); 
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
        switch (field.GetPlantData(pos).plantState)
        {
            case PlantState.None:
                map.Bar.SetHotBar("施設メニュー", "種を植える");
                break;
            case PlantState.Growth:
                map.Bar.SetHotBar("施設メニュー", "水をあげる");
                break;
            case PlantState.Harvest:
                map.Bar.SetHotBar("施設メニュー", "収穫する");
                break;
            default:
                map.Bar.SetHotBar("施設メニュー", "");
                break;
        }
    }
    public override void OnLeftClickRun()
    {
        menu.ButtonToMain();
        menu.State = MenuState.EventSelect;
        field.ShowPlantData(TileManager.GetInstance().MousePosToCell());
        pos = TileManager.GetInstance().MousePosToCell();
        pp = TileManager.GetInstance().CellToWorldPos(pos);
        pp.x += 0.5f;
        pp.y += 0.5f;
        MenuButtonMake();
    }
    public override void OnRightClickRun()
    {
        pos = TileManager.GetInstance().MousePosToCell();
        pp = TileManager.GetInstance().CellToWorldPos(pos);
        pp.x += 0.5f;
        pp.y += 0.5f;
        switch (field.GetPlantData(pos).plantState)
        {
            case PlantState.None:
                EventStart(events[0].eventText);
                break;
            case PlantState.Growth:
                EventStart(events[2].eventText);
                break;
            case PlantState.Harvest:
                EventStart(events[3].eventText);
                break;
            default:
                break;
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
                ButtonTextSet(events[1].objText, events[1].eventText, buttonList[buttnCount++]);
                break;
            case PlantState.Growth:
                ButtonTextSet(events[2].objText, events[2].eventText, buttonList[buttnCount++]);
                ButtonTextSet(events[1].objText, events[1].eventText, buttonList[buttnCount++]);
                break;
            case PlantState.Harvest:
                ButtonTextSet(events[3].objText, events[3].eventText, buttonList[buttnCount++]);
                ButtonTextSet(events[1].objText, events[1].eventText, buttonList[buttnCount++]);
                break;
            default:
                break;
        }
        ButtonTextSet("何もしない", "None", buttonList[buttnCount++]);
    }
    public override void EventStart(string text)
    {
        //種を植える
        if (text == events[0].eventText)
        {
            menu.SendMenuButton("ItemSet");
            menu.MenuManagerB.Open(pos);
            ItemSetManager setM = (ItemSetManager)menu.MenuManagerB;
            setM.TypeSet(ItemType.Seed);
            
        }
        //肥料をあげる
        if (text == events[1].eventText)
        {
            menu.SendMenuButton("ItemSet");
            menu.MenuManagerB.Open(pos);
            ItemSetManager setM = (ItemSetManager)menu.MenuManagerB;
            setM.TypeSet(ItemType.Fertilizer);
        }
        //水やり
        if (text == events[2].eventText)
        {
            field.GetPlantData(pos).soilWaterValue = 100;
            field.GetPlantData(pos).soilState = Soil.VeryMoist;
            menu.State = MenuState.None;
            MainManager.GetInstance.Particle.PaticleMake(MainManager.GetInstance.Particle.Particle[1], pp);
            DontDestroyManager.my.Sound.PlaySE("Water");
        }
        //収穫
        if (text == events[3].eventText)
        {
            field.Harvest(pos);
            menu.State = MenuState.None;
            MainManager.GetInstance.Particle.PaticleMake(MainManager.GetInstance.Particle.Particle[0], pp);
            DontDestroyManager.my.Sound.PlaySE("Dig");
        }
        if(text == "None")
        {
            menu.State = MenuState.None;
        }
        MenuClose();
        view.PlantVSetActive(false);
        MapEventManager.GetInstance().buttonPressd = false;
    }
}
