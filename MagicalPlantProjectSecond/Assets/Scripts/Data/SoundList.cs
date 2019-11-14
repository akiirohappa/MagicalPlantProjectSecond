using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundList", menuName = "SoundList")]
[System.Serializable]
public class SoundList : ScriptableObject
{
    int a;
    public List<SoundData> BGMs;
    public List<SoundData> SEs;
}