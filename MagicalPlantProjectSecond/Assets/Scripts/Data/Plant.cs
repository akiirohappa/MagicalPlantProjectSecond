using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public enum Soil
{
    Dry,
    Moist,
}
public enum PlantState
{
    None,
    Growth,
    Harvest,
    DontUse,
}
[System.Serializable]
public class Plant
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
    public Plant(PlantState p = PlantState.DontUse)
    {
        name = "から";
        nowGrowth = 0f;
        growthSpeed = 0f;
        quality = 50;
        plantState = p;
    }
    public Plant(PlantDataForSave p)
    {
        name=p.name;
        icon= p.icon;
        nowGrowth= p.nowGrowth;
        growthSpeed= p.growthSpeed;
        soilState= p.soilState;
        quality = p.quality;
        upQuality = p.upQuality;
        downQuality= p.downQuality;
        plantState= p.plantState;
        info= p.info;
        defValue= p.defValue;
        getValue= p.getValue;
    }
    public Plant(Item i)
    {
        Regex reg = new Regex("の種");
        name = reg.Replace(i.itemName, "");
        icon = i.icon;
        nowGrowth = 0;
        growthSpeed = i.growthSpeed;
        soilState = Soil.Dry;
        quality = i.quality;
        upQuality = i.upQuality;
        downQuality = i.downQuality;
        plantState = PlantState.Growth;
        info = i.info;
        defValue = i.sellPrice;
        getValue = i.getValue;
    }
    public void Reset()
    {
        name = "から";
        nowGrowth = 0f;
        growthSpeed = 0f;
        quality = 50;
        plantState = PlantState.None;
    }
}
