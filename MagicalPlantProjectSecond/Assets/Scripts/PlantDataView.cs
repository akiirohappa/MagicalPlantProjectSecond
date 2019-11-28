//データを表示する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantDataView
{
    GameObject myObj;
    bool viewIsOpen = false;
    public PlantDataView()
    {
        myObj =  GameObject.Instantiate(Resources.Load<GameObject>("PlantData"),GameObject.Find("Menu").transform);
        myObj.SetActive(false);
    }
    public void DataPreview(Plant plant)
    {
        PlantVSetActive(true);
        myObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = plant.name;
        myObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "成長度：" + plant.nowGrowth;
        myObj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "品質：" + plant.quality;
        myObj.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "土：<br>" + (plant.soilState == Soil.Dry ? "乾いている" : "湿っている");
        myObj.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "成長速度：<br>" + plant.growthSpeed + "%/日";
    }
    public void PlantVSetActive(bool b)
    {
        viewIsOpen = b;
        myObj.SetActive(b);
    }
    public bool GetIsView()
    {
        return viewIsOpen;
    }
}
