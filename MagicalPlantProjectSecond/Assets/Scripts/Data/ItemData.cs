﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Item", menuName = "MyData/Item")]
public class ItemData : ScriptableObject
{
    public Item data;
    [Multiline(2)]
    public string itemName;
    public ItemType itemType;
    public int defaltValue;
    public float growthSpeed = 10;
    public int quality;
    public int downQuality;
    public int plantPrice;
    public int plantValue;
    public Sprite icon;
    [Multiline(3)]
    public string info;
    public Item GetItem()
    {
        if(data == null)
        {
            data = new Item(this);
        }
        return data;
    }
}