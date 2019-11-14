using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    LoadPanel load;
    [SerializeField]float loadminTime = 3f;
    private void Awake()
    {
        load = transform.GetChild(0).GetComponent<LoadPanel>();
        LoadScene("Main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string name)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(Loading(name));
    }
    IEnumerator Loading(string name)
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if(time >= loadminTime)
            {
                break;
            }
            StartCoroutine(load.LoadingTextAnimation());
            yield return null;
        }
        AsyncOperation loadasync = SceneManager.LoadSceneAsync(name);
        while (loadasync.isDone)
        {
            StartCoroutine( load.LoadingTextAnimation());
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
