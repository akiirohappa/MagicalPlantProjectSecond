//-------------------------------------------------------------
//畑の情報など
//-------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager
{
    public Plant[] myField;
    private Vector3Int[] fieldTileData;
    private GameObject plantField;
    PlantDataView view;
    public PlantDataView View { get { return view; } }
    private HarvestCalc harvest;
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

    }
    public void Start()
    {
        myField = new Plant[25];
        for (int i = 0; i < 25; i++)
        {
            myField[i] = new Plant();
        }
        myField[4] = new Plant(PlantState.None);
        myField[3] = new Plant(PlantState.None);
        myField[9] = new Plant(PlantState.None);
        myField[8] = new Plant(PlantState.None);
        fieldTileData = TileManager.GetInstance().TileFieldGet();
        view = new PlantDataView();
        harvest = new HarvestCalc();
        TileManager.GetInstance().ReWritePlantTile(PlantTileData.None, fieldTileData[4]);
        TileManager.GetInstance().ReWritePlantTile(PlantTileData.None, fieldTileData[3]);
        TileManager.GetInstance().ReWritePlantTile(PlantTileData.None, fieldTileData[9]);
        TileManager.GetInstance().ReWritePlantTile(PlantTileData.None, fieldTileData[8]);
    }
    //作物の成長
    public void PlantGrowth()
    {
        foreach(Plant p in myField)
        {
            switch (p.plantState)
            {
                case PlantState.Growth:
                    p.nowGrowth += p.growthSpeed;
                    if (p.nowGrowth >= 100)
                    {
                        p.nowGrowth = 100;
                        p.plantState = PlantState.Harvest;
                    }
                    else if (p.soilState == Soil.Dry)
                    {
                        p.quality -= p.downQuality;
                    }
                    else if (p.soilState == Soil.Moist)
                    {
                        p.quality += p.upQuality;
                        Debug.Log(p.quality);
                    }
                    p.soilState = Soil.Dry;

                    break;
                default:
                    break;
            }
            
        }
        for (int i = 0; i < myField.Length; i++)
        {
            switch (myField[i].plantState)
            {
                case PlantState.Growth:
                    PlantTileData tile = PlantTileData.Zero;
                    if (myField[i].nowGrowth < 20)
                    {
                        tile = PlantTileData.Zero;
                    }
                    else if (myField[i].nowGrowth < 50)
                    {
                        tile = PlantTileData.Twenty;
                    }
                    else if (myField[i].nowGrowth < 70)
                    {
                        tile = PlantTileData.Fifty;
                    }
                    else
                    {
                        tile = PlantTileData.Seventy;
                    }
                    TileManager.GetInstance().ReWritePlantTile(tile, TileManager.GetInstance().TileFieldGet()[i]);
                    break;
                case PlantState.Harvest:
                    TileManager.GetInstance().ReWritePlantTile(PlantTileData.Hundred, TileManager.GetInstance().TileFieldGet()[i]);
                    break;
                default:

                    break;
            }
        }
    }
    public void SetPlantData(Vector3Int vec,Plant plant)
    {
        int num = GetPlantPos(vec);
        if (num != -1)
        {
            myField[num] = plant;
            if (plant.plantState == PlantState.None)
            {
                TileManager.GetInstance().ReWritePlantTile(PlantTileData.None, vec);
            }
            else
            {
                TileManager.GetInstance().ReWritePlantTile(PlantTileData.Zero, vec);
            }
            
        }
    }
    public Plant GetPlantData(Vector3Int vec)
    {
        int num = GetPlantPos(vec);
        if (num != -1)
        {
            return myField[num];
        }
        else return null;
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
    //収穫
    public void Harvest(Vector3Int vec)
    {
        Debug.Log(vec);
        Plant pl = GetPlantData(vec);
        Debug.Log(pl.name);
        if (pl.name == "から")
        {
            Debug.Log("無を取得した");
            return;
        }
        Item it = harvest.Harvest(pl);
        PlayerData.GetInstance().Item.ItemGet(it,it.getValue);
        MainManager.GetInstance.Log.LogMake(it.itemName + "を手に入れた！", it.icon);
        pl.Reset();
        SetPlantData(vec, pl);
    }
    //作物データセット
    public void SetFieldData(Plant[] pls)
    {
        myField = pls;
        for (int i = 0;i < pls.Length; i++)
        {
            switch (pls[i].plantState)
            {
                case PlantState.None:
                    TileManager.GetInstance().ReWritePlantTile(PlantTileData.None, TileManager.GetInstance().TileFieldGet()[i]);
                    break;
                case PlantState.Growth:
                    PlantTileData tile = PlantTileData.Zero;
                    if (pls[i].nowGrowth < 20)
                    {
                        tile = PlantTileData.Zero
;
                    }
                    else if (pls[i].nowGrowth < 50)
                    {
                        tile = PlantTileData.Twenty;
                    }
                    else if (pls[i].nowGrowth < 70)
                    {
                        tile = PlantTileData.Fifty;
                    }
                    else
                    {
                        tile = PlantTileData.Seventy;
                    }
                    TileManager.GetInstance().ReWritePlantTile(tile, TileManager.GetInstance().TileFieldGet()[i]);
                    break;
                case PlantState.Harvest:
                    TileManager.GetInstance().ReWritePlantTile(PlantTileData.Hundred, TileManager.GetInstance().TileFieldGet()[i]);
                    break;
                default:

                    break;
            }
        }
    }
}
[System.Serializable]
public class PlantDataForSave
{
    public string name;
    public Sprite icon;
    public float nowGrowth;
    public float growthSpeed;
    public Soil soilState;
    public int quality;
    public int upQuality;
    public int downQuality;
    public PlantState plantState;
    public string info;
    public int defValue;
    public int getValue;
    public PlantDataForSave(Plant p)
    {
        name = p.name;
        icon = p.icon;
        nowGrowth = p.nowGrowth;
        growthSpeed = p.growthSpeed;
        soilState = p.soilState;
        quality = p.quality;
        upQuality = p.upQuality;
        downQuality = p.downQuality;
        plantState = p.plantState;
        info = p.info;
        defValue = p.defValue;
        getValue = p.getValue;
    }
}
