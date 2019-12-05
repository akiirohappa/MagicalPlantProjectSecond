using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject mycamera;
    [SerializeField] GameObject mainPos;
    [SerializeField] GameObject homePos;
    [SerializeField] GameObject toMainBT;
    [SerializeField] GameObject toHomeBT;
    [SerializeField] Image blackOut;
    // Start is called before the first frame update
    void Start()
    {
        toHomeBT.gameObject.SetActive(true);
        toMainBT.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MoveBlackOut(GameObject toPos)
    {
        MainManager.GetInstance.timeMove = false;
        DontDestroyManager.my.Sound.PlaySE("EreaChange");
        blackOut.gameObject.SetActive(true);
        blackOut.transform.rotation = Quaternion.Euler(0,0,0);
        for(int i = 0;i <= 100; i++)
        {
            blackOut.fillAmount = (float)i / 100;
            yield return null;
        }
        mycamera.transform.position = toPos.transform.position;
        toHomeBT.gameObject.SetActive(!toHomeBT.gameObject.activeSelf);
        toMainBT.gameObject.SetActive(!toHomeBT.gameObject.activeSelf);
        yield return new WaitForSeconds(0.5f);
        blackOut.transform.rotation = Quaternion.Euler(0, 180, 0);
        for (int i = 100; i >= 0; i--)
        {
            blackOut.fillAmount = (float)i / 100;
            yield return null;
        }
        MainManager.GetInstance.timeMove = true;
        blackOut.gameObject.SetActive(false);
    } 
    public void ToHome()
    {
        StartCoroutine(MoveBlackOut(homePos));

    }
    public void ToMain()
    {
        if (TimeManager.GetInstance().sleep)
        {
            return;
        }
        StartCoroutine(MoveBlackOut(mainPos));
    }
}
