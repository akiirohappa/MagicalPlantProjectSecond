using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WindowTest : EditorWindow
{

    
    SoundList sound;
    AudioClip clip;
    AudioSource s;
    AudioSource Source
    {
        get
        {
            
            if (s == null)
            {
                g = GameObject.Find("Audio");
                if (g == null)
                {
                    g = new GameObject("Audio");
                }
                s = g.GetComponent<AudioSource>();
                if (s == null)
                {
                    s = g.AddComponent<AudioSource>();
                }
                s.hideFlags = HideFlags.HideAndDontSave;
                s.volume = volume;
            }
            return s;
        }
        set
        {
            s = value;
        }
    }
    GameObject g;
    float volume = 0.1f;
    int playNum;
    int PlayNumChange
    {
        get
        {
            return playNum;
        }
        set
        {
            if (value == sound.BGMs.Count)
            {
                PlayNumChange = 0;
            }
            else if (value == -1)
            {
                PlayNumChange = sound.BGMs.Count - 1;
            }
            else
            {
                clip = sound.BGMs[value].audio;
                Source.clip = clip;
                playNum = value;
            }
        }
    }
    [MenuItem("Test/Test #p")]
    static void Open()
    {
        EditorWindow w = GetWindow<WindowTest>("サウンドプレイヤー☆彡");
        w.maxSize = new Vector2(300f, 200f);
        w.minSize = new Vector2(300f, 200f);
    }
    private void OnGUI()
    {
        if (sound == null)
        {
            sound = Resources.LoadAll<SoundList>("Extra")[0];
            PlayNumChange = 0;
        }
        SoundList sl = EditorGUILayout.ObjectField(sound, typeof(SoundList), true) as SoundList;

        if (sound != sl)
        {
            sound = sl;
            if(sound != null)
            {
                if(s != null)
                {
                    PlayNumChange = 0;
                }
            }
        }
        float f = EditorGUILayout.Slider("音量",volume, 0, 1);
        if(f != volume)
        {
            volume = f;
            Source.volume = volume;
        }
        Source.loop = EditorGUILayout.Toggle("ループ", Source.loop);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("⇦", GUILayout.Width(40),GUILayout.Height(40)))
        {
            PlayNumChange--;
            if (!Source.isPlaying)
            {
                Source.Play();
            }
            
        }
        if (!Source.isPlaying)
        {
            if (GUILayout.Button("再生", GUILayout.Height(40)))
            {
                Source.clip = clip;
                Source.Play();
            }
        }
        else
        {
            if (GUILayout.Button("一時停止", GUILayout.Height(40)))
            {
                Source.Pause();
            }
        }
        if (GUILayout.Button("⇨",GUILayout.Width(40), GUILayout.Height(40)))
        {
            PlayNumChange++;
            if (!Source.isPlaying)
            {
                Source.Play();
            }
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("停止"))
        {
            DestroyImmediate(Source.gameObject);
        }
        GUILayout.Space(30);
        EditorGUILayout.BeginVertical();
        if(clip != null)
        {
            EditorGUILayout.LabelField((Source.isPlaying ? "　　再生中":"一時停止中") + "：" + clip.name);
        }
        

        EditorGUILayout.EndVertical();
    }
}
