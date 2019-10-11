using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileSet : MonoBehaviour
{
    [SerializeField] Tilemap[] tiles;
    [SerializeField] TileBase tiletest;
    [SerializeField] Vector3Int plantStart;
    [SerializeField] Vector3Int plantEnd;
    [SerializeField] Vector3Int pos;
    [SerializeField] List<Vector3Int> plantFieldPos;
    [SerializeField] TileBase[] EventTiles;
    // Start is called before the first frame update
    void Start()
    {
        TilePlantFieldGet(tiles[0]);
    }

    // Update is called once per frame
    void Update()
    {
        //TileGet(tiles[0]);
    }
    void TilePlantFieldGet(Tilemap t)
    {
        plantFieldPos = new List<Vector3Int>();
        for(int x = plantStart.x;x <= plantEnd.x; x++)
        {
            for (int y = plantStart.y; y <= plantEnd.y; y++)
            {
                plantFieldPos.Add(new Vector3Int(x, y, 0));
                t.SetTile(new Vector3Int(x, y, 0), tiletest);
            }
        }
    }
}
