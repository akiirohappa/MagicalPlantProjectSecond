using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEVButton : MonoBehaviour
{
    public string eventCode;
    public MapEventBase eventB;
    public void ButtonPressd()
    {
        eventB.EventStart(eventCode);
    }
}
