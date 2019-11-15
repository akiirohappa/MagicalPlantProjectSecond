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
        BGMVol = -20f;
        SEVol = -20f;
    }
    public void CoufigLoad()
    {
        DontDestroyManager.my.Sound.Mixer.GetFloat("MasterVolume",out MasterVol);
        DontDestroyManager.my.Sound.Mixer.GetFloat("BGMVolume", out BGMVol);
        DontDestroyManager.my.Sound.Mixer.GetFloat("SEVolume", out SEVol);
    }
}
