﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    static MainManager main;
    PlayerData player;
    TileManager tile;
    FieldManager field;
    MapEventManager map;
    TimeManager time;
    HeaderDataView view;
    LogManager log;
    SaveAndLoad sl;
    [SerializeField] int ExTime;
    public static MainManager  GetInstance
    {
        get
        {
            if(main == null)
            {
                if(SceneManager.GetActiveScene().name != "Main")
                {
                    return null;
                }
                GameObject g = GameObject.Find("Manager");
                main = g.GetComponent<MainManager>();
                if(main == null)
                {
                    main = g.AddComponent<MainManager>();
                }
            }
            return main;
        }
    }
    public HeaderDataView View
    {
        get
        {
            if (view == null) view = new HeaderDataView();
            return view;
        }
    }
    public LogManager Log
    {
        get
        {
            if(log == null)
            {
                log = new LogManager();
            }
            return log;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerData.GetInstance();
        tile = TileManager.GetInstance();
        field = FieldManager.GetInstance();
        map = MapEventManager.GetInstance();
        time = TimeManager.GetInstance();
        view = new HeaderDataView();
        sl = new SaveAndLoad();
        tile.Start();
        field.Start();
        map.Start();
        time.Start();
        player.Start();
        //sl.Save(0, new SaveData());
    }

    // Update is called once per frame
    void Update()
    {
        map.CheckEvent();
        time.TimeCalc(ExTime);
    }
}
