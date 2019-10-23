using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackGround
{
    GameObject backGround;
    public TitleBackGround()
    {
        backGround = GameObject.Find("BackGround");
    }
    public void Update()
    {
        float scr = Mathf.Repeat(Time.time * 0.1f, 1);
        Vector2 off = new Vector2(scr, 0);
        backGround.gameObject.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", off);
    }
}
