using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    TitleBackGround bg;
    GameObject plantObj;
    LoadManager load;
    // Start is called before the first frame update
    void Start()
    {
        bg = new TitleBackGround();
        plantObj = Resources.Load<GameObject>("Prefabs/TitleBackObj");
        load = new LoadManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        bg.Update();
        BackObjMake();
    }
    void BackObjMake()
    {
        if(Random.Range(0,300) < 1)
        {
            float posy = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.y;
            float pos = Random.Range(100, posy-200);
            GameObject g = Instantiate(plantObj, new Vector3(900, pos),new Quaternion(),GameObject.Find("BackGroundObj").transform);
            g.GetComponent<TitlePlantObj>().SetPlantImage(Resources.Load<Sprite>("PlantImages/kab"));
        }
    }
    public void NewGame()
    {
        DontDestroyManager.my.Scene.LoadScene("Main");
    }
    public void LoadGame()
    {
        load.Open();
    }
    public void CloseLoad()
    {
        load.Close();
    }
    public void SelectSaveData(int num)
    {
        load.cullentDataNum = num;
        load.SaveDataShow(true);
    }
    public void SaveSubmit()
    {
        LoadMainScene(load.GetSaveData());
    }
    public void SaveCancel()
    {
        load.SaveDataShow(false);
    }
    void LoadMainScene(SaveData sd)
    {
        DontDestroyManager.my.Scene.LoadScene("Main",sd);
    }
    public void SaveReset()
    {
        PlayerPrefs.DeleteAll();
    }
}
