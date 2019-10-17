using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    bool MenuOpen = false;
    GameObject openButton;
    // Start is called before the first frame update
    void Start()
    {
        openButton = GameObject.Find("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MenuButton()
    {
        if (!MenuOpen)
        {
            openButton.GetComponent<Animator>().SetTrigger("Open");
            MenuOpen = true;
            openButton.transform.GetChild(0).GetComponent<Text>().text = "閉じる";
        }
        else
        {
            openButton.GetComponent<Animator>().SetTrigger("Close");
            MenuOpen = false;
            openButton.transform.GetChild(0).GetComponent<Text>().text = "メニュー";
        }
    }
}
