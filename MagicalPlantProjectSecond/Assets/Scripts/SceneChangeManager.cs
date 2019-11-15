using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    LoadPanel load;
    [SerializeField]float loadminTime = 3f;
    SaveAndLoad sl;
    private void Awake()
    {
        load = transform.GetChild(0).GetComponent<LoadPanel>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string name,SaveData s = null)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(Loading(name,s));
    }
    IEnumerator Loading(string name,SaveData s)
    {
        float time = 0;
        while (time <= loadminTime)
        {
            time += Time.deltaTime;
            StartCoroutine(load.LoadingTextAnimation());
            yield return null;
        }
        AsyncOperation loadasync = SceneManager.LoadSceneAsync(name);
        while (loadasync.isDone || time <= loadminTime)
        {
            time += Time.deltaTime;
            StartCoroutine( load.LoadingTextAnimation());
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);

        transform.GetChild(0).gameObject.SetActive(false);
        if (s != null)
        {
            sl = new SaveAndLoad();
            sl.SaveDataSet(s);
        }
    }
}
