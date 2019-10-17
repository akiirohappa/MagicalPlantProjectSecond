//データを表示する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantDataView
{
    GameObject myObj;
    bool viewIsOpen = false;
    public PlantDataView()
    {
        myObj =  GameObject.Instantiate(Resources.Load<GameObject>("PlantData"),GameObject.Find("Canvas").transform);
        myObj.SetActive(false);
    }
    public void DataPreview(Plant plant)
    {
        PlantVSetActive(true);
        myObj.transform.GetChild(0).GetComponent<Text>().text = plant.name;
        myObj.transform.GetChild(1).GetComponent<Text>().text = "成長度：" + plant.nowGrowth;
        myObj.transform.GetChild(2).GetComponent<Text>().text = "品質：" + plant.quality;
        myObj.transform.GetChild(3).GetComponent<Text>().text = "土：";
        myObj.transform.GetChild(4).GetComponent<Text>().text = "成長速度：\n" + plant.growthSpeed + "%/日";
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
