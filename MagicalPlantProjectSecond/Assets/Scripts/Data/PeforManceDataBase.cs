using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PeforManceDataBase
{
    public string[] Title;
    public string conditionsText;
    public string rewardText;

    //実績クリア条件
    public long[] conditions;
    //実績の現在レベル
    public int nowLevel;
    //実績の数値
    public long nowState;
    public bool[] Clear;
    //コンストラクタ
    public PeforManceDataBase()
    {
        
    }
    //実績の条件をクリアしたか確認
    public abstract bool PeConditions();
    //実績がクリアされてるか確認
    public bool GetIsUnlock(int level)
    {
        return Clear[level];
    }
    //今条件がいくつか取得
    public abstract long GetNowValue();

}
[System.Serializable]
public class PeforManceSaveData
{
    public string Title;
    public string nowValue;
    public string nowLevel;
    public bool[] Clear;
}