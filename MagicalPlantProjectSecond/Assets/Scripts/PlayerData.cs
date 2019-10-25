using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private static PlayerData _player;
    public static PlayerData GetInstance()
    {
        if(_player == null)
        {
            _player = new PlayerData();
        }
        return _player;
    }
    int money;
    ItemList item;
    TimeData time;
    MainManager mm;
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            mm.View.MoneySet(money);
        }
    }
    public ItemList Item
    {
        get { return item; }
    }
    private PlayerData()
    {
        mm = GameObject.Find("Manager").GetComponent<MainManager>();
        money = 100;
        mm.View.MoneySet(money);
        item = new ItemList();
        time = TimeManager.GetInstance().GetTime();
    }
}
