using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    Animator[] loadingTexts;
    bool isEnd = true;
    private void Awake()
    {
        loadingTexts = new Animator[transform.GetChild(0).childCount];
        for(int i = 0;i < loadingTexts.Length; i++)
        {
            loadingTexts[i] = transform.GetChild(0).GetChild(i).GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator LoadingTextAnimation()
    {
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
    }
}
