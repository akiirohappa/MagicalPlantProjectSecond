using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleManager : MonoBehaviour
{
    TitleBackGround bg;
    GameObject plantObj;
    // Start is called before the first frame update
    void Start()
    {
        bg = new TitleBackGround();
        plantObj = Resources.Load<GameObject>("Prefabs/TitleBackObj");
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
            Debug.Log(posy);
            float pos = Random.Range(100, posy-200);
            GameObject g = Instantiate(plantObj, new Vector3(900, pos),new Quaternion(),GameObject.Find("BackGroundObj").transform);
        }
    }
}
