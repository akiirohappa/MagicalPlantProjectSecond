﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Soil
{
    Dry,
    Moist,
}

public class Plant
{
    public string name;
    public Sprite icon;
    public float nowGrowth;
    public float growthSpeed;
    public Soil soilState;
    public int quality;
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
}
