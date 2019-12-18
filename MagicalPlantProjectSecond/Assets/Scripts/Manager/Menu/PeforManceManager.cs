using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PeforManceMenuState
{
    PeforMance,
    Library,
}
public class PeforManceManager : MenuManagerBase
{
    PeforManceMenuState state;
    public PeforManceManager(MenuManager m):base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Peformance").gameObject;
    }
    public override void Open()
    {
        base.Open();
        state = PeforManceMenuState.PeforMance;
        MenuSet();
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
    void MenuSet()
    {
        
    }
}
