using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePlantObj : MonoBehaviour
{
    Sprite plantImage;
    RectTransform pos;
    GameObject text;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<RectTransform>();
        text = Resources.Load<GameObject>("Prefabs/TitleBObjText");
        speed = Screen.width /  10f * (1f/60f);
    }
    public void SetPlantImage(Sprite img)
    {
        plantImage = img;
        transform.GetChild(0).GetComponent<Image>().sprite = plantImage;
    }
    private void FixedUpdate()
    {
        pos.transform.position = new Vector3(pos.position.x-speed,pos.position.y);
        pos.transform.GetChild(0).Rotate(new Vector3(0, 0, 0.50f));
        if(pos.transform.position.x < -50)
        {
            Destroy(gameObject);
        }
    }
    public void OnClickedEvent()
    {
        Instantiate(text, transform).GetComponent<TitleObjText>().SetText("ｺﾝﾆﾁﾜ");
    }
}
