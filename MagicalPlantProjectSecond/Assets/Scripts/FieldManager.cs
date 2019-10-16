﻿//-------------------------------------------------------------
//畑の情報など
//-------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager
{
    Plant[] myField;
    Vector3Int[] fieldTileData;
    private static FieldManager _field;
    public static FieldManager GetInstance()
    {
        if(_field == null)
        {
            _field = new FieldManager();
        }
        return _field;
    }
    private FieldManager()
    {
        myField = new Plant[25];
        for (int i = 0; i < 25; i++)
        {
            myField[i] = new Plant();
        }
        fieldTileData = TileManager.GetInstance().TileFieldGet();
    }
    //畑の情報表示（仮）
    public void ShowPlantData(Vector3Int vec)
    {
        for(int i = 0;i < myField.Length;i++)
        {
            if(fieldTileData[i].x == vec.x && fieldTileData[i].y == vec.y)
            {
                Debug.Log(myField[i].name);
            }
        }
    }
}
