using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant
{
    public string name;
    public Sprite icon;
    public float nowGrowth;
    public float growthSpeed;
    public int quality;
    public Plant()
    {
        name = "空";
        nowGrowth = 0f;
        growthSpeed = 0f;
        quality = 50;
    }
}
