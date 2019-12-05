using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaData_Bed : FacilitiesData
{
    public FaData_Bed()
    {
        name = "ベッド";
        infoText = "家に備え付けられていたベッド。\n使うと時間が加速する。";
        valueText = "時間が加速する。";
        nowLevel = 0;
        maxLevel = 3;
        levelUpPrice = new int[3] { 100,500,1000 };
    }
    public override void LevelUpAct()
    {

    }
}
