using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePlantObj : MonoBehaviour
{
    Sprite plantImage;
    RectTransform pos;
    GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<RectTransform>();
        text = Resources.Load<GameObject>("Prefabs/TitleBObjText");
    }
    public void SetPlantImage(Sprite img)
    {
        plantImage = img;
        transform.GetChild(0).GetComponent<Image>().sprite = plantImage;
    }
    private void Update()
    {
        pos.transform.position = new Vector3(pos.position.x-0.25f,pos.position.y);
        pos.transform.GetChild(0).Rotate(new Vector3(0, 0, 0.25f));
        if(pos.transform.position.x < -150)
        {
            Destroy(gameObject);
        }
    }
    public void OnClickedEvent()
    {
        Instantiate(text, transform);
    }
}
