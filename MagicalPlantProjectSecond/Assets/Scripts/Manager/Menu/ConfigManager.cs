using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MenuManagerBase
{
    public enum ConfigType
    {
        Sound,
    }
    ConfigType nowType;
    ConfigData config;
    SoundManager sound;
    GameObject[] soundSliders;
    public ConfigManager(MenuManager m) : base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Config").gameObject;
        soundSliders = new GameObject[myObjct.transform.GetChild(2).childCount];
        for(int i = 0;i <  myObjct.transform.GetChild(2).childCount; i++)
        {
            soundSliders[i] = myObjct.transform.GetChild(2).GetChild(i).gameObject;
        }
        nowType = ConfigType.Sound;
        sound = DontDestroyManager.my.Sound;
    }
    public override void Open()
    {
        base.Open();
        ConfigData c = new ConfigData();
        c.CoufigLoad();
        config = c;
    }
    public override void Submit()
    {

    }
    public override void Cancel()
    {

    }
    public override void Button(string state)
    {
        base.Button(state);
    }
    public override void SliderChange(float f)
    {
        switch (nowType)
        {
            case ConfigType.Sound:
                int num = -1;
                for(int i = 0;i < soundSliders.Length; i++)
                {
                    if(mManager.Cullent == soundSliders[i])
                    {
                        num = i;
                    }
                }
                switch (num)
                {
                    case 0:
                        sound.SetVolume(SoundType.Master, f);
                        break;
                    case 1:
                        sound.SetVolume(SoundType.BGM, f);
                        break;
                    case 2:
                        sound.SetVolume(SoundType.SE, f);
                        break;
                    default:
                        break;
                }
                break;
        }
    }
}
