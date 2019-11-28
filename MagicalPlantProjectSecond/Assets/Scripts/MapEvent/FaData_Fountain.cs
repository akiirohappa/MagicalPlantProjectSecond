using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaData_Fountain : FacilitiesData
{
    public FaData_Fountain()
    {
        name = "噴水";
        infoText = "なぜか置かれている噴水。\n使うと畑全部に水をやることができる。";
        valueText = "畑全部に水をやることができる。";
        nowLevel = 0;
        maxLevel = 1;
        levelUpPrice = new int[1] {1200};
    }
    public override void LevelUpAct()
    {
        nowLevel++;
    }
}
