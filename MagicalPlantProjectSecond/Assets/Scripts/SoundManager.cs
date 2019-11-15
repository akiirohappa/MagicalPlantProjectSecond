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
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SE;
    [SerializeField] AudioMixer audioMixer;
    public AudioMixer Mixer
    {
        get { return audioMixer;  }
    }
    [SerializeField] SoundList sound;
    [SerializeField] float fadeNowValue = 0f;
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
    public IEnumerator FadeIn()
    {
        fadeNowValue = -80;
        while (true)
        {
            fadeNowValue += 0.15f;
            SetVolume(SoundType.BGM, fadeNowValue);
            if(fadeNowValue >= 0)
            {
                break;
            }
            yield return null;
        }
    }
    public IEnumerator FadeOut()
    {
        fadeNowValue = 0;
        while (true)
        {
            fadeNowValue -= 0.15f;
            SetVolume(SoundType.BGM, fadeNowValue);
            if (fadeNowValue <= -80)
            {
                break;
            }
            yield return null;
        }
    }
    //音量調整
    public void SetVolume(SoundType s,float vol)
    {
        string name;
        switch (s)
        {
            case SoundType.BGM:
                name = "BGMVolume";
                break;
            case SoundType.SE:
                name = "SEVolume";
                break;
            case SoundType.Master:
                name = "MasterVolume";
                break;
            default:
                name = "";
                break;
        }
        if(name != "")
        {
            audioMixer.SetFloat(name, vol);
        }
        else
        {
            Debug.Log("再生できません。");
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
