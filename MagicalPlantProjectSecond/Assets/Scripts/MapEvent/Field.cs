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
    }
    public override void OnHoverRun(Vector3Int pos)
    {
        //field.ShowPlantData(pos);
    }
    public override void OnLeftClickRun()
    {
        
    }
    public override void OnRightClickRun()
    {
        if (menu.state == MenuState.None)
        {
            field.ShowPlantData(TileManager.GetInstance().MousePosToCell());
        }
    }
}
