using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigData
{
    public float MasterVol;
    public float BGMVol;
    public float SEVol;
    public ConfigData()
    {
        MasterVol = -20f;
        BGMVol = 0.5f;
        SEVol = 0.5f;
    }
    public void CoufigLoad()
    {
        DontDestroyManager.my.Sound.Mixer.GetFloat("MasterVolume",out MasterVol);
        BGMVol = DontDestroyManager.my.Sound.Bgm.volume;
        SEVol = DontDestroyManager.my.Sound.Se.volume;
    }
}
