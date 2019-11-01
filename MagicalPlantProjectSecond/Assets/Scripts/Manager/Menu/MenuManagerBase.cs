//------------------------------------------------------------------------------
//メニュー画面：基本編。
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  MenuManagerBase
{
    //MenuManagerを入れておく。
    protected MenuManager mManager;
    //それぞれのメニュー部分のオブジェクト。
    protected GameObject myObjct;
    public MenuManagerBase(MenuManager menu)
    {
        mManager = menu;
    }
    public abstract void Submit();
    public abstract void Cancel();
    public virtual void Button(string state)
    {
        Debug.Log("空:0" + state);
    }
    public virtual void PlessItemButton(Item item)
    {
        Debug.Log("アイテムボタンの処理が空");
    }
    public virtual void Open()
    {
        myObjct.SetActive(true);
    }
    public virtual void Open(Vector3Int p)
    {
        myObjct.SetActive(true);
    }
    public void Close()
    {
        myObjct.SetActive(false);
    }
}
