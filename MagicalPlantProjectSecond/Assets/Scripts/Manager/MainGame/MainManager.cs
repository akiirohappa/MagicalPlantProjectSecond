using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    TileManager tile;
    FieldManager field;
    MapEventManager map;
    TimeManager time;
    // Start is called before the first frame update
    void Start()
    {
        tile = TileManager.GetInstance();
        field = FieldManager.GetInstance();
        map = MapEventManager.GetInstance();
        time = TimeManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        map.CheckEvent();
        time.TimeCalc(5);
    }
}
