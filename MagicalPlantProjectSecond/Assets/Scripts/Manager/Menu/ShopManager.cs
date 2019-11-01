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
            ShopList[ItemType.Seed].Item.Add(i.GetItem());
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
                    PlayerData.GetInstance().Money -= nowViewItem.defaltValue * shopValue;
                    PlayerData.GetInstance().Item.ItemGet(nowViewItem, shopValue);
                    Cancel();
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
                state = ShopState.JanlSelect;
                nowJanl = null;
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
                        break;
                    case "Sell":
                        trade = TradeState.Sell;
                        break;
                }
                state = ShopState.JanlSelect;
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
                            shopValue = PlayerData.GetInstance().Money / nowViewItem.defaltValue;
                            if (shopValue > 99) shopValue = 99;
                            else if (shopValue == 0) shopValue = 1;
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
    void SetStateObject()
    {
        switch (state)
        {
            case ShopState.Menu:
                state = ShopState.Menu;
                ObjectActive(state);
                break;
            case ShopState.JanlSelect:

                break;
            default:
                break;
        }
    }
    void GoodsButtonMake()
    {
        foreach(Transform t in shopListParent.transform)
        {
            GameObject.Destroy(t.gameObject);
        }
        GameObject g;
        foreach(Item i in nowJanl.Item)
        {
            g = GameObject.Instantiate(itemButtonPrefab,shopListParent.transform);
            g.GetComponent<ItemButton>().item = i;
            g.GetComponent<Button>().onClick.AddListener(mManager.ButtonItem);
            g.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = i.itemName;
            g.transform.Find("Icon").GetComponent<Image>().sprite = i.icon;
            g.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = i.defaltValue + "株";
        }
    }
    public override void PlessItemButton(Item item)
    {
        if(item != null)
        {
            nowViewItem = item;
            goodsPanel.Find("Icon").GetComponent<Image>().sprite = item.icon;
            goodsPanel.Find("NameText").GetComponent<TextMeshProUGUI>().text = item.itemName;
            goodsPanel.Find("PriceText").GetComponent<TextMeshProUGUI>().text = item.defaltValue + "株";
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
        else if (shopValue > 99)
        {
            shopValue = 99;
        }
        int nowValue = nowViewItem.defaltValue * shopValue;
        goodsPanel.Find("PriceText").GetComponent<TextMeshProUGUI>().text = nowValue + "株";
        ShopObject[ShopState.ValueSelect].transform.Find("ValueText").GetComponent<TextMeshProUGUI>().text = shopValue + "個";
        if (nowValue > PlayerData.GetInstance().Money)
        {
            ShopObject[ShopState.ValueSelect].transform.Find("ArartText").gameObject.SetActive(true);
        }
        else
        {
            ShopObject[ShopState.ValueSelect].transform.Find("ArartText").gameObject.SetActive(false);
        }
    }
}

