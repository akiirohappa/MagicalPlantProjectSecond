using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="PlantTile",menuName = "PlantTile")]
public class PlantTile : ScriptableObject
{
    public Tile plant0;
    public Tile plant20;
    public Tile plant50;
    public Tile plant70;
    public Tile plant100;
}
