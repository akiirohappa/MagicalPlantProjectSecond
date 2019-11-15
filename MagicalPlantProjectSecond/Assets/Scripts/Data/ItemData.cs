using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Item", menuName = "MyData/Item")]
public class ItemData : ScriptableObject
{
    
    public string itemName;
    public ItemType itemType;
    public int defaltValue;
    public float growthSpeed;
    public int quality;
    public int upQuality;
    public int downQuality;
    public int sellPrice;
    public int getValue;
    public Sprite icon;
    [Multiline(3)]
    public string info;
    public int itemNum;
    public SeasonData vestSeason;
    public Item data;
    public Item GetItem()
    {
        return data;
    }
}
