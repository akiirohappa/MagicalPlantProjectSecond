using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PeforMance",menuName ="PeforManceData")]
public class PeforManceDataBase:ScriptableObject
{
    public string Title;
    public string conditionsText;
    public string rewardText;
    [Multiline(3)]
    public string DeText;
    //実績クリア条件
    public long conditions;
    //実績の数値
    long nowState;
    public long NowState
    {
        get
        {
            return nowState;
        }
        set
        {
            nowState = value;
        }
    }
    public bool Clear;
    //コンストラクタ
    public PeforManceDataBase()
    {
        
    }
    //実績の条件をクリアしたか確認
    public bool PeConditions()
    {
        if (conditions <= nowState)
        {
            Clear = true;
            return true;
        }
        return false;
    }
//    public void Unlock();
    //実績がクリアされてるか確認
    public bool GetIsUnlock(int level)
    {
        PeConditions();
        return Clear;
    }
    public void SaveDataSet(PeforManceSaveData data)
    {
        nowState = data.nowValue;
        Clear = data.Clear;

    }


}
[System.Serializable]
public class PeforManceSaveData
{
    public string Title;
    public long nowValue;
    public bool Clear;
}