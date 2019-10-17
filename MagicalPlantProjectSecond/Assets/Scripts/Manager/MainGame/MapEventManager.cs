//-------------------------------------------------------------
//マップ上でクリックしたときの処理
//-------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventManager
{
    private static MapEventManager _map;
    List<MapEventBase> events;
    public static MapEventManager GetInstance()
    {
        if (_map == null)
        {
            _map = new MapEventManager();
        }
        return _map;
    }
    private MapEventManager()
    {
        events = new List<MapEventBase>();
        events.Add(new Field(0));
    }
    public MapEventBase MapEventGet(Vector3Int vec)
    {
        int evnum = TileManager.GetInstance().GetTileEvent(vec);
        if (evnum != -1 && evnum < events.Count)
        {
            return events[evnum];
        }
        else return null;
    }
    public void CheckEvent()
    {
        Vector3Int vec = TileManager.GetInstance().MousePosToCell();
        MapEventBase m = MapEventGet(vec);
        if(Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
        {
            if(m != null)
            {
                if (m.eventStr != "Field" && FieldManager.GetInstance().v.GetIsView())
                {
                    FieldManager.GetInstance().v.PlantVSetActive(false);
                }
            }
            else FieldManager.GetInstance().v.PlantVSetActive(false);
        }
        if(m != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m != null)
                {
                    m.OnLeftClickRun();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (m != null)
                {
                    m.OnRightClickRun();
                }
            }
            else
            {
                m.OnHoverRun(vec);
            }
        }
    }
}
