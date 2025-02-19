﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum ItemType
{
    Seed,
    Plant,
    Fertilizer,
    Extra,
}
[System.Serializable]
public class Item
{
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
    public Item(ItemData id)
    {
        itemName = id.itemName;
        itemType = id.itemType;
        defaltValue = id.defaltValue;
        growthSpeed = id.growthSpeed;
        quality = id.quality;
        upQuality = id.upQuality;
        downQuality = id.downQuality;
        sellPrice = id.sellPrice;
        getValue = id.getValue;
        icon = id.icon;
        info = id.info;
        itemNum = id.itemNum;
        vestSeason = id.vestSeason;
        plantType = id.plantType;
    }
    public string SeasonToStr
    {
        get
        {
            switch (vestSeason)
            {
                case SeasonData.Spring:
                    return "春";
                case SeasonData.Summer:
                    return "夏";
                case SeasonData.Autumn:
                    return "秋";
                case SeasonData.Winter:
                    return "冬";
                default:
                    return "空";
            }
        }
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
    [Multiline(3)]
    public string info;
    public int itemNum;
    public SeasonData vestSeason;
    public PlantType plantType;
}
