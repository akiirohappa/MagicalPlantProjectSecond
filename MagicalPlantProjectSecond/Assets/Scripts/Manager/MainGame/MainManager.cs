using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    TileManager tile;
    FieldManager field;
    MapEventManager map;
    // Start is called before the first frame update
    void Start()
    {
        tile = TileManager.GetInstance();
        field = FieldManager.GetInstance();
        map = MapEventManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        map.CheckEvent();
    }
}
