//-------------------------------------------------------------------------------------
//セーブ・ロード機能の実行部分
//-------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class SaveAndLoad
{
    string saveDataPassBase = "SaveData";
    SaveData[] saveDatas = new SaveData[10]; 
    public void GetSaveData()
    {
        for(int i = 0;i < 10; i++)
        {
            string json = PlayerPrefs.GetString(saveDataPassBase + i,"None");
            if(json != "None")
            {
                saveDatas[i] = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                saveDatas[i] = null;
            }
        }
    }
    public void Save(int num,SaveData s)
    {
        if(num > 10)
        {
            Debug.Log("Error");
            return;
        }
        string json = JsonUtility.ToJson(s);
        Debug.Log(json);
        PlayerPrefs.SetString(saveDataPassBase + num, json);
        PlayerPrefs.Save();
    }
    public SaveData Load(int num)
    {
        if (num > 10)
        {
            Debug.Log("Error");
            return null;
        }
        if(saveDatas == null)
        {
            GetSaveData();
        }
        return saveDatas[num];
    }
}
public class SaveData
{
    public SaveData(PlayerData pl,FieldManager fi)
    {
        time = TimeManager.GetInstance().GetTime();
        plants = fi.myField;
        myItems = pl.Item;
        money = pl.Money;
    }
    public TimeData time;
    public ItemList myItems;
    public Plant[] plants;
    public long money;
}