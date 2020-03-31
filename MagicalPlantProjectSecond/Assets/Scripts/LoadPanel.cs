using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    Animator[] loadingTexts;
    public bool isEnd = true;
    float y;
    private void Awake()
    {
        loadingTexts = new Animator[transform.GetChild(0).childCount];
        for(int i = 0;i < loadingTexts.Length; i++)
        {
            loadingTexts[i] = transform.GetChild(0).GetChild(i).GetComponent<Animator>();
        }
        y = loadingTexts[0].transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator LoadingTextAnimation()
    {
        gameObject.SetActive(true);

        if (isEnd)
        {
            isEnd = false;
            for (int i = 0; i < loadingTexts.Length; i++)
            {
                loadingTexts[i].SetTrigger("Go");
                yield return new WaitForSeconds(0.5f);
            }
            isEnd = true;
        }
        for (int i = 0; i < loadingTexts.Length; i++)
        {
            loadingTexts[i].transform.localPosition = new Vector3(loadingTexts[i].transform.localPosition.x, y);

            //Debug.Log(i + ":" + loadingTexts[i].transform.position);
        }
    }
}
