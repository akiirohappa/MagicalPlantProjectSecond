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
        w.maxSize = new Vector2(250f, 600f);
        w.minSize = new Vector2(250f, 600f);
    }
    HelpItem help;
    const string savePath = "Assets/Resources/Help";
    int page;
    string fileTitle;
    private void OnGUI()
    {
        
        if (help == null)
        {
            help = new HelpItem();
            help.itemValue = new List<string>();
            help.itemValue.Add("");
            fileTitle = "";
            page = 0;
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("セーブ",GUILayout.Height(30)))
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string path = EditorUtility.SaveFilePanel("セーブ", savePath,(fileTitle != "" ? fileTitle: "New Help"),"json");
            if (!string.IsNullOrEmpty(path))
            {
                string json = JsonUtility.ToJson(help);
                File.WriteAllText(path, json);
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogWarning("セーブパスが空です。");
            }
        }
        if (GUILayout.Button("ロード", GUILayout.Height(30)))
        {
            string path = EditorUtility.OpenFilePanel("ロード", "Assets/Resource/Help", "json");
            if (!string.IsNullOrEmpty(path))
            {
                string json = File.ReadAllText(path);
                fileTitle = Path.GetFileName(path);
                help = JsonUtility.FromJson<HelpItem>(json);
            }
            else
            {
                Debug.LogWarning("ロードパスが空です。");
            }
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("リセット", GUILayout.Height(20)))
        {
            help = new HelpItem();
            help.itemValue = new List<string>();
            help.itemValue.Add("");
            fileTitle = "";
            page = 0;
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("名前",GUILayout.Width(50));
        help.itemName = EditorGUILayout.TextField(help.itemName);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("＜"))
        {
            GUI.FocusControl("");
            if (page-1 != -1)
            {
                page--;
            }
            
        }
        if (GUILayout.Button("×"))
        {
            if(help.itemValue.Count != 1)
            {
                help.itemValue.Remove(help.itemValue[page]);
                if (page != 0)
                {
                    page--;
                }
            }
        }
        if (GUILayout.Button("+"))
        {
            help.itemValue.Add("");
        }
        if (GUILayout.Button("＞"))
        {
            GUI.FocusControl("");
            if(page+1 != help.itemValue.Count)
            {
                page++;
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("項目");
        EditorGUILayout.LabelField((page+1)+"/"+ help.itemValue.Count + "ページ");
        help.itemValue[page] = EditorGUILayout.TextArea(help.itemValue[page], GUILayout.Height(175));
    }
}
