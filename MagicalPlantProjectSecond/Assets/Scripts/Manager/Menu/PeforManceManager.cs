using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PeforManceMenuState
{
    PeforMance,
    Library,
}
public class PeforManceManager : MenuManagerBase
{
    PeforManceMenuState state;
    List<Button> ItemButtons;
    List<Button> PeformanceButtons;
    public PeforManceDatas datas;
    public GameObject listParent;
    public GameObject peforManceButton;
    Button ItemPref;

    public PeforManceManager(MenuManager m):base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Peformance").gameObject;
        ItemButtons = new List<Button>();
        PeformanceButtons = new List<Button>();
        datas = PlayerData.GetInstance().PD;
        peforManceButton = Resources.Load<GameObject>("Prefabs/PefoeManceButton");
        listParent = myObjct.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject;
        PeforManceType key = PeforManceType.Money;
        int backNum = 0;
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
            for (int i = 0; i < datas.Peformances[key].Count; i++)
            {
                PeformanceButtons.Add(GameObject.Instantiate(peforManceButton, listParent.transform).GetComponent<Button>());
                PeformanceButtons[backNum + i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = datas.Peformances[key][i].Title;
                PeformanceButtons[backNum + i].gameObject.SetActive(false);
                
            }
            backNum += datas.Peformances[key].Count;
        }
        myObjct.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => StateChange(PeforManceMenuState.PeforMance));
        myObjct.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => StateChange(PeforManceMenuState.Library));
        ItemPref = Resources.Load<Button>("Prefabs/PeItemButton");
    }
    public override void Open()
    {
        base.Open();
        state = PeforManceMenuState.PeforMance;
        MenuSet();
    }
    public override void Submit()
    {
        
    }
    public override void Cancel()
    {

    }
    public override void Button(string state)
    {
        
    }
    public void StateChange(PeforManceMenuState st)
    {
        if(st != state)
        {
            state = st;
            MenuSet();
        }
    }
    void MenuSet()
    {
        var buttons = (state == PeforManceMenuState.PeforMance ? PeformanceButtons:ItemButtons);
        int count = 0;
        ParagraphSet();
        foreach (Button b in PeformanceButtons)
        {
            b.gameObject.SetActive(false);
        }
        foreach (Button b in ItemButtons)
        {
            b.gameObject.SetActive(false);
        }
        if (state == PeforManceMenuState.PeforMance)
        {
            myObjct.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "実績";
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
                for (int i = 0; i < datas.Peformances[key].Count; i++)
                {
                    buttons[count].gameObject.SetActive(true);
                    buttons[count].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = datas.Peformances[key][i].Title;
                    buttons[count].transform.GetChild(1).GetComponent<Slider>().maxValue = datas.Peformances[key][i].conditions;
                    buttons[count].transform.GetChild(1).GetComponent<Slider>().minValue = 0;
                    buttons[count].transform.GetChild(1).GetComponent<Slider>().value = datas.Peformances[key][i].NowState;
                    buttons[count].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = datas.Peformances[key][i].NowState +"/\n" + datas.Peformances[key][i].conditions;
                    PeforManceDataBase d = datas.Peformances[key][i];
                    buttons[count].onClick.RemoveAllListeners();
                    buttons[count++].onClick.AddListener(() => ParagraphSet(d));
                }
            }
            ParagraphSet();
        }
        else
        {
            myObjct.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "図鑑";
            foreach (Item i in PlayerData.GetInstance().DicList.Item)
            {
                if(count >= buttons.Count)
                {
                    buttons.Add(GameObject.Instantiate(ItemPref,listParent.transform));
                }
                buttons[count].gameObject.SetActive(true);
                buttons[count].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.itemName;
                buttons[count].transform.GetChild(1).GetComponent<Image>().sprite = i.icon;
                Item it = PlayerData.GetInstance().DicList.Item[count];
                buttons[count].onClick.RemoveAllListeners();
                buttons[count++].onClick.AddListener(() => ParagraphSet(it));
            }
        }


    }
    public void ItemGet(ItemData item,int num,PeforManceDataBase data)
    {
        myObjct.transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = false;
        data.rewardGet = true;
        item.itemNum = num;
        PlayerData.GetInstance().Item.ItemGet(new Item(item),item.itemNum);
        PlayerData.GetInstance().DicList.ItemGet(new Item(item), item.itemNum);
    }
    void ParagraphSet()
    {
        myObjct.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        myObjct.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        myObjct.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        myObjct.transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        myObjct.transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
        myObjct.transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
        myObjct.transform.GetChild(0).GetChild(8).gameObject.SetActive(false);
    }
    void ParagraphSet(PeforManceDataBase data)
    {
        myObjct.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        myObjct.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        myObjct.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        myObjct.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
        myObjct.transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
        myObjct.transform.GetChild(0).GetChild(8).gameObject.SetActive(true);
        if (data.icon != null)
        {
            myObjct.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            myObjct.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = data.icon;
        }
        else
        {
            myObjct.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        }
        myObjct.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = data.Title;
        myObjct.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = data.icon;
        myObjct.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = data.conditionsText;
        myObjct.transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = data.rewardText;
        if(data.Clear && !data.rewardGet && data.rewardItem != null)
        {
            myObjct.transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = true;
            myObjct.transform.GetChild(0).GetChild(6).GetComponent<Button>().onClick.RemoveAllListeners();
            myObjct.transform.GetChild(0).GetChild(6).GetComponent<Button>().onClick.AddListener(() => ItemGet(data.rewardItem, data.rewardItemNum,data));
        }
        else
        {
            myObjct.transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = false;
        }
        myObjct.transform.GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>().text = (data.Clear ? data.DeText:"");
        myObjct.transform.GetChild(0).GetChild(8).GetComponent<TextMeshProUGUI>().text = data.NowState+"/" +data.conditions;
    }
    void ParagraphSet(Item data)
    {
        myObjct.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        myObjct.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        myObjct.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        myObjct.transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
        myObjct.transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
        myObjct.transform.GetChild(0).GetChild(8).gameObject.SetActive(true);
        myObjct.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = data.itemName;
        if(data.icon != null)
        {
            myObjct.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            myObjct.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = data.icon;
        }
        else
        {
            myObjct.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        }
        myObjct.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = "獲得数"+data.itemNum;
        myObjct.transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = (data.itemType != ItemType.Extra ? "成長速度："+data.growthSpeed + "%/日<br>":"") + "基本売値：" + data.sellPrice + "株";
        myObjct.transform.GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>().text = data.info;
        myObjct.transform.GetChild(0).GetChild(8).GetComponent<TextMeshProUGUI>().text = (data.itemType != ItemType.Extra ? "品質上昇：" + data.upQuality + "・" + "下降：" + data.downQuality:"") 
            + (data.itemType == ItemType.Fertilizer ? "<br>効果時間：" + (float)data.defaltValue/24 + "日":(data.itemType != ItemType.Extra ? "<br>旬：" + data.SeasonToStr :""));
    }
}
