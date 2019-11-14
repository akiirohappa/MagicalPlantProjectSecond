using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SoundData
{
    [SerializeField]
    public string key;
    [SerializeField]
    public AudioClip audio;
    public SoundData()
    {
        key = "";
    }
}
