using System.Collections;
using System.Collections.Generic;
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
    public Plant(string n,Sprite i,float ng, float gs, Soil ss, int q)
    {
        name = n;
        icon = i;
        nowGrowth = ng;
        growthSpeed = gs;
        soilState = ss;
        quality = q;
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
}
