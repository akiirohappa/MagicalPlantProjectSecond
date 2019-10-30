//-------------------------------------------------------------
//畑の情報など
//-------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager
{
    Plant[] myField;
    private Vector3Int[] fieldTileData;
    private GameObject plantField;
    PlantDataView view;
    public PlantDataView View { get { return view; } } 

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
        view = new PlantDataView(); 
    }
    //作物の成長
    public void PlantGrowth()
    {
        foreach(Plant p in myField)
        {
            p.nowGrowth += p.growthSpeed;
            if(p.nowGrowth >= 100)
            {
                p.nowGrowth = 100;
                p.plantState = PlantState.Harvest;
            }
            else if(p.soilState == Soil.Dry)
            {
                p.quality -= p.downQuality;
            }
            else if (p.soilState == Soil.Moist)
            {
                p.quality -= p.upQuality;
            }
            p.soilState = Soil.Dry;
        }
    }
    public void SetPlantData(Vector3Int vec,Plant plant)
    {
        int num = GetPlantPos(vec);
        if (num != -1)
        {
            myField[num] = plant;
        }
    }
    //畑の情報表示（仮）
    public void ShowPlantData(Vector3Int vec)
    {
        int num = GetPlantPos(vec);
        if (num != -1)
        {
            view.DataPreview(myField[num]);
        }
    }
    //マップ座標から畑の座標を取得
    public int GetPlantPos(Vector3Int vec)
    {
        for (int i = 0; i < myField.Length; i++)
        {
            if (fieldTileData[i].x == vec.x && fieldTileData[i].y == vec.y)
            {
                return i;
            }
        }
        return -1;
    }
}
