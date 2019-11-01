using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    PlayerData player;
    TileManager tile;
    FieldManager field;
    MapEventManager map;
    TimeManager time;
    HeaderDataView view;
    public HeaderDataView View
    {
        get
        {
            if (view == null) view = new HeaderDataView();
            return view;
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
    }

    // Update is called once per frame
    void Update()
    {
        map.CheckEvent();
        time.TimeCalc(50);
    }
}
