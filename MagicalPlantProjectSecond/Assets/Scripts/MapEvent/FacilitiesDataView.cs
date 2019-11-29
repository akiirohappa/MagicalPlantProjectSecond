using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FacilitiesDataView
{
    public GameObject myobj;
    public FacilitiesDataView()
    {
        myobj = GameObject.Find("Canvas").transform.Find("Menu").transform.Find("FacilitiesView").gameObject;
    }
    public void SetData(FacilitiesData fd)
    {
        if (!myobj.activeSelf)
        {
            myobj.SetActive(true);
        }
        myobj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = fd.name;
        myobj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "レベル：" + fd.nowLevel + "/" + fd.maxLevel;
        myobj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = fd.infoText;
        myobj.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = fd.valueText;
        myobj.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = (fd.nowLevel == fd.maxLevel ?  "レベルは最大です":"レベルアップ：\n" +fd.levelUpPrice[fd.nowLevel] + "株");
    }
    public void SetHideHotBar(bool b)
    {
        myobj.SetActive(b);
    }
}
