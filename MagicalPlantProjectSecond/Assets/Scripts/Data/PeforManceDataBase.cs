using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PeforMance",menuName ="PeforManceData")]
public class PeforManceDataBase:ScriptableObject
{
    public string Title;
    public Sprite icon;
    public string conditionsText;
    public string rewardText;
    public ItemData rewardItem;
    public int rewardItemNum;
    public bool rewardGet;
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
        rewardGet = data.rewardGet;
    }
    public PeforManceSaveData SaveDataGet()
    {
        PeforManceSaveData data = new PeforManceSaveData(this);
        return data;
    }
    void OnValidate()
    {
        if(rewardItemNum <= 0)
        {
            rewardItemNum = 1;
        }
    }

}
public class PeforManceDatas
{
    public Dictionary<string, List<PeforManceDataBase>> Peformances;
    public PeforManceDatas()
    {
        Peformances = new Dictionary<string, List<PeforManceDataBase>>();
        Peformances["Money"] = new List<PeforManceDataBase>();
        Peformances["Plant"] = new List<PeforManceDataBase>();
        Peformances["Water"] = new List<PeforManceDataBase>();
        Peformances["Time"] = new List<PeforManceDataBase>();
        PeforManceDataBase[] d = Resources.LoadAll<PeforManceDataBase>("PeforMance/Money");
        string key = "Money";
        for (int j = 0; j < 4; j++)
        {
            switch (j)
            {
                case 1:
                    d = Resources.LoadAll<PeforManceDataBase>("PeforMance/Plant");
                    key = "Plant";
                    break;
                case 2:
                    d = Resources.LoadAll<PeforManceDataBase>("PeforMance/Water");
                    key = "Water";
                    break;
                case 3:
                    d = Resources.LoadAll<PeforManceDataBase>("PeforMance/Time");
                    key = "Time";
                    break;
                default:
                    break;
            }
            for (int i = 0; i < d.Length; i++)
            {
                Peformances[key].Add(d[i]);
                Peformances[key][i].NowState = 0;
                Peformances[key][i].Clear = false;
                Peformances[key][i].rewardGet = false;
            }
        }
    }
    public void DataSet(PeforManceSaveDatas d)
    {
        string key = "Money";
        List<PeforManceSaveData> data = d.Money;
        for (int j = 0; j < 4; j++)
        {
            switch (j)
            {
                case 1:
                    key = "Plant";
                    data = d.Plant;
                    break;
                case 2:
                    key = "Water";
                    data = d.Water;
                    break;
                case 3:
                    key = "Time";
                    data = d.Time;
                    break;
                default:
                    break;
            }
            for (int i = 0; i < data.Count; i++)
            {
                Peformances[key][i].SaveDataSet(data[i]);
            }
        }
    }
}
[System.Serializable]
public class PeforManceSaveDatas
{
    public List<PeforManceSaveData> Money;
    public List<PeforManceSaveData> Plant;
    public List<PeforManceSaveData> Water;
    public List<PeforManceSaveData> Time;
    public PeforManceSaveDatas(PeforManceDatas d)
    {
        Money = new List<PeforManceSaveData>();
        Plant = new List<PeforManceSaveData>();
        Water = new List<PeforManceSaveData>();
        Time = new List<PeforManceSaveData>();
        string key = "Money";
        for (int j = 0; j < 4; j++)
        {
            switch (j)
            {
                case 1:
                    key = "Plant";
                    break;
                case 2:
                    key = "Water";
                    break;
                case 3:
                    key = "Time";
                    break;
                default:
                    break;
            }
            for (int i = 0; i < d.Peformances[key].Count; i++)
            {
                switch (j)
                {
                    case 0:
                        Money.Add(new PeforManceSaveData(d.Peformances[key][i]));
                        break;
                    case 1:
                        Money.Add(new PeforManceSaveData(d.Peformances[key][i]));
                        break;
                    case 2:
                        Money.Add(new PeforManceSaveData(d.Peformances[key][i]));
                        break;
                    case 3:
                        Money.Add(new PeforManceSaveData(d.Peformances[key][i]));
                        break;
                }
                
            }
        }
    }
}
[System.Serializable]
public class PeforManceSaveData
{
    public string Title;
    public long nowValue;
    public bool Clear;
    public bool rewardGet;
    public PeforManceSaveData(PeforManceDataBase data)
    {
        Title = data.Title;
        nowValue = data.NowState;
        Clear = data.Clear;
        rewardGet = data.rewardGet;
    }
}