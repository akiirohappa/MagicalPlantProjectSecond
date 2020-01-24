//-------------------------------------------------------------------------
//メニュー管理：ショップ編。
//-------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopManager:MenuManagerBase
{
    enum ShopState
    {
        Menu,
        JanlSelect,
        GoodsPanel,
        GoodsSelect,
        ValueSelect,
    }
    enum TradeState
    {
        Buy,
        Sell,
        None
    }
    ShopState state;
    TradeState trade;
    Dictionary<ShopState, GameObject> ShopObject;
    Dictionary<ItemType, ItemList> ShopList;
    ItemList nowJanl;
    GameObject shopListParent;
    GameObject itemButtonPrefab;
    List<GameObject> itemButtons;
    Item nowViewItem;
    Transform goodsPanel;
    int shopValue;
    ItemListSort sort;
    public ShopManager(MenuManager m):base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Shop").gameObject;
        state = ShopState.Menu;
        trade = TradeState.None;
        ObjectDicSet();
        ShopListGet();
        shopListParent = ShopObject[ShopState.GoodsPanel].transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        itemButtonPrefab = Resources.Load<GameObject>("Prefabs/ItemButton");
        itemButtons = new List<GameObject>();
        goodsPanel = ShopObject[ShopState.GoodsPanel].transform.Find("GoodsPanel");
        sort = myObjct.transform.GetChild(0).GetChild(2).Find("SortPanel").GetComponent<ItemListSort>();
    }
    void ObjectDicSet()
    {
        ShopObject = new Dictionary<ShopState, GameObject>();
        ShopObject[ShopState.Menu] = myObjct.transform.Find("Panel").Find("ModeSelect").gameObject;
        ShopObject[ShopState.JanlSelect] = myObjct.transform.Find("Panel").Find("JanlSelect").gameObject;
        ShopObject[ShopState.GoodsPanel] = myObjct.transform.Find("Panel").Find("GoodsSelect").gameObject;
        ShopObject[ShopState.GoodsSelect] = ShopObject[ShopState.GoodsPanel].transform.Find("GoodsPanel").Find("GoodsSelect").gameObject;
        ShopObject[ShopState.ValueSelect] = ShopObject[ShopState.GoodsPanel].transform.Find("GoodsPanel").Find("ValueSelect").gameObject;
    }
    void ShopListGet()
    {
        ShopList = new Dictionary<ItemType, ItemList>();
        ShopList[ItemType.Seed] = new ItemList();
        ShopList[ItemType.Plant] = new ItemList();
        ShopList[ItemType.Fertilizer] = new ItemList();
        ItemData[] items = Resources.LoadAll<ItemData>("Item");
        foreach(ItemData i in items)
        {
            if(i.itemType == ItemType.Seed)
            {
                ShopList[ItemType.Seed].Item.Add(new Item(i));
            }
        }
        items = Resources.LoadAll<ItemData>("Item/Fartilizer");
        foreach (ItemData i in items)
        {
            if (i.itemType == ItemType.Fertilizer)
            {
                ShopList[ItemType.Fertilizer].Item.Add(new Item(i));
            }
        }

    }
    public override void Open()
    {
        base.Open();
        state = ShopState.Menu;
        ObjectActive(state);
    }
    public override void Submit()
    {
        switch (state)
        {
            case ShopState.GoodsSelect:
                state = ShopState.ValueSelect;
                shopValue = 1;
                GoodsValueChange(0);
                break;
            case ShopState.ValueSelect:
                if(trade == TradeState.Buy)
                {
                    //金が足りてる時の処理
                    if(PlayerData.GetInstance().Money >= nowViewItem.defaltValue * shopValue)
                    {
                        PlayerData.GetInstance().Money -= nowViewItem.defaltValue * shopValue;
                        PlayerData.GetInstance().Item.ItemGet(nowViewItem, shopValue);
                        PlayerData.GetInstance().DicList.ItemGet(nowViewItem, shopValue);
                        Cancel();
                    }
                    //金が足りない時の処理
                    else
                    {

                    }
                }
                else
                {

                    PlayerData.GetInstance().Money += nowViewItem.sellPrice * shopValue;
                    PlayerData.GetInstance().Item.ItemGet(nowViewItem, -shopValue);
                    Cancel();
                    if (nowViewItem.itemNum - shopValue <= 0)
                    {
                        PlessItemButton(null);
                    }
                    GoodsButtonMake();
                }
                break;
            default:
                break;
        }
        ObjectActive(state);
    }
    public override void Cancel()
    {
        switch (state)
        {
            case ShopState.JanlSelect:
                state = ShopState.Menu;
                trade = TradeState.None;
                break;
            case ShopState.GoodsSelect:
                if(trade == TradeState.Buy)
                {
                    state = ShopState.JanlSelect;
                    nowJanl = null;
                }
                else
                {
                    state = ShopState.Menu;
                }
                break;
            case ShopState.ValueSelect:
                state = ShopState.GoodsSelect;
                shopValue = 1;
                GoodsValueChange(0);
                break;
            default:
                break;
        }
        ObjectActive(state);
    }
    public override void Button(string st)
    {
        switch (state)
        {
            case ShopState.Menu:
                { 
                switch (st)
                {
                    case "Buy":
                            trade = TradeState.Buy;
                            state = ShopState.JanlSelect;
                            break;
                    case "Sell":
                            trade = TradeState.Sell;
                            nowJanl = PlayerData.GetInstance().Item;
                            sort.item = PlayerData.GetInstance().Item;
                            sort.states = new SortState[]{
                                SortState.ItemName,
                                SortState.ItemType,
                                SortState.ItemValue,
                                SortState.GrowthSpeed,
                            };
                            state = ShopState.GoodsSelect;
                            GoodsButtonMake();
                            PlessItemButton(null);
                            break;
                }
                
                ObjectActive(state);
                }
                break;
            case ShopState.JanlSelect:
                {
                    switch (st)
                    {
                        case "Seed":
                            nowJanl = ShopList[ItemType.Seed];
                            sort.item = nowJanl;
                            sort.states = new SortState[]{
                                SortState.ItemName,
                                SortState.ItemType,
                                SortState.ItemValue,
                                SortState.GrowthSpeed,
                            };
                            state = ShopState.GoodsSelect;
                            ObjectActive(state);
                            GoodsButtonMake();
                            PlessItemButton(null);
                            break;
                        case "Fartilizer":
                            nowJanl = ShopList[ItemType.Fertilizer];
                            sort.item = nowJanl;
                            sort.states = new SortState[]{
                                SortState.ItemName,
                                SortState.ItemType,
                                SortState.ItemValue,
                                SortState.GrowthSpeed,
                            };
                            state = ShopState.GoodsSelect;
                            ObjectActive(state);
                            GoodsButtonMake();
                            PlessItemButton(null);
                            break;
                    }
                }
                break;
            case ShopState.GoodsSelect:
            case ShopState.ValueSelect:
                {
                    switch (st)
                    {
                        case "+1":
                            GoodsValueChange(1);
                            break;
                        case "+5":
                            GoodsValueChange(5);
                            break;
                        case "+All":
                            if(trade == TradeState.Buy)
                            {
                                if (PlayerData.GetInstance().Money / nowViewItem.defaltValue > 99)
                                {
                                    shopValue = 99;
                                }
                                else if (shopValue == 0) shopValue = 1;
                                else
                                {
                                    shopValue = (int)PlayerData.GetInstance().Money / nowViewItem.defaltValue;
                                }
                            }
                            else
                            {
                                shopValue = nowViewItem.itemNum;
                            }
                            GoodsValueChange(0);
                            break;
                        case "-1":
                            GoodsValueChange(-1);
                            break;
                        case "-5":
                            GoodsValueChange(-5);
                            break;
                        case "-All":
                            shopValue = 1;
                            GoodsValueChange(0);
                            break;
                        case "Reset":
                            GoodsButtonMake();
                            break;
                        default:
                            break;
                    }
                }
                break;
            default:
                break;
        }
    }
    void GoodsButtonMake()
    {
        if(itemButtons.Count != 0)
        {
            foreach (GameObject g in itemButtons)
            {
                g.SetActive(false);
            }
        }
        for(int i = 0;i < nowJanl.Item.Count; i++)
        {
            if(itemButtons.Count <= i)
            {
                itemButtons.Add(GameObject.Instantiate(itemButtonPrefab, shopListParent.transform));
            }
            else
            {
                itemButtons[i].SetActive(true);
            }
            itemButtons[i].GetComponent<ItemButton>().item = nowJanl.Item[i];
            itemButtons[i].GetComponent<Button>().onClick.AddListener(mManager.ButtonItem);
            itemButtons[i].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = nowJanl.Item[i].itemName;
            itemButtons[i].transform.Find("Icon").GetComponent<Image>().sprite = nowJanl.Item[i].icon;
            itemButtons[i].transform.Find("Price").GetComponent<TextMeshProUGUI>().text = (trade == TradeState.Buy ?  nowJanl.Item[i].defaltValue:nowJanl.Item[i].sellPrice) + "株";
            if (nowJanl.Item[i].itemType == ItemType.Plant)
            {
                itemButtons[i].transform.Find("Quality").gameObject.SetActive(true);
                itemButtons[i].transform.Find("Quality").GetComponent<TextMeshProUGUI>().text = "品質: " + nowJanl.Item[i].quality;
                //itemButtons[i].transform.Find("Value").gameObject.SetActive(false);
                //itemButtons[i].transform.Find("Value").GetComponent<TextMeshProUGUI>().text = "個数: " + nowJanl.Item[i].itemNum + "個";
            }
            else
            {
                itemButtons[i].transform.Find("Quality").gameObject.SetActive(false);
                //itemButtons[i].transform.Find("Value").gameObject.SetActive(false);
            }
        }
    }
    public override void PlessItemButton(Item item)
    {
        ObjectActive( state = ShopState.GoodsSelect);
        if(item != null)
        {
            nowViewItem = item;
            goodsPanel.Find("Icon").GetComponent<Image>().sprite = item.icon;
            goodsPanel.Find("NameText").GetComponent<TextMeshProUGUI>().text = item.itemName;
            goodsPanel.Find("PriceText").GetComponent<TextMeshProUGUI>().text = (trade == TradeState.Buy ? item.defaltValue : item.sellPrice) + "株";
            ShopObject[ShopState.GoodsSelect].transform.Find("GrowthText").GetComponent<TextMeshProUGUI>().text = "成長速度：" + item.growthSpeed + "%/日";
            ShopObject[ShopState.GoodsSelect].transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = item.info;
        }
        else
        {
            nowViewItem = null;
            goodsPanel.Find("Icon").GetComponent<Image>().sprite = null;
            goodsPanel.Find("NameText").GetComponent<TextMeshProUGUI>().text = "";
            goodsPanel.Find("PriceText").GetComponent<TextMeshProUGUI>().text = "";
            ShopObject[ShopState.GoodsSelect].transform.Find("GrowthText").GetComponent<TextMeshProUGUI>().text = "";
            ShopObject[ShopState.GoodsSelect].transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = "ここに選んだ商品の情報が出るよ";
        }
    }
    void ObjectActive(ShopState nowst)
    {
        foreach(ShopState g in ShopObject.Keys)
        {
            if(g != nowst)
            {
                if (nowst == ShopState.GoodsSelect || nowst == ShopState.ValueSelect)
                {
                    ShopObject[ShopState.GoodsPanel].SetActive(true);
                }
                ShopObject[g].SetActive(false);
            }
            else
            {
                ShopObject[g].SetActive(true);
            }
        }
    }
    void GoodsValueChange(int num)
    {
        shopValue += num;
        if(shopValue <= 0)
        {
            shopValue = 1;
        }
        else if (trade == TradeState.Buy && shopValue > 99)
        {
            shopValue = 99;
        }
        else if(trade == TradeState.Sell && shopValue > nowViewItem.itemNum)
        {
            shopValue = nowViewItem.itemNum;
        }
        int nowValue = (trade == TradeState.Buy ? nowViewItem.defaltValue: nowViewItem.sellPrice) * shopValue;
        goodsPanel.Find("PriceText").GetComponent<TextMeshProUGUI>().text = nowValue + "株";
        ShopObject[ShopState.ValueSelect].transform.Find("ValueText").GetComponent<TextMeshProUGUI>().text = shopValue + "個";
        if (trade == TradeState.Buy && nowValue > PlayerData.GetInstance().Money)
        {
            ShopObject[ShopState.ValueSelect].transform.Find("ArartText").gameObject.SetActive(true);
        }
        else
        {
            ShopObject[ShopState.ValueSelect].transform.Find("ArartText").gameObject.SetActive(false);
        }
    }
}

