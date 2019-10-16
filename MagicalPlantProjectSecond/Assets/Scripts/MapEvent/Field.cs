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
    public Field(int num) : base(num)
    {
        field = FieldManager.GetInstance();
    }
    public override void OnHoverRun(Vector3Int pos)
    {
        field.ShowPlantData(pos);
    }
    public override void OnClickRun()
    {

    }
}
