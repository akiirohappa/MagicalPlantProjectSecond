using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HotBarView
{
    GameObject myobj;
    public HotBarView()
    {
        myobj = GameObject.Find("Canvas").transform.Find("Menu").transform.Find("HotBar").gameObject;
    }
    public void SetHotBar(string leftMenu,string rightMenu)
    {
        if (!myobj.activeSelf)
        {
            myobj.SetActive(true);
        }
        myobj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "左クリック："+leftMenu;
        myobj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "右クリック：" + rightMenu;
    }
    public void SetHideHotBar(bool b)
    {
        myobj.SetActive(b);
    }
}
