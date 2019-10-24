using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  MenuManagerBase
{
    protected MenuManager mManager;
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
    public virtual void Open()
    {
        myObjct.SetActive(true);
    }
}
