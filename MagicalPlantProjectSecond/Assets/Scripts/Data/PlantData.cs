using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Plant",menuName ="MyData/Plant")]
public class PlantData : ScriptableObject
{
    public Plant data;
    public string Plantname;
    public Sprite icon;
    public float nowGrowth;
    public float growthSpeed;
    public Soil soilState;
    public int quality;
    public Plant GetPlant()
    {
        if(data == null)
        {
            data = new Plant(Plantname,icon,nowGrowth,growthSpeed,soilState,quality);
        }
        return data;
    }
}
