//-----------------------------------------------------------------------
//アイテムのソートをする
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public enum SortState
{
    ItemName,
    ItemNum,
    ItemType,
    ItemValue,
    GrowthSpeed,
}
public class ItemListSort :MonoBehaviour 
{
    public ItemList item;
    public SortState state;
    public SortState[] states;
    public bool IsUpper;
    void Start()
    {
        
    }
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
                case SortState.ItemValue:
                    item.Item.Sort((a, b) => a.defaltValue - b.defaltValue);
                    break;
                case SortState.GrowthSpeed:
                    item.Item.Sort((a, b) => (int)a.growthSpeed - (int)b.growthSpeed);
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
                case SortState.ItemValue:
                    item.Item.Sort((a, b) => b.defaltValue - a.defaltValue);
                    break;
                case SortState.GrowthSpeed:
                    item.Item.Sort((a, b) => (int)b.growthSpeed - (int)a.growthSpeed);
                    break;
                default:

                    break;
            }
        }
        GameObject.Find("Manager").GetComponent<MenuManager>().ButtonEx("Reset");
    }
    public void SetSortMode(int value)
    {
        state = states[value];
    }
    public void SetUpperMode(bool value)
    {
        IsUpper = value;
    }
}
