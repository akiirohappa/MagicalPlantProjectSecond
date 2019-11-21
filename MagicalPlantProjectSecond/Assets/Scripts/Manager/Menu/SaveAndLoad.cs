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
    string[] saveDatas = new string[10];
    public void GetSaveData()
    {
        for(int i = 0;i < 10; i++)
        {
            string json = PlayerPrefs.GetString(saveDataPassBase + i,"None");
            if(json != "None")
            {
                saveDatas[i] = json;
            }
            else
            {
                saveDatas[i] = null;
            }
        }
    }
    public void Save(int num,SaveData s)
    {
        if (num > 9 && num < 0)
        {
            Debug.Log("Error");
            return;
        }
        string json = JsonUtility.ToJson(s);
        saveDatas[num] = json;
//        Debug.Log(json);
        PlayerPrefs.SetString(saveDataPassBase + num, json);
        PlayerPrefs.Save();
    }
    public SaveData Load(int num)
    {
        if (num > 9 && num < 0)
        {
            Debug.Log("Error");
            return null;
        }
        if(saveDatas == null)
        {
            GetSaveData();
        }
        return JsonUtility.FromJson<SaveData>(saveDatas[num]) ;
    }
    public void SaveDataSet(SaveData sd)
    {
        TimeManager.GetInstance().GetTime().TimeSet(sd.time);
        TimeManager.GetInstance().TimeSet(TimeManager.GetInstance().GetTime());
        PlayerData.GetInstance().Item.SetItemList(sd.myItems.list.ToList());
        Plant[] plants = new Plant[sd.plants.Length];
        for(int i = 0;i < plants.Length; i++)
        {
            plants[i] = new Plant( sd.plants[i]);
        }
        FieldManager.GetInstance().SetFieldData(plants);
        PlayerData.GetInstance().Money = sd.money;
        DontDestroyManager.my.Sound.ConfigSet(sd.config);
    }
}
public class SaveData
{
    public SaveData()
    {
    }
    public void SaveSet()
    {
        time = new TimeForSave(TimeManager.GetInstance().GetTime());
        plants = new PlantDataForSave[25];
        for (int i = 0; i < plants.Length; i++)
        {
            plants[i] = new PlantDataForSave(FieldManager.GetInstance().myField[i]);
        }
        myItems = new ItemListForSave(PlayerData.GetInstance().ListItem);
        money = PlayerData.GetInstance().Money;
        config = new ConfigData();
        config.CoufigLoad();
    }
    public TimeForSave time;
    public ItemListForSave myItems;
    public PlantDataForSave[] plants;
    public ConfigData config;
    public long money;
}