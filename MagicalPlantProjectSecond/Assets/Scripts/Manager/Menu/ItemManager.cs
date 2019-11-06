using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager:MenuManagerBase
{
    ItemList list;
    GameObject buttonField;
    GameObject infoPanel;
    GameObject itemButtonPrefab;
    List<GameObject> itemButtons;
    bool panelOn = false;
    Item showItem;  
    ItemListSort sort;
    public ItemManager(MenuManager m):base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Item").gameObject;
        buttonField = myObjct.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        infoPanel = myObjct.transform.GetChild(2).gameObject;
        itemButtonPrefab = Resources.Load<GameObject>("Prefabs/ItemButton");
        sort = myObjct.transform.Find("SortPanel").GetComponent<ItemListSort>();
        itemButtons = new List<GameObject>();
    }
    public override void Open()
    {
        base.Open();
        list = PlayerData.GetInstance().Item;
        sort.item = list;
        ItemSet();
    }
    public void ItemSet()
    {
        sort.states = new SortState[]{
            SortState.ItemName,
            SortState.ItemNum,
            SortState.ItemType,
        };
        if (itemButtons.Count != 0)
        {
            foreach (GameObject g in itemButtons)
            {
                g.SetActive(false);
            }
        }
        for (int i = 0; i < list.Item.Count; i++)
        {
            if (itemButtons.Count <= i)
            {
                itemButtons.Add(GameObject.Instantiate(itemButtonPrefab, buttonField.transform));
            }
            else
            {
                itemButtons[i].SetActive(true);
            }
            itemButtons[i].GetComponent<ItemButton>().item = list.Item[i];
            itemButtons[i].GetComponent<Button>().onClick.AddListener(mManager.ButtonItem);
            itemButtons[i].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = list.Item[i].itemName;
            itemButtons[i].transform.Find("Icon").GetComponent<Image>().sprite = list.Item[i].icon;
            itemButtons[i].transform.Find("Price").GetComponent<TextMeshProUGUI>().text = list.Item[i].defaltValue + "株";
        }
    }
    public override void Submit()
    {

    }
    public override void Cancel()
    {
        panelOn = false;
        PlessItemButton(null);
        infoPanel.SetActive(false);
    }
    public override void PlessItemButton(Item item)
    {
        infoPanel.SetActive(true);
        panelOn = true;
        if (item != null)
        {
            showItem = item;
            infoPanel.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
            infoPanel.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = item.itemName;
            infoPanel.transform.Find("PriceText").GetComponent<TextMeshProUGUI>().text = "個数:" + item.itemNum;
            infoPanel.transform.transform.Find("GrowthText").GetComponent<TextMeshProUGUI>().text = "成長速度：" + item.growthSpeed + "%/日";
            infoPanel.transform.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = item.info;
        }
    }
    public override void Button(string state)
    {
        if(state == "Reset")
        {
            ItemSet();
        }
    }
}
