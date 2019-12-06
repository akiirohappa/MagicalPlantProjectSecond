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
    PlantData,
    Event,
}
public enum PlantTileData
{
    None,
    Zero,
    Twenty,
    Fifty,
    Seventy,
    Hundred,
}
public class TileManager
{
    private static TileManager _tile;
    Dictionary<MapLayer,Tilemap> tiles;
    Grid grid;
    TileBase tiletest;
    List<Vector3Int> plantFieldPos;
    public List<Vector3Int> PlantField
    {
        get { return plantFieldPos; }
    }
    Sprite[] eventTiles;
    Dictionary<PlantType,  Dictionary<PlantTileData, Tile>> PlantTiles;
    private TileManager()
    {

    }
    public void Start()
    {
        eventTiles = Resources.LoadAll<Sprite>("Tile/EventTile");
        TileMapSet();
        TileFieldSet(tiles[MapLayer.Event]);
        TileDataSet();
    }
    void TileDataSet()
    {
        PlantTiles = new Dictionary<PlantType, Dictionary<PlantTileData, Tile>>();
        PlantTiles[PlantType.Leaf] = new Dictionary<PlantTileData, Tile>();
        PlantTile p = Resources.Load<PlantTile>("Tile/Tree");
        PlantTiles[PlantType.Tree][PlantTileData.None] = null;
        PlantTiles[PlantType.Tree][PlantTileData.Zero] = p.plant0;
        PlantTiles[PlantType.Tree][PlantTileData.Twenty] = p.plant20;
        PlantTiles[PlantType.Tree][PlantTileData.Fifty] = p.plant50;
        PlantTiles[PlantType.Tree][PlantTileData.Seventy] = p.plant70;
        PlantTiles[PlantType.Tree][PlantTileData.Hundred] = p.plant100;
        p = Resources.Load<PlantTile>("Tile/Leaf");
        PlantTiles[PlantType.Leaf][PlantTileData.None] = null;
        PlantTiles[PlantType.Leaf][PlantTileData.Zero] = p.plant0;
        PlantTiles[PlantType.Leaf][PlantTileData.Twenty] = p.plant20;
        PlantTiles[PlantType.Leaf][PlantTileData.Fifty] = p.plant50;
        PlantTiles[PlantType.Leaf][PlantTileData.Seventy] = p.plant70;
        PlantTiles[PlantType.Leaf][PlantTileData.Hundred] = p.plant100;
        p = Resources.Load<PlantTile>("Tile/Mushroom");
        PlantTiles[PlantType.Mushroom][PlantTileData.None] = null;
        PlantTiles[PlantType.Mushroom][PlantTileData.Zero] = p.plant0;
        PlantTiles[PlantType.Mushroom][PlantTileData.Twenty] = p.plant20;
        PlantTiles[PlantType.Mushroom][PlantTileData.Fifty] = p.plant50;
        PlantTiles[PlantType.Mushroom][PlantTileData.Seventy] = p.plant70;
        PlantTiles[PlantType.Mushroom][PlantTileData.Hundred] = p.plant100;
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
        tiles[MapLayer.PlantData] = grid.transform.GetChild(4).GetComponent<Tilemap>();
        tiles[MapLayer.Event] = grid.transform.GetChild(5).GetComponent<Tilemap>();
        
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
        if(pos.x > Screen.width && pos.y > Screen.height)
        {
            return new Vector3Int(99999, 99999, 99999);
        }
        pos.z = 10f;
        pos = Camera.main.ScreenToWorldPoint(pos);
        Vector3Int cellpos = grid.WorldToCell(pos);
        return cellpos;
    }
    public Vector3 CellToWorldPos(Vector3Int pos)
    {
        return grid.CellToWorld(pos); ;
    }
    //畑描画用タイルマップの書き換え
    public void ReWritePlantTile(PlantType type,PlantTileData tile,Vector3Int pos)
    {
        tiles[MapLayer.PlantData].SetTile(pos, PlantTiles[type][tile]);
    }
}
