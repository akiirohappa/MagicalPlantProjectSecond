using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioList", menuName = "AudioList")]
public class AudioList : ScriptableObject
{
    public AudioClip[] audios;
}