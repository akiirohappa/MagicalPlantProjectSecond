//----------------------------------------------------------------------
//ロード機能（タイトル）
//----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadManager
{
    GameObject titleobj;
    GameObject myobj;
    SaveAndLoad sl;
    GameObject saveButtonPref;
    GameObject saveButtonPer;
    GameObject[] saveButtons;
    GameObject saveDataShow;
    TitleManager title;
    public int cullentDataNum;
    public LoadManager(TitleManager t)
    {
        title = t;
        sl = new SaveAndLoad();
        titleobj = GameObject.Find("Canvas").transform.Find("Title").gameObject;
        myobj = GameObject.Find("Canvas").transform.Find("LoadMenu").gameObject;
        saveButtonPer = myobj.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        saveDataShow = myobj.transform.GetChild(2).gameObject;
        saveButtonPref = Resources.Load<GameObject>("Prefabs/SaveButton");
    }
    public void Open()
    {
        titleobj.SetActive(false);
        myobj.SetActive(true);
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
                saveButtons[i].GetComponent<Button>().onClick.AddListener(() => { title.SelectSaveData(n); });
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
    public void SaveDataShow(bool b)
    {
        
        if (!b)
        {
            saveDataShow.SetActive(b);
            return;
        }
        if(cullentDataNum > 9&& cullentDataNum < 0)
        {
            return;
        }
        SaveData sd = sl.Load(cullentDataNum);
        if (sd != null)
        {
            saveDataShow.SetActive(b);
            saveDataShow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "セーブデータ" + (cullentDataNum + 1);
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
    public SaveData GetSaveData()
    {
        return sl.Load(cullentDataNum);
    }
}
