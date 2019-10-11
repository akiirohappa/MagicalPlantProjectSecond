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
public class TileManager : MonoBehaviour
{
    [SerializeField] Dictionary<MapLayer,Tilemap> tiles;
    [SerializeField] Grid grid;
    [SerializeField] TileBase tiletest;
    [SerializeField] Vector3Int plantStart;
    [SerializeField] Vector3Int plantEnd;
    [SerializeField] Vector3Int pos;
    [SerializeField] List<Vector3Int> plantFieldPos;
    [SerializeField] Sprite[] EventTiles;
    
    // Start is called before the first frame update
    void Start()
    {
        TileMapSet();
        TilePlantFieldGet(tiles[0]);
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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePosToCell();
        }
    }
    void TilePlantFieldGet(Tilemap t)
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
                    if (tile.sprite == EventTiles[9])
                    {
                        plantFieldPos.Add(new Vector3Int(x, y, 0));
                        t.SetTile(new Vector3Int(x, y, 0), tiletest);
                    }
                    
                }

                
            }
        }
    }
    Vector3Int MousePosToCell()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10f;
        pos = Camera.main.ScreenToWorldPoint(pos);
        Vector3Int cellpos = grid.WorldToCell(pos);
        return cellpos;
    }
}
