using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class HelpWriter : EditorWindow
{
    [MenuItem("Editor/HelpWriter %e")]
    public static void Open()
    {
        EditorWindow w = GetWindow<HelpWriter>("ヘルプライター☆彡");
        w.maxSize = new Vector2(500f, 600f);
        w.minSize = new Vector2(500f, 600f);
    }
    HelpItem help;
    Vector2 vec;
    private void OnGUI()
    {
        if(help == null)
        {
            help = new HelpItem();
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("セーブ",GUILayout.Height(30)))
        {
            string path = EditorUtility.SaveFilePanel("セーブ", "Assets/Resource","New Help","json");
            if (!string.IsNullOrEmpty(path))
            {
                string json = JsonUtility.ToJson(help);
                File.WriteAllText(path,json);
            }
            else
            {
                Debug.LogWarning("セーブパスが空です。");
            }
        }
        if (GUILayout.Button("ロード", GUILayout.Height(30)))
        {
            string path = EditorUtility.OpenFilePanel("ロード", "Assets/Resource", "json");
            if (!string.IsNullOrEmpty(path))
            {
                string json = File.ReadAllText(path);
                help = JsonUtility.FromJson<HelpItem>(json);
            }
            else
            {
                Debug.LogWarning("ロードパスが空です。");
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("名前",GUILayout.Width(50));
        help.itemName = EditorGUILayout.TextField(help.itemName);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("項目");
        vec = EditorGUILayout.BeginScrollView(vec);
        help.itemValue = EditorGUILayout.TextArea(help.itemValue);
        EditorGUILayout.EndScrollView();
    }
}
