using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PeforManceType
{
    Money,
    Plant,
    Time,
}
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
    public bool GetIsUnlock()
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
    public Dictionary<PeforManceType, List<PeforManceDataBase>> Peformances;
    public PeforManceDatas()
    {
        Peformances = new Dictionary<PeforManceType, List<PeforManceDataBase>>();
        Peformances[PeforManceType.Money] = new List<PeforManceDataBase>();
        Peformances[PeforManceType.Plant] = new List<PeforManceDataBase>();
        Peformances[PeforManceType.Time] = new List<PeforManceDataBase>();
        PeforManceDataBase[] d = Resources.LoadAll<PeforManceDataBase>("PeforMance/Money");
        PeforManceType key = PeforManceType.Money;
        for (int j = 0; j < 3; j++)
        {
            switch (j)
            {
                case 1:
                    d = Resources.LoadAll<PeforManceDataBase>("PeforMance/Plant");
                    key = PeforManceType.Plant;
                    break;
                case 2:
                    d = Resources.LoadAll<PeforManceDataBase>("PeforMance/Time");
                    key = PeforManceType.Time;
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
        PeforManceType key = PeforManceType.Money;
        List<PeforManceSaveData> data = d.Money;
        for (int j = 0; j < 3; j++)
        {
            switch (j)
            {
                case 1:
                    key = PeforManceType.Plant;
                    data = d.Plant;
                    break;
                case 2:
                    key = PeforManceType.Time;
                    data = d.Time;
                    break;
                default:
                    break;
            }
            for (int i = 0; i < Peformances[key].Count; i++)
            {
                Peformances[key][i].SaveDataSet(data[i]);
            }
        }
    }
    public void DataUnlock(PeforManceType type,long value)
    {
        foreach(PeforManceDataBase data in Peformances[type])
        {
            data.NowState += value;
            data.GetIsUnlock();
        }
    }
}
[System.Serializable]
public class PeforManceSaveDatas
{
    public List<PeforManceSaveData> Money;
    public List<PeforManceSaveData> Plant;
    public List<PeforManceSaveData> Time;
    public PeforManceSaveDatas(PeforManceDatas d)
    {
        Money = new List<PeforManceSaveData>();
        Plant = new List<PeforManceSaveData>();
        Time = new List<PeforManceSaveData>();
        PeforManceType key = PeforManceType.Money;
        for (int j = 0; j < 3; j++)
        {
            switch (j)
            {
                case 1:
                    key = PeforManceType.Plant;
                    break;
                case 2:
                    key = PeforManceType.Time;
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
                        Plant.Add(new PeforManceSaveData(d.Peformances[key][i]));
                        break;
                    case 2:
                        Time.Add(new PeforManceSaveData(d.Peformances[key][i]));
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