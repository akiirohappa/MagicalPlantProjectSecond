using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Seed,
    Plant,
    Fertilizer,
}
public class Item
{
    public Item(string name,ItemType type = ItemType.Seed,int value = 10,int qua = 50,string ifo = "",Sprite ic = null)
    {
        itemName = name;
        itemType = type;
        defaltValue = value;
        quality = qua;
        info = ifo;
        icon = ic;
        itemNum = 0;
    }
    public Item(ItemData it)
    {
        itemName = it.itemName;
        itemType = it.itemType;
        defaltValue = it.defaltValue;
        quality = it.quality;
        info = it.info;
        icon = it.icon;
        growthSpeed = it.growthSpeed;
        downQuality = it.downQuality;
        plantPrice = it.plantPrice;
        plantValue = it.plantValue;
        itemNum = 0;
    }
    public string itemName;
    public ItemType itemType;
    public int defaltValue;
    public float growthSpeed;
    public int quality;
    public int downQuality;
    public int plantPrice;
    public int plantValue;
    public Sprite icon;
    public string info;
    public int itemNum;
}
