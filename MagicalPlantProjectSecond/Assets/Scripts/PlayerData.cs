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
    TimeData time;


    private PlayerData()
    {

    }
}
