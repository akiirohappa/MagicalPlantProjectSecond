using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaData_Axe : FacilitiesData
{
    public FaData_Axe()
    {
        name = "斧";
        infoText = "畑を耕すのに使った斧。\n使うと畑のサイズが広くなる。";
        valueText = "畑のサイズを広げる。";
        nowLevel = 0;
        maxLevel = 3;
        levelUpPrice = new int[3] { 1000,5000,10000 };
    }
    public override void LevelUpAct()
    {

    }
}
