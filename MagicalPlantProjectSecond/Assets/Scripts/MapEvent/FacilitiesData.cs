using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FacilitiesData
{
    public int nowLevel;
    public int maxLevel;
    public int[] value;
    public int[] levelUpPrice;
    public string name;
    public string infoText;
    public string valueText;
    public FacilitiesData()
    {
        
    }
    public abstract void LevelUpAct();
}
