//-------------------------------------------------------------------
//マップのクリックしたときの処理、のベース
//-------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public abstract class MapEventBase
{
    //マネージャーから参照用の番号
    public int eventNum;
    //マネージャーから検索用の文字列
    public string eventStr;
    
    public GameObject buttonParent;
    //選択時メニューのボタンプレハブ
    protected GameObject buttonPrefab;
    protected List<GameObject> buttonList;

    protected MenuManager menu;
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
    protected FacilitiesData data;
    public FacilitiesData Data
    {
        get { return data; }
    }
    //コンストラクタ君
    public MapEventBase(int num)
     {
        menu = GameObject.Find("Manager").GetComponent<MenuManager>();
        eventNum = num;
        
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
        if(buttonList.Count == 0)
        {
            foreach (Transform t in buttonParent.transform)
            {
                buttonList.Add(t.gameObject);
            }
        }
        buttonParent.SetActive(true);
        Vector3 mousePos = Input.mousePosition;
        mousePos.x += 16;
        mousePos.y += 16;
        buttonParent.transform.position = mousePos;
        foreach (Transform t in buttonParent.transform)
        {
            t.gameObject.SetActive(false);
        }
        int childNum = buttonParent.transform.childCount;
        for (int i = 0; i <= events.Length + 1; i++)
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
        if(g.GetComponent<EventTrigger>().triggers.Count == 0)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((x) => MapEventManager.GetInstance().ButtonOnPointer());
            g.GetComponent<EventTrigger>().triggers.Add(entry);
        }

    }
    public abstract void EventStart(string text);
    public void MenuClose()
    {
        foreach (Transform t in buttonParent.transform)
        {
            t.gameObject.SetActive(false);
        }
        buttonParent.SetActive(false);
        
    }
}

