using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelpManager : MenuManagerBase
{
    HelpItem[] helps;
    GameObject helpButton;
    GameObject buttonList;
    TextMeshProUGUI text;
    TextMeshProUGUI pagetext;
    int nowPage;
    int nowHelp;
    List<Button> buttons;
    public HelpManager(MenuManager m) : base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Help").gameObject;
        helpButton = Resources.Load<GameObject>("Prefabs/HelpButton");
        text = myObjct.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        pagetext = myObjct.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        myObjct.transform.GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(() => { PageChange(-1); });
        myObjct.transform.GetChild(1).GetChild(3).GetComponent<Button>().onClick.AddListener(() => { PageChange(1); });
        buttonList = myObjct.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        buttons = new List<Button>();
        TextAsset[] jsons = Resources.LoadAll<TextAsset>("Help");
        helps = new HelpItem[jsons.Length];
        string json;
        for (int i = 0; i < jsons.Length; i++)
        {
            json = jsons[i].ToString();
            helps[i] = JsonUtility.FromJson<HelpItem>(json);
        }
    }
    public override void Open()
    {
        TextReset();
        nowHelp = -1;
        base.Open();
        Button b;
        nowPage = 0;
        foreach(Button bt in buttons)
        {
            bt.gameObject.SetActive(false);
        }
        for(int i = 0;i < helps.Length; i++)
        {
            int ii = i;
            if(i >= buttons.Count)
            {
                b = GameObject.Instantiate(helpButton, buttonList.transform).GetComponent<Button>();
                b.onClick.AddListener(() => { HelpOpen(ii); });
                buttons.Add(b);
            }
            else
            {
                b = buttons[i];
                b.gameObject.SetActive(true);
            }
            b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = helps[i].itemName;

        }
    }
    public override void Button(string state)
    {
        
    }
    public override void Submit()
    {

    }
    public override void Cancel()
    {

    } 
    public void HelpOpen(int i)
    {
        if(nowHelp == i)
        {
            return;
        }
        nowHelp = i;
        TextView(nowHelp, nowPage=0);
    }
    public void PageChange(int i)
    {
        if(nowHelp == -1)
        {
            return;
        }
        if(nowPage + i != -1 && nowPage+i != helps[nowHelp].itemValue.Count)
        {
            nowPage += i;
        }
        TextView(nowHelp, nowPage);
    }
    public void TextView(int helpnum,int pagenum)
    {
        text.text = "<size=50>" + helps[helpnum].itemName + "</size><br>";
        text.text += helps[helpnum].itemValue[pagenum];
        pagetext.text = (pagenum + 1) + "/" + (helps[helpnum].itemValue.Count) + "ページ";
    }
    public void TextReset()
    {
        text.text = "";
        pagetext.text = "0/0ページ";
        nowHelp = -1;
        nowPage = -1;
    }
}
