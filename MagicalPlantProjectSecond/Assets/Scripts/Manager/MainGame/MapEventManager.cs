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
    public List<MapEventBase> Events
    {
        get { return events; }
    }
    GameObject mousePoint;
    TileManager tile;
    MapEventBase cullentMenu;
    MenuManager menu;
    HotBarView bar;
    public HotBarView Bar
    {
        get { return bar;}
    }
    FacilitiesDataView facilities;
    public FacilitiesDataView Facilities
    {
        get { return facilities; }
    }
    public bool buttonPressd = false;
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

    }
    public void Start()
    {
        events = new List<MapEventBase>();
        events.Add(new Field(0));
        events.Add(new MeData_Fountain(8));
        events.Add(new MeData_Bed(5));
        events.Add(new MeData_Axe(6));
        events.Add(new MeData_Bookshelf(3));
        mousePoint = GameObject.Instantiate(Resources.Load<GameObject>("MousePoint"));
        mousePoint.SetActive(false);
        tile = TileManager.GetInstance();
        menu = GameObject.Find("Manager").GetComponent<MenuManager>();
        bar = new HotBarView();
        facilities = new FacilitiesDataView();
    }
    public MapEventBase MapEventGet(Vector3Int vec)
    {
        int evnum = TileManager.GetInstance().GetTileEvent(vec);
        if (evnum != -1)
        {
            for(int i = 0;i < events.Count;i++)
            {
                if(events[i].eventNum == evnum)
                {
                    return events[i];
                }
            }
            return null;
        }
        else return null;
    }
    public void CheckEvent()
    {
        if (TimeManager.GetInstance().sleep)
        {
            return;
        }
        Vector3Int vec = TileManager.GetInstance().MousePosToCell();
        if(vec.x == 99999)
        {
            return;
        }
        MapEventBase m = MapEventGet(vec);
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (m != null)
            {
                if (m.eventStr != "Field" && FieldManager.GetInstance().View.GetIsView())
                {
                    FieldManager.GetInstance().View.PlantVSetActive(false);
                }
                if (menu.State == MenuState.EventSelect)
                {
                    if (!buttonPressd)
                    {
                        m.MenuClose();
                        FieldManager.GetInstance().View.PlantVSetActive(false);
                        menu.State = MenuState.None;
                        m = null;
                    }
                    else
                    {
                        buttonPressd = false;
                    }
                }
            }
            else
            {
                if (menu.State == MenuState.EventSelect)
                {
                    //cullentMenu != null &&
                    if (!buttonPressd)
                    {
                        cullentMenu.MenuClose();
                        FieldManager.GetInstance().View.PlantVSetActive(false);
                        menu.State = MenuState.None;
                    }
                    else
                    {
                        buttonPressd = false;
                    }
                }

                FieldManager.GetInstance().View.PlantVSetActive(false);
            }
        }
        switch (menu.State)
        {
            case MenuState.None:
            case MenuState.ItemSet:
                break;
            default:
                return;
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
                cullentMenu = m;
                m.OnHoverRun(vec);
                mousePoint.SetActive(true);
                Vector3 pos = tile.CellToWorldPos(tile.MousePosToCell());
                pos.x += 0.5f;
                pos.y += 0.5f;
                mousePoint.transform.position = pos;
            }
        }
        else
        {
            bar.SetHideHotBar(false);
            facilities.SetHideHotBar(false);
            mousePoint.SetActive(false);
        }
    }
    public void ButtonOnPointer()
    {
        buttonPressd = true;
    }
    public void MapEVLevelSet(MapEvent map)
    {
        for(int i = 0;i < events.Count; i++)
        {
            if(i >= map.events.Length)
            {
                break;
            }
            if(events[i].eventNum == 0)
            {
                continue;
            }
            if(events[i].Data != null)
            {
                events[i].Data.nowLevel = map.events[i];
            }
            
        }
    }
}
[System.Serializable]
public class MapEvent
{

    public int[] events;
    public MapEvent(List<MapEventBase>map)
    {
        events = new int[map.Count];
        for(int i = 0;i < map.Count; i++)
        {
            if(map[i].eventNum == 0)
            {
                continue;
            }
            if(map[i].Data != null)
            {
                events[i] = map[i].Data.nowLevel;
            }
            

            
        }
    }
}