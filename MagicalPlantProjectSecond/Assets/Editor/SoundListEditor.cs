using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundList))]
public class SoundListEditor : Editor
{
    SoundList list;
    SerializedObject sound;
//    SerializedProperty sound;
    EditorStyle style;
    bool foutB = false;
    bool foutS = false;
    public override void OnInspectorGUI()
    {
        if (style == null)
        {
            style = Resources.Load<EditorStyle>("EditorStyle");
        }
        list = target as SoundList;
        style.Setup();
        if(list.BGMs == null)
        {
            list.BGMs = new List<SoundData>();
        }
        if(list.SEs == null)
        {
            list.SEs = new List<SoundData>();
        }
        EditorGUILayout.BeginHorizontal(style.skin.box, GUILayout.Height(35));
        {
            EditorGUILayout.LabelField("サウンドデータ☆彡",style.Styles["Center"],GUILayout.Height(35));
            if (GUILayout.Button("セーブ", GUILayout.Width(200),GUILayout.Height(35)))
            {
                EditorUtility.SetDirty(list);
                AssetDatabase.SaveAssets();
                Debug.Log("セーブが完了しました！");
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(style.skin.box,GUILayout.Height(35)); {
            if(GUILayout.Button((foutB ? "▼":"▶") + "BGM" + "　合計" + list.BGMs.Count + "個",style.Styles["Button"],GUILayout.Height(35)))
            {
                foutB = !foutB;
            }
            if (GUILayout.Button("追加", GUILayout.Width(35),GUILayout.Height(35)))
            {
                list.BGMs.Add(new SoundData());
            }
        }
        EditorGUILayout.EndHorizontal();
        if(foutB)
        {
            if (list.BGMs.Count == 0)
            {
                EditorGUILayout.BeginHorizontal(style.Styles["Child"],GUILayout.Height(35));
                {
                    EditorGUILayout.LabelField("リストが空です",style.Styles["Center"],GUILayout.Height(35));
                    EditorGUILayout.EndHorizontal();
                }
            }
            for (int i = 0; i < list.BGMs.Count; i++)
            {
                EditorGUILayout.BeginHorizontal(style.Styles["Child"]);
                {
                    EditorGUILayout.BeginVertical();
                    list.BGMs[i].key = EditorGUILayout.TextField("名前", list.BGMs[i].key);
                    list.BGMs[i].audio = EditorGUILayout.ObjectField("BGM", list.BGMs[i].audio, typeof(AudioClip), true) as AudioClip;
                    EditorGUILayout.EndVertical();
                    if (GUILayout.Button("×", GUILayout.Width(35), GUILayout.Height(35)))
                    {
                        list.BGMs.Remove(list.BGMs[i]);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.BeginHorizontal(style.skin.box, GUILayout.Height(35));
        {
            if (GUILayout.Button((foutS ? "▼" : "▶") + "SE" + "　合計" + list.SEs.Count + "個", style.Styles["Button"], GUILayout.Height(35)))
            {
                foutS = !foutS;
            }
            if (GUILayout.Button("追加", GUILayout.Width(35), GUILayout.Height(35)))
            {
                list.SEs.Add(new SoundData());
                
            }
        }
        EditorGUILayout.EndHorizontal();
        if (foutS)
        {
            if (list.SEs.Count == 0)
            {
                EditorGUILayout.BeginHorizontal(style.Styles["Child"], GUILayout.Height(35));
                {
                    EditorGUILayout.LabelField("リストが空です", style.Styles["Center"], GUILayout.Height(35));
                    EditorGUILayout.EndHorizontal();
                }
            }
            for (int i = 0; i < list.SEs.Count; i++)
            {
                EditorGUILayout.BeginHorizontal(style.Styles["Child"]);
                {
                    EditorGUILayout.BeginVertical();
                    list.SEs[i].key = EditorGUILayout.TextField("名前", list.SEs[i].key);
                    list.SEs[i].audio = EditorGUILayout.ObjectField("SE", list.SEs[i].audio, typeof(AudioClip), true) as AudioClip;
                    EditorGUILayout.EndVertical();
                    if (GUILayout.Button("×", GUILayout.Width(35), GUILayout.Height(35)))
                    {
                        list.SEs.Remove(list.SEs[i]);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

}
