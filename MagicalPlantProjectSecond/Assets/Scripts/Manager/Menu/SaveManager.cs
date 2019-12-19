//----------------------------------------------------------
//セーブ機能（メイン部分）
//----------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SaveManager:MenuManagerBase
{
    SaveAndLoad sl;
    GameObject saveButtonPref;
    GameObject saveButtonPer;
    GameObject[] saveButtons;
    GameObject saveDataShow;
    int cullentDataNum;
    public SaveManager(MenuManager m):base(m)
    {
        sl = new SaveAndLoad();
        myObjct = GameObject.Find("Menu").transform.Find("Save").gameObject;
        saveButtonPer = myObjct.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        saveDataShow = myObjct.transform.GetChild(2).gameObject;
        saveButtonPref = Resources.Load<GameObject>("Prefabs/SaveButton");
    }
    public override void Open()
    {
        DontDestroyManager.my.Sound.PlaySE("Submit_S");
        base.Open();
        cullentDataNum = -1;
        ButtonSetUp();
    }
    void ButtonSetUp()
    {
        sl.GetSaveData();
        if (saveButtons == null)
        {
            saveButtons = new GameObject[10];
            for (int i = 0; i < saveButtons.Length; i++)
            {
                saveButtons[i] = GameObject.Instantiate(saveButtonPref, saveButtonPer.transform);
                int n = i;
                saveButtons[i].GetComponent<Button>().onClick.AddListener(() => { mManager.ButtonEx(n.ToString());});
            }
        }
        for (int i = 0; i < saveButtons.Length; i++)
        {
            saveButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "セーブデータ" + (i + 1);
            SaveData sd = sl.Load(i);
            if (sd != null)
            {
                saveButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    sd.time.year + "年　" +
                    sd.time.SeasonToStr + "　" +
                    sd.time.day + "日　<br>" +
                    sd.time.hour + "時　" +
                    sd.time.minit.ToString("0") + "分　";
                saveButtons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                    (sd.money / 1000000000000 != 0 ? sd.money % 10000000000000000 / 1000000000000 + "兆" : "") +
                    (sd.money % 1000000000000 / 100000000 != 0 ? sd.money % 1000000000000 / 100000000 + "億<br>" : "") +
                    (sd.money % 100000000 / 10000 != 0 ? sd.money % 100000000 / 10000 + "万" : "") +
                    (sd.money % 10000 != 0 ? (sd.money % 10000).ToString() : "") + "株";
            }
            else
            {
                saveButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "から";
            }
        }
    }
    public override void Submit()
    {
        SaveData s = new SaveData();
        s.SaveSet();
        sl.Save(cullentDataNum, s);
        DontDestroyManager.my.Sound.PlaySE("Submit_L");
        ButtonSetUp();
        SaveDataShow(false);
    }
    public override void Cancel()
    {
        DontDestroyManager.my.Sound.PlaySE("Cancel");
        SaveDataShow(false);
    }
    void SaveDataShow(bool b)
    {
        saveDataShow.SetActive(b);
        if(!b)
        {
            return;
        }
        saveDataShow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "セーブデータ" + (cullentDataNum + 1);
        SaveData sd = sl.Load(cullentDataNum);
        if (sd != null)
        {
            saveDataShow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
               sd.time.year + "年　" +
                sd.time.SeasonToStr + "　" +
                sd.time.day + "日　<br>" +
                sd.time.hour + "時　" +
                sd.time.minit.ToString("0") + "分　";
            saveDataShow.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                (sd.money / 1000000000000 != 0 ? sd.money % 10000000000000000 / 1000000000000 + "兆" : "") +
                (sd.money % 1000000000000 / 100000000 != 0 ? sd.money % 1000000000000 / 100000000 + "億<br>" : "") +
                (sd.money % 100000000 / 10000 != 0 ? sd.money % 100000000 / 10000 + "万" : "") +
                (sd.money % 10000 != 0 ? (sd.money % 10000).ToString() : "") + "株";
        }
        else
        {
            
        }
    }
    public override void Button(string state)
    {
        int num = int.Parse(state);

        if (num > 9 && num < 0)
        {
            Debug.Log("Error");
            return;
        }
        cullentDataNum = num;
        DontDestroyManager.my.Sound.PlaySE("Submit_S");
        SaveDataShow(true);
    }
}
