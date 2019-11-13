using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundList", menuName = "SoundList")]
//[SerializeField]
public class SoundList : ScriptableObject
{
    
    public List<SoundData> BGMs = new List<SoundData>();
    public List<SoundData> SEs = new List<SoundData>();
}