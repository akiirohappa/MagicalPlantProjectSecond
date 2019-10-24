using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TitleObjText : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void SetText(string str)
    {
        text.text = str;
    }
    public void Break()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
