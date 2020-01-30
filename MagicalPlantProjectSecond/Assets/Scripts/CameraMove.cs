using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject mycamera = null;
    [SerializeField] GameObject mainPos = null;
    [SerializeField] GameObject homePos = null;
    [SerializeField] GameObject toMainBT = null;
    [SerializeField] GameObject toHomeBT = null;
    [SerializeField] Image blackOut = null;
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
        for(int i = 0;i <= 50; i++)
        {
            blackOut.fillAmount = (float)i / 50;
            yield return new WaitForSeconds(Time.fixedDeltaTime/10);
        }
        mycamera.transform.position = toPos.transform.position;
        toHomeBT.gameObject.SetActive(!toHomeBT.gameObject.activeSelf);
        toMainBT.gameObject.SetActive(!toHomeBT.gameObject.activeSelf);
        yield return new WaitForSeconds(0.5f);
        blackOut.transform.rotation = Quaternion.Euler(0, 180, 0);
        for (int i = 50; i >= 0; i--)
        {
            blackOut.fillAmount = (float)i / 50;
            yield return new WaitForSeconds(Time.fixedDeltaTime/10);
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
