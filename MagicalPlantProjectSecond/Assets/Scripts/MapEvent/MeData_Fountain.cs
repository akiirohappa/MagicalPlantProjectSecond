using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeData_Fountain:MapEventBase
{
    FaData_Fountain data;
    public MeData_Fountain(int num):base(num)
    {
        data = new FaData_Fountain();
    }
    public override void OnHoverRun(Vector3Int pos)
    {

    }
    public override void OnLeftClickRun()
    {

    }
    public override void OnRightClickRun()
    {
        if (data.nowLevel == 1)
        {
            for(int i = 0;i < FieldManager.GetInstance().myField.Length; i++)
            {
                if(FieldManager.GetInstance().myField[i].plantState != PlantState.DontUse)
                {
                    FieldManager.GetInstance().myField[i].soilState = Soil.Moist;
                }
            }
            
        }
    }
    public override void EventStart(string text)
    {

    }
}
