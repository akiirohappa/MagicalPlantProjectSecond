using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Plant",menuName ="MyData/Plant")]
public class PlantData : ScriptableObject
{
    public Plant data;
    public Plant GetPlant()
    {
        return data;
    }
}
