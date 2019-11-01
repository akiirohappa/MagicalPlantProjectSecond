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
    public void ItemGet(Item item,int num)
    {
        foreach(Item listI in list)
        {
            if(listI.itemName == item.itemName)
            {
                if(listI.sellPrice == item.sellPrice)
                {
                    listI.itemNum += num;
                    if (listI.itemNum <= 0)
                    {
                        list.Remove(listI);
                    }
                    return;
                }
            }
        }
        item.itemNum = num;
        list.Add(item);
    }
}
