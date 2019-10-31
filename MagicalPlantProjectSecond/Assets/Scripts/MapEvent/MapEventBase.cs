//-------------------------------------------------------------------
//マップのクリックしたときの処理、のベース
//-------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public abstract class MapEventBase
{
    //マネージャーから参照用の番号
    public int eventNum;
    //マネージャーから検索用の文字列
    public string eventStr;

    protected GameObject buttonParent;
    //選択時メニューのボタンプレハブ
    protected GameObject buttonPrefab;
    protected List<GameObject> buttonList;

    protected struct EventCode
    {
        public string objText;
        public string eventText;
        public EventCode(string o,string e)
        {
            objText = o;
            eventText = e;
        }
    }
    protected EventCode[] events;
    protected Vector3Int pos; 
    //コンストラクタ君
    public MapEventBase(int num)
     {
        eventNum = num;
        eventStr = "Field";
        buttonParent = GameObject.Find("EventMenu");
        buttonPrefab = Resources.Load<GameObject>("Prefabs/EventButton");
        buttonList = new List<GameObject>();
     }
    //上にポインターを置いた時の描写とか
    public abstract void OnHoverRun(Vector3Int pos);
    //クリックしたときの処理
    public abstract void OnLeftClickRun();
    public abstract void OnRightClickRun();
    protected virtual void MenuButtonMake()
    {
        buttonParent.SetActive(true);
        buttonParent.transform.position = Input.mousePosition;
        foreach (Transform t in buttonParent.transform)
        {
            t.gameObject.SetActive(false);
        }
        int childNum = buttonParent.transform.childCount;
        
        for (int i = 0; i < events.Length + 1; i++)
        {
            if (i > buttonList.Count)
            {
                buttonList.Add(GameObject.Instantiate<GameObject>(buttonPrefab, buttonParent.transform));
                buttonList[buttonList.Count - 1].SetActive(false);
            }
        }
    }
    protected void ButtonTextSet(string objText, string eventText,GameObject g)
    {
        g.SetActive(true);

        g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = objText;
        string t = eventText;
        g.GetComponent<Button>().onClick.AddListener(g.GetComponent<MapEVButton>().ButtonPressd);
        g.GetComponent<MapEVButton>().eventCode = eventText;
        g.GetComponent<MapEVButton>().eventB = this;
    }
    public abstract void EventStart(string text);
}

