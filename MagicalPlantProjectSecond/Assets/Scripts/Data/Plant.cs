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


    public Plant()
    {
        name = "から";
        nowGrowth = 0f;
        growthSpeed = 0f;
        quality = 50;
    }
    public Plant(PlantData p)
    {
        name = p.Plantname;
        icon = p.icon;
        nowGrowth = p.nowGrowth;
        growthSpeed = p.growthSpeed;
        soilState = p.soilState;
        quality = p.quality;
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
    }
}
