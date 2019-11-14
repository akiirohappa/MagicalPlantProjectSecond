using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SE;
    [SerializeField] AudioMixer audioMixer;
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
        BGM.loop = loop;
        BGM.Play();
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
        SE.Play();
    }
    public IEnumerator FadeIn()
    {
        fadeNowValue = -80;
        while (true)
        {
            fadeNowValue += 0.15f;
            SetVolume("BGMVolume", fadeNowValue);
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
            SetVolume("BGMVolume", fadeNowValue);
            if (fadeNowValue <= -80)
            {
                break;
            }
            yield return null;
        }
    }
    //音量調整
    public void SetVolume(string name,float vol)
    {
        audioMixer.SetFloat(name, vol);
    }
}
