using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList
{
    List<Item> list;
    public ItemList()
    {
        list = new List<Item>();
    }
    public List<Item> Item
    {
        get { return list; }
    }
}
