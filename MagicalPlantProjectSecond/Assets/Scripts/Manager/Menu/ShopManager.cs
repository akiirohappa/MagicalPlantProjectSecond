using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager:MenuManagerBase
{
    enum ShopState
    {
        Menu,
        JanlSelect,
        BuyGoodsSelect,
        BuyValueSelect,
        SellGoodsSelect,
        SellValueSelect,
    }
    ShopState state;
    public ShopManager(MenuManager m):base(m)
    {
        myObjct = GameObject.Find("Shop");
        state = ShopState.Menu;
    }
    public override void Open()
    {
        base.Open();
    }
    public override void Submit()
    {
        switch (state)
        {
            case ShopState.BuyGoodsSelect:

                break;
            case ShopState.BuyValueSelect:

                break;
            case ShopState.SellGoodsSelect:

                break;
            case ShopState.SellValueSelect:

                break;
            default:
                break;
        }
    }
    public override void Cancel()
    {
        switch (state)
        {
            case ShopState.JanlSelect:

                break;
            case ShopState.BuyGoodsSelect:

                break;
            case ShopState.BuyValueSelect:

                break;
            case ShopState.SellGoodsSelect:

                break;
            case ShopState.SellValueSelect:

                break;
            default:
                break;
        }
    }
    public override void Button(string st)
    {
        switch (state)
        {
            case ShopState.JanlSelect:

                break;
            case ShopState.BuyGoodsSelect:

                break;
            case ShopState.BuyValueSelect:

                break;
            case ShopState.SellGoodsSelect:

                break;
            case ShopState.SellValueSelect:

                break;
            default:
                break;
        }
    }
}

