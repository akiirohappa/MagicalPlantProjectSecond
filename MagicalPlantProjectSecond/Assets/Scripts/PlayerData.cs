﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    /*
    //メモ的に単位を置いとく
    const long TenTh = 10000;
    const long Billion = 100000000;
    const long Trillion = 1000000000000;
    */
    private static PlayerData _player;
    public static PlayerData GetInstance()
    {
        if(_player == null)
        {
            _player = new PlayerData();
        }
        return _player;
    }
    long money;
    //int money;
    ItemList item;
    TimeData time;
    MainManager mm;
    PeforManceDatas Pd;
    public ItemList DicList;
    public long Money
    {
        get
        {
            return money;
        }
        set
        {
            if(value >= long.MaxValue)
            {
                Debug.Log("最大値超えるとかどうかしてる");
            }
            else
            {
                long l = money;
                money = value;
                mm.View.MoneySet(money);
                if (value - l > 0)
                {
                    PD.DataUnlock(PeforManceType.Money, value - l);
                }
            }
        }
    }
    public ItemList Item
    {
        get
        {
            if(item == null)
            {
                item = new ItemList();
            }
            return item;
        }
        set { item = value; }
    }
    public List<Item> ListItem
    {
        get
        {
            if(Item.Item == null)
            {
                item.SetItemList(new List<Item>());
            }
            return item.Item;
        }
    }
    public PeforManceDatas PD
    {
        get { return Pd; }
        set { Pd = value; }
    }
    private PlayerData()
    {

    }
    public void Start()
    {
        Pd = new PeforManceDatas();
        mm = MainManager.GetInstance;
        Money = 1000;
        mm.View.MoneySet(money);
        //item = new ItemList();
        time = TimeManager.GetInstance().Time;
        DicList = new ItemList();
    }
}
