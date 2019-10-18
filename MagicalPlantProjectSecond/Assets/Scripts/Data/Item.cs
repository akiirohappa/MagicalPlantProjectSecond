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
    public Item(string name,ItemType type = ItemType.Seed,int value = 10,int qua = 50)
    {
        itemName = name;
        itemType = type;
        defaltValue = value;
        quality = qua;
    }
    public string itemName;
    public ItemType itemType;
    public int defaltValue;
    public int quality;
    public Sprite icon;
}
