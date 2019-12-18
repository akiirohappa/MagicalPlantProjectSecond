using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogManager
{
    GameObject logParent;
    GameObject logDataobj;
    public LogManager()
    {
        logParent = GameObject.Find("LogPanel");
        logDataobj = Resources.Load<GameObject>("Prefabs/LogData");
    }
    public void LogMake(string text,Sprite image)
    {
        GameObject g = GameObject.Instantiate(logDataobj,logParent.transform);
        g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        g.transform.GetChild(1).GetComponent<Image>().sprite = image;
        if (logParent.transform.childCount == 1)
        {
            logParent.transform.GetChild(0).GetComponent<Log>().AnimStart(this);
        }
    }
    public void LogDeleteStart()
    {
        if(logParent.transform.childCount != 1)
        {
            logParent.transform.GetChild(1).GetComponent<Log>().AnimStart(this);
        }
    }

}
