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
    public PeforManceDataBase[] PeforMances;

    public GameObject listParent;
    public GameObject peforManceButton;


    public PeforManceManager(MenuManager m):base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Peformance").gameObject;
        ItemButtons = new List<Button>();
        PeformanceButtons = new List<Button>();
        PeforMances = Resources.LoadAll<PeforManceDataBase>("PeforMance");
        peforManceButton = Resources.Load<GameObject>("Prefabs/PefoeManceButton");
        listParent = myObjct.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject;
        for (int i = 0;i < PeforMances.Length; i++)
        {
            PeformanceButtons.Add(GameObject.Instantiate(peforManceButton, listParent.transform).GetComponent<Button>());
            PeformanceButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PeforMances[i].Title;
        }
        
       
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
        if(state == PeforManceMenuState.PeforMance)
        {
            

        }



    }
}
