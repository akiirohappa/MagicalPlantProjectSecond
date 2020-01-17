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


    public PeforManceManager(MenuManager m):base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Peformance").gameObject;
        ItemButtons = new List<Button>();
        PeformanceButtons = new List<Button>();
        datas = PlayerData.GetInstance().PD;


        peforManceButton = Resources.Load<GameObject>("Prefabs/PefoeManceButton");
        listParent = myObjct.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject;
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
            for (int i = 0; i < datas.Peformances[key].Count; i++)
            {
                PeformanceButtons.Add(GameObject.Instantiate(peforManceButton, listParent.transform).GetComponent<Button>());
                PeformanceButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = datas.Peformances[key][i].Title;
                PeformanceButtons[i].gameObject.SetActive(false);
            }
        }
        
       
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
    void MenuSet()
    {
        var buttons = (state == PeforManceMenuState.PeforMance ? PeformanceButtons:ItemButtons);
        int count = 0;
        if(state == PeforManceMenuState.PeforMance)
        {
            myObjct.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "実績";
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
                for (int i = 0; i < datas.Peformances[key].Count; i++)
                {
                    buttons[count].gameObject.SetActive(true);
                    buttons[count].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = datas.Peformances[key][i].Title;
                    buttons[count].transform.GetChild(1).GetComponent<Slider>().maxValue = datas.Peformances[key][i].conditions;
                    buttons[count].transform.GetChild(1).GetComponent<Slider>().minValue = 0;
                    buttons[count].transform.GetChild(1).GetComponent<Slider>().value = datas.Peformances[key][i].NowState;
                    buttons[count].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = datas.Peformances[key][i].NowState +"/\n" + datas.Peformances[key][i].conditions;
                    PeforManceDataBase d = datas.Peformances[key][count];
                    buttons[count++].onClick.AddListener(() => ParagraphSet(d));
                }
            }
            ParagraphSet(null);
        }



    }
    public void ItemGet(ItemData item,int num,PeforManceDataBase data)
    {
        myObjct.transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = false;
        data.rewardGet = true;
        item.itemNum = num;
        PlayerData.GetInstance().Item.Item.Add(new Item(item));
    }
    void ParagraphSet(PeforManceDataBase data)
    {
        if (data == null)
        {
            myObjct.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            myObjct.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            myObjct.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            myObjct.transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
            myObjct.transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
            myObjct.transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
        }
        else
        {
            myObjct.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            myObjct.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            myObjct.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
            myObjct.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            myObjct.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
            myObjct.transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
            myObjct.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = data.Title;
            myObjct.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = data.icon;
            myObjct.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = data.conditionsText;
            myObjct.transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = data.rewardText;
            if(data.Clear && !data.rewardGet && data.rewardItem != null)
            {
                myObjct.transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = true;
                myObjct.transform.GetChild(0).GetChild(6).GetComponent<Button>().onClick.AddListener(() => ItemGet(data.rewardItem, data.rewardItemNum,data));
            }
            else
            {
                myObjct.transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = false;
            }
            myObjct.transform.GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>().text = data.DeText;
            myObjct.transform.GetChild(0).GetChild(8).GetComponent<TextMeshProUGUI>().text = data.NowState+"/" +data.conditions;
        }

    }
}
