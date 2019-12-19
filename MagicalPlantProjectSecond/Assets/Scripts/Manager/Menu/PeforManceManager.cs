using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PeforManceMenuState
{
    PeforMance,
    Library,
}
public class PeforManceManager : MenuManagerBase
{
    PeforManceMenuState state;
    List<Button> ItemButtons;
    List<Button> PeformanceButtons;
    public Dictionary<string, PeforManceDataBase> PeforMances;

    public PeforManceManager(MenuManager m):base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Peformance").gameObject;
        ItemButtons = new List<Button>();
        PeformanceButtons = new List<Button>();
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
        var buttons = (state == PeforManceMenuState.PeforMance ? PeformanceButtons:ItemButtons);
        



    }
}
