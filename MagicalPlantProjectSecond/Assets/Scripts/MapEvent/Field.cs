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
    MenuManager menu;
    
    public Field(int num) : base(num)
    {
        field = FieldManager.GetInstance();
        menu = GameObject.Find("Manager").GetComponent<MenuManager>();
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
        if (text == events[0].eventText)
        {
            menu.SendMenuButton("ItemSet");
            ItemSetManager setM = (ItemSetManager)menu.MenuManagerB;
            setM.TypeSet(ItemType.Seed);
        }
        if (text == events[1].eventText)
        {
            menu.SendMenuButton("ItemSet");
            ItemSetManager setM = (ItemSetManager)menu.MenuManagerB;
            setM.TypeSet(ItemType.Fertilizer);
        }
        if (text == events[2].eventText)
        {
            field.GetPlantData(pos).soilState = Soil.Moist;
        }
        if (text == events[3].eventText)
        {

        }
        if(text == "None")
        {
            menu.state = MenuState.None;
            foreach (Transform t in buttonParent.transform)
            {
                t.gameObject.SetActive(false);
            }
            buttonParent.SetActive(false);
        }
    }
}
