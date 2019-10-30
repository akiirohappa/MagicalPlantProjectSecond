//-----------------------------------------------------------------------
//アイテムのソートをする
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SortState
{
    ItemName,
    ItemNum,
    ItemType,
}
public class ItemListSort
{
    public ItemList item;
    public SortState state;
    public bool IsUpper;
    public void Sort()
    {
        if (IsUpper)
        {
            switch (state)
            {
                case SortState.ItemName:
                    item.Item.Sort((a, b) => string.Compare(a.itemName, b.itemName));
                    break;
                case SortState.ItemNum:
                    item.Item.Sort((a, b) => a.itemNum - b.itemNum);
                    break;
                case SortState.ItemType:
                    item.Item.Sort((a, b) => a.itemType - b.itemType);
                    break;
                default:

                    break;
            }
        }
        else
        {
            switch (state)
            {
                case SortState.ItemName:
                    item.Item.Sort((a, b) => string.Compare(b.itemName, a.itemName));
                    break;
                case SortState.ItemNum:
                    item.Item.Sort((a, b) => b.itemNum - a.itemNum);
                    break;
                case SortState.ItemType:
                    item.Item.Sort((a, b) => b.itemType - a.itemType);
                    break;
                default:

                    break;
            }
        }
    }
}
