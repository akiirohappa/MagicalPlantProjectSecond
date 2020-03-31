using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    LoadPanel load;
    [SerializeField]float loadminTime = 3f;
    SaveAndLoad sl;
    public SaveData s;
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
        StartCoroutine(MainLoading(name,s));
    }
    public void LoadScene()
    {
        StartCoroutine(TitleLoading());
    }
    IEnumerator MainLoading(string name,SaveData s)
    {
		float time = 0;
		StartCoroutine(load.LoadingTextAnimation());
        AsyncOperation loadasync = SceneManager.LoadSceneAsync(name);
		while (!loadasync.isDone && time <= loadminTime)
		{
			time += Time.deltaTime;
			yield return null;
		}
		
		if (s != null)
		{
			this.s = s;
			sl = new SaveAndLoad();
			sl.SaveDataSet(s);
		}
		else
		{
			TimeManager.GetInstance().TimeSet(TimeManager.GetInstance().Time);
		}
		DontDestroyManager.my.Sound.BGMChange("Main_" + TimeManager.GetInstance().Time.nowSeason.ToString());
		yield return new WaitForSeconds(0.5f);
		transform.GetChild(0).gameObject.SetActive(false);
		
    }
    IEnumerator TitleLoading()
    {
        float time = 0;
        StartCoroutine(load.LoadingTextAnimation());
        while (time <= loadminTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        AsyncOperation loadasync = SceneManager.LoadSceneAsync("Title");
        while (!loadasync.isDone && time <= loadminTime)
        {
            time += Time.deltaTime;
			//StartCoroutine(load.LoadingTextAnimation());
			if (load.isEnd)
			{
				StartCoroutine(load.LoadingTextAnimation());
			}
			yield return null;

        }
        DontDestroyManager.my.Sound.BGMChange("Title");
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        
    }
}
