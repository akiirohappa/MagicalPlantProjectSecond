//-------------------------------------------------------------
//マップ上でクリックしたときの処理
//-------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEventManager : MonoBehaviour
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
}
