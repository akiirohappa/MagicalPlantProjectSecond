using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager:MenuManagerBase
{
    public ItemManager(MenuManager m):base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Item").gameObject;
    }
    public override void Open()
    {
        base.Open();
    }
    public override void Submit()
    {

    }
    public override void Cancel()
    {

    }
    public override void Button(string state)
    {

    }
}
