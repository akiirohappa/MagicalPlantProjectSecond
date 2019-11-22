using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class AudioPlayer : EditorWindow
{
    [SerializeField]
    public static AudioClip clip;
    [SerializeField]
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
                s.gameObject.hideFlags = HideFlags.HideAndDontSave;
                s.volume = 0.1f;
            }
            return s;
        }
        set
        {
            s = value;
        }
    }

    public static GameObject g;
    static int playNum = 0;
    int PlayNumChange
    {
        get
        {
            return playNum;
        }
        set
        {
            if (value == Audios.Count)
            {
                PlayNumChange = 0;
            }
            else if (value == -1)
            {
                PlayNumChange = Audios.Count - 1;
            }
            else
            {
                clip = Audios[value];
                Source.clip = clip;
                playNum = value;
            }
        }
    }
    static bool windowOpen = false;
    [SerializeField]
    static List<AudioClip> sl;
    static List<AudioClip> Audios
    {
        get
        {
            if(sl == null)
            {
                SoundList s = Resources.LoadAll<SoundList>("Extra")[0];
                sl = new List<AudioClip>();
                foreach(SoundData bgm in s.BGMs)
                {
                    sl.Add(bgm.audio);
                }
            }
            return sl;
        }
        set
        {
        
        }
    }
    Vector2 windowSize = new Vector2(300f, 200f);
    Vector2 soundView = Vector2.zero;
    bool listOpen;

    static void GamePlayAct(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            DontDestroyOnLoad(g);
        }
    }
    [MenuItem("Sound/Player _F9")]
    static void Open()
    {
        if (!windowOpen)
        {
            windowOpen = true;
            EditorWindow w = GetWindow<AudioPlayer>("サウンドプレイヤー☆彡");
            w.maxSize = new Vector2(300f, 200f);
            w.minSize = new Vector2(300f, 200f);
        }
        else
        {
            windowOpen = false;
            EditorWindow w = GetWindow<AudioPlayer>("サウンドプレイヤー☆彡");
            w.Close();
        }
        EditorApplication.playModeStateChanged += GamePlayAct;
    }

    [MenuItem("Sound/Play _F11")]
    static void Play()
    {
        if (Audios.Count == 0)
        {
            return;
        }
        GameObject g = GameObject.Find("Audio");
        if(g == null)
        {
            return;
        }
        AudioSource s = g.GetComponent<AudioSource>();
        if(s == null)
        {
            return;
        }
        if (s.isPlaying)
        {
            s.Pause();
        }
        else
        {
            s.Play();
        }
    }

    [MenuItem("Sound/+ _F12")]
    static void MusicPlus()
    {
        if (Audios.Count == 0)
        {
            return;
        }
        if (playNum + 1 == Audios.Count)
        {
            playNum = 0;
        }
        else
        {
            playNum++;
        }
        GameObject g = GameObject.Find("Audio");
        if (g == null)
        {
            return;
        }
        AudioSource s = g.GetComponent<AudioSource>();
        if (s == null)
        {
            return;
        }
        clip = Audios[playNum];
        s.clip = clip;
        if (s.isPlaying)
        {
            Play();
        }
    }

    [MenuItem("Sound/- _F10")]
    static void MusicMinus()

    {
        if (Audios.Count == 0)
        {
            return;
        }
        if (playNum  == 0)
        {
            playNum = Audios.Count-1;
        }
        else
        {
            playNum--;
        }
        GameObject g = GameObject.Find("Audio");
        if (g == null)
        {
            return;
        }
        AudioSource s = g.GetComponent<AudioSource>();
        if (s == null)
        {
            return;
        }
        clip = Audios[playNum];
        s.clip = clip;
        if (s.isPlaying)
        {
            Play();
        }
    }
    
    private void OnGUI()
    {
        if(clip == null)
        {
            clip = Audios[0];
            Source.clip = clip;
        }
        Source.volume = EditorGUILayout.Slider("音量", Source.volume, 0, 1);
        Source.loop = EditorGUILayout.Toggle("ループ", Source.loop);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("⇦", GUILayout.Width(40), GUILayout.Height(40)))
        {
            if(Audios.Count != 0)
            {
                PlayNumChange--;
                if (!Source.isPlaying)
                {
                    Source.time = 0;
                    Source.Play();
                }
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
        if (GUILayout.Button("⇨", GUILayout.Width(40), GUILayout.Height(40)))
        {
            if (Audios.Count != 0)
            {
                PlayNumChange++;
                if (!Source.isPlaying)
                {
                    Source.time = 0;
                    Source.Play();
                }
            }

        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("停止"))
        {
            DestroyImmediate(Source.gameObject);
        }
        GUILayout.Space(28);
        EditorGUILayout.BeginVertical();
        if (Source.clip != null)
        {
            EditorGUILayout.LabelField(playNum+1 + "曲目　"+(Source.isPlaying ? "　　再生中" : "一時停止中") + "：" + clip.name);
            float f;
            f = EditorGUILayout.Slider(Source.time,0, Source.clip.length);
            if(f != Source.time)
            {
                Source.time = f;
            }

        }
        else
        {
            GUILayout.Space(32);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("リスト"))
        {
            listOpen = !listOpen;
            minSize = maxSize = (listOpen ? new Vector2(windowSize.x,windowSize.y+200) : windowSize);
        }
        EditorGUILayout.EndHorizontal();
        soundView = EditorGUILayout.BeginScrollView(soundView, GUILayout.Height(200));
        if (listOpen)
        {
            for (int i = 0; i < Audios.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i + 1) + "曲目", GUILayout.Width(100));
                EditorGUILayout.ObjectField(Audios[i], typeof(AudioClip), true);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
    }
}


[InitializeOnLoad]
public static class AudioPlayerSub
{
    static AudioPlayerSub()
    {
        EditorApplication.playModeStateChanged += ToDontDestroy;
    }
    static void ToDontDestroy(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            GameObject g = GameObject.Find("Audio");
            
            if (g == null)
            {
                return;
            }
            AudioSource s = g.GetComponent<AudioSource>();
            if (s == null)
            {
                return;
            }
            GameObject.DontDestroyOnLoad(g);
            //GameObject.DontDestroyOnLoad(s);
        }
    }
}