using System.Collections;
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
                money = value;
                mm.View.MoneySet(money);
            }
        }
    }
    public ItemList Item
    {
        get { return item; }
    }
    private PlayerData()
    {
        mm = GameObject.Find("Manager").GetComponent<MainManager>();
        money = 4999999999999999;
        mm.View.MoneySet(money);
        item = new ItemList();
        time = TimeManager.GetInstance().GetTime();
    }
}
