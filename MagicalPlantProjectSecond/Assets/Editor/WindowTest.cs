using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WindowTest : EditorWindow
{
    [MenuItem("Test/Test %e")]
    static void Open()
    {
        GetWindow<WindowTest>("テスト");
    }
    private void OnGUI()
    {
        EditorGUILayout.LabelField("aaaaaaa");
    }
}
