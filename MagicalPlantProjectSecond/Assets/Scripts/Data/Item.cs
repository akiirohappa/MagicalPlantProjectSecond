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
        upQuality = it.upQuality;
        sellPrice = it.plantPrice;
        getValue = it.plantValue;
        itemNum = 0;
    }
    public Item(Plant pl)
    {
        itemName = pl.name;
        itemType = ItemType.Plant;
        defaltValue = pl.defValue;
        quality = pl.quality;
        info = pl.info;
        icon = pl.icon;
        itemNum = 0;
        getValue = pl.getValue;
    }
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
    public string info;
    public int itemNum;
    public SeasonData vestSeason;
}
