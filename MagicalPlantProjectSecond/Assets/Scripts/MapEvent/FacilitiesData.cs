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
    public bool LevelUPPriceCheck()
    {
        long lvPl = levelUpPrice[nowLevel];
        if(PlayerData.GetInstance().Money >= lvPl)
        {
            DontDestroyManager.my.Sound.PlaySE("Submit_S");
            PlayerData.GetInstance().Money -= lvPl;
            return true;
        }
        else
        {
            MainManager.GetInstance.Log.LogMake("お金が足りません！", null);
            DontDestroyManager.my.Sound.PlaySE("Cancel");
            return false;
        }
    }
}
