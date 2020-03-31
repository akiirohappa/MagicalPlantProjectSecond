using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public enum SoundType
{
    Master,
    BGM,
    SE,

}
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource BGM = null;
    [SerializeField] AudioSource SE = null;
    [SerializeField] AudioMixer audioMixer = null;
    public AudioMixer Mixer
    {
        get { return audioMixer;  }
    }
    public AudioSource Bgm
    {
        get
        {
            return BGM;
        }
    }
    public AudioSource Se
    {
        get
        {
            return SE;
        }
    }
    [SerializeField] SoundList sound = null;
    [SerializeField] float fadeNowValue = 0f;
    [SerializeField] float fadeTime = 1; 
    //BGMを再生、ループはデフォルトでtrue
    //keyに"Stop"と入れると停止
    public void PlayBGM(string key,bool loop = true)
    {
        if(key == "Stop")
        {
            BGM.Stop();
        }
        AudioClip clip = null;
        for(int i = 0;i < sound.BGMs.Count; i++)
        {
            if(key == sound.BGMs[i].key)
            {
                clip = sound.BGMs[i].audio;
                break;
            }
        }
        if(clip == null)
        {
            Debug.Log(key + "は見つかりませんでした");
        }
        else
        {
            BGM.clip = clip;
            BGM.loop = loop;
            BGM.Play();
        }

    }
    //SEを再生
    public void PlaySE(string key)
    {
        AudioClip clip = null;
        for (int i = 0; i < sound.SEs.Count; i++)
        {
            if (key == sound.SEs[i].key)
            {
                clip = sound.SEs[i].audio;
                break;
            }
        }
        if (clip == null)
        {
            Debug.Log(key + "は見つかりませんでした");
        }
        else
        {
            //SE.clip = clip;
            SE.PlayOneShot(clip);
        }
       
    }
    public void BGMChange(string key)
    {
        StartCoroutine(BGMFadeChange(key));
    }
    public IEnumerator BGMFadeChange(string key)
    {
		float volume = Bgm.volume;
        Coroutine cor =  StartCoroutine(FadeOut(volume,fadeTime));
        yield return cor;
        PlayBGM(key);
        cor = StartCoroutine(FadeIn(volume, fadeTime));
    }
    public IEnumerator FadeIn(float volume, float time)
    {
		if(volume != 0)
		{
			float fadeValue = volume;
			for (float i = 0; i < time; i += UnityEngine.Time.fixedDeltaTime)
			{
				SetVolume(SoundType.BGM, fadeValue * i);
				yield return null;
			}
		}

    }
    public IEnumerator FadeOut(float volume,float time)
    {
        float fadeValue = volume;

		for (float i = 0; i < time; i += Time.fixedDeltaTime)
        {
			if(volume != 0)
			{
				SetVolume(SoundType.BGM, volume - fadeValue * i);
			}
            
            yield return null;
        }
    }
    //音量調整
    public void SetVolume(SoundType s,float vol)
    {
		Debug.Log(s + ":" + vol);
        switch (s)
        {
            case SoundType.BGM:
                BGM.volume = vol;
                break;
            case SoundType.SE:
                SE.volume = vol;
                break;
            case SoundType.Master:
                audioMixer.SetFloat("MasterVolume", vol);
                break;
            default:
                break;
        }
    }
    //コンフィグデータから音量丸ごとセット
    public void ConfigSet(ConfigData c)
    {
        SetVolume(SoundType.Master, c.MasterVol);
        SetVolume(SoundType.BGM, c.BGMVol);
        SetVolume(SoundType.SE, c.SEVol);
    }
}
