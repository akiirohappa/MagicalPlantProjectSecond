//-------------------------------------------------------------
//タイルマップの情報など
//-------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
enum MapLayer
{
    Ground,
    Layer1,
    Layer2,
    Layer3,
    Event,
}
public class TileManager
{
    private static TileManager _tile;

    Dictionary<MapLayer,Tilemap> tiles;
    Grid grid;
    TileBase tiletest;
    List<Vector3Int> plantFieldPos;
    Sprite[] eventTiles;
    private TileManager()
    {
        eventTiles = Resources.LoadAll<Sprite>("Tile/EventTile");
        Debug.Log(eventTiles.Length);
        TileMapSet();
        TileFieldSet(tiles[MapLayer.Event]);
       
    }
    public static TileManager GetInstance()
    {
        if(_tile == null)
        {
            _tile = new TileManager();
        }
        return _tile;
    }
    void TileMapSet()
    {
        tiles = new Dictionary<MapLayer ,Tilemap>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        tiles[MapLayer.Ground] = grid.transform.GetChild(0).GetComponent<Tilemap>();
        tiles[MapLayer.Layer1] = grid.transform.GetChild(1).GetComponent<Tilemap>();
        tiles[MapLayer.Layer2] = grid.transform.GetChild(2).GetComponent<Tilemap>();
        tiles[MapLayer.Layer3] = grid.transform.GetChild(3).GetComponent<Tilemap>();
        tiles[MapLayer.Event] = grid.transform.GetChild(4).GetComponent<Tilemap>();
    }
    //渡された座標にイベントが仕込まれているかを取得
    public int GetTileEvent(Vector3Int vec)
    {
        var bound = tiles[MapLayer.Event].cellBounds;
        for (int x = bound.min.x; x < bound.max.x; x++)
        {
            for (int y = bound.min.y; y < bound.max.y; y++)
            {
               if (vec.x == x && vec.y == y)
               {
                    var tile = tiles[MapLayer.Event].GetTile<Tile>(new Vector3Int(x, y, 0));
                    if (tile != null)
                    {
                        for(int i = 0;i < eventTiles.Length; i++)
                        {
                            if(tile.sprite == eventTiles[i])
                            {
                                return i;
                            }
                        }
                    }
                }
            }
        }
        return -1;
    }
    //畑の座標を探す
    void TileFieldSet(Tilemap t)
    {
        plantFieldPos = new List<Vector3Int>();
        var bound = t.cellBounds;
        for(int x = bound.min.x;x < bound.max.x; x++)
        {
            for (int y = bound.min.y; y < bound.max.y; y++)
            {
                var tile = t.GetTile<Tile>(new Vector3Int(x, y, 0));
                if(tile != null)
                {
                    if (tile.sprite == eventTiles[0])
                    {
                        plantFieldPos.Add(new Vector3Int(x, y, 0));
                    }
                }
            }
        }
    }
    //畑の座標を渡す
    public Vector3Int[] TileFieldGet()
    {
        return plantFieldPos.ToArray();
    }
    //マウスの位置からタイルマップ上の座標取得
    public Vector3Int MousePosToCell()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10f;
        pos = Camera.main.ScreenToWorldPoint(pos);
        Vector3Int cellpos = grid.WorldToCell(pos);
        return cellpos;
    }
}
