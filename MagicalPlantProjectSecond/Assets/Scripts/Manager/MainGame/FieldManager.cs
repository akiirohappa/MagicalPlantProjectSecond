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
        
        fieldTileData = TileManager.GetInstance().TileFieldGet();
        view = new PlantDataView();
        harvest = new HarvestCalc();
        FieldUnlock(0);
    }
    //水の量の減少
    public void WaterDown()
    {
        List<FertilizerData> fCp = new List<FertilizerData>();
        foreach (Plant p in myField)
        {
            p.soilWaterValue -= 2;
            if(p.soilWaterValue < 0)
            {
                p.soilWaterValue = 0;
            }
            if(p.soilWaterValue == 0)
            {
                p.soilState = Soil.VeryDry;
            }
            else if(p.soilWaterValue <= 50)
            {
                p.soilState = Soil.Dry;
            }
            else if (p.soilWaterValue < 100)
            {
                p.soilState = Soil.Moist;
            }
            else
            {
                p.soilState = Soil.VeryMoist;
            }
            foreach(FertilizerData f in p.fertilizers)
            {
                f.getValue--;
                if (f.getValue <= 0)
                {
                    fCp.Add(f);
                }
            }
            foreach(FertilizerData f in fCp)
            {
                p.fertilizers.Remove(f);
            }
            fCp.Clear();
        }
    }
    //作物の成長
    public void PlantGrowth()
    {
        int exQuality;
        int upQuality;
        int downQuality;
        float upSpeed;
        foreach (Plant p in myField)
        {
            exQuality = 0;
            upQuality = 0;
            downQuality = 0;
            upSpeed = 0;
            switch (p.plantState)
            {
                case PlantState.Growth:
                    foreach(FertilizerData f in p.fertilizers)
                    {
                        exQuality += f.quality;
                        upQuality += f.upQuality;
                        downQuality += f.downQuality;
                        upSpeed += f.growthSpeed;
                    }
                    p.nowGrowth += (p.growthSpeed+upSpeed);
                    if (p.nowGrowth >= 100)
                    {
                        p.nowGrowth = 100;
                        p.quality += exQuality;
                        p.plantState = PlantState.Harvest;
                    }
                    else if (p.soilState == Soil.VeryDry)
                    {
                        p.quality -= (int)(p.downQuality *1.5)-downQuality;
                    }
                    else if (p.soilState == Soil.Dry)
                    {
                        p.quality -= p.downQuality-downQuality;
                    }
                    else
                    {
                        p.quality += (TimeManager.GetInstance().Time.nowSeason == p.jastSeason ? 2:1)*p.upQuality+upQuality;
                    }
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
                    TileManager.GetInstance().ReWritePlantTile(myField[i].plantType,tile, TileManager.GetInstance().TileFieldGet()[i]);
                    break;
                case PlantState.Harvest:
                    TileManager.GetInstance().ReWritePlantTile(myField[i].plantType, PlantTileData.Hundred, TileManager.GetInstance().TileFieldGet()[i]);
                    break;
                default:

                    break;
            }
        }
    }
    public void SetPlantData(Vector3Int vec,Item item)
    {
        int num = GetPlantPos(vec);
        if (num != -1)
        {
            myField[num].PlantDataSet(item);
            TileSet(vec, myField[num]);
        }
    }
    void TileSet(Vector3Int vec,Plant p)
    {
        if (p.plantState == PlantState.None)
        {
            TileManager.GetInstance().ReWritePlantTile(p.plantType, PlantTileData.None, vec);
        }
        else
        {
            TileManager.GetInstance().ReWritePlantTile(p.plantType, PlantTileData.Zero, vec);
        }
    }
    public Plant GetPlantData(Vector3Int vec)
    {
        int num = GetPlantPos(vec);
        if (num != -1)
        {
            return myField[num];
        }
        else
        {
            return null;
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
        TileSet(vec, pl);
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
                    TileManager.GetInstance().ReWritePlantTile(myField[i].plantType, PlantTileData.None, TileManager.GetInstance().TileFieldGet()[i]);
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
                    TileManager.GetInstance().ReWritePlantTile(myField[i].plantType, tile, TileManager.GetInstance().TileFieldGet()[i]);
                    break;
                case PlantState.Harvest:
                    TileManager.GetInstance().ReWritePlantTile(myField[i].plantType, PlantTileData.Hundred, TileManager.GetInstance().TileFieldGet()[i]);
                    break;
                default:

                    break;
            }
        }
    }
    //畑のサイズ拡張
    public void FieldUnlock(int level)
    {
        for (int i = 0; i < 25; i++)
        {
            if (i % 5 >= 5-2-level && i/5 <= level + 1 && myField[i].plantState == PlantState.DontUse)
            {
                myField[i].plantState = PlantState.None;
                TileManager.GetInstance().ReWritePlantTile(myField[i].plantType, PlantTileData.None, fieldTileData[i]);
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
    public PlantType plantType;
    public int soilWaterValue;
    public SeasonData jastSeason;
    public List<FertilizerSaveData> fertilizers;
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
        plantType = p.plantType;
        soilWaterValue = p.soilWaterValue;
        jastSeason = p.jastSeason;
        fertilizers = new List<FertilizerSaveData>();
        foreach(FertilizerData f in p.fertilizers)
        {
            fertilizers.Add(new FertilizerSaveData(f));
        }
    }
}
