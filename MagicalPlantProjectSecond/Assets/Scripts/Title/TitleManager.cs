using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    TitleBackGround bg;
    GameObject plantObj;
    LoadManager load;
    RectTransform rect;
    Sprite[] plantImages;
    // Start is called before the first frame update
    void Start()
    {
        
        bg = new TitleBackGround();
        plantObj = Resources.Load<GameObject>("Prefabs/TitleBackObj");
        load = new LoadManager(this);
        rect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        plantImages = Resources.LoadAll<Sprite>("PlantImages");
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
            float pos = Random.Range(-310,310);
            GameObject g = Instantiate(plantObj,GameObject.Find("ItemPos").transform);
            g.transform.localPosition = new Vector3(50, pos);
            g.transform.SetParent(GameObject.Find("BackGroundObj").transform);
            g.GetComponent<TitlePlantObj>().SetPlantImage(plantImages[Random.Range(0,plantImages.Length)]);
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
    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
        Application.Quit();
#endif
    }
    public void SaveReset()
    {
        PlayerPrefs.DeleteAll();
    }
}
