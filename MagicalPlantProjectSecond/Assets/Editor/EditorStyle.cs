using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
[CreateAssetMenu(fileName ="EditorStyle",menuName = "EditorStyle")]
public class EditorStyle:ScriptableObject
{ 
    [SerializeField]
    public GUISkin skin;
    public Dictionary<string, GUIStyle> Styles;
    public EditorStyle()
    {

    }
    public void Setup()
    {
        Styles = new Dictionary<string, GUIStyle>();
        Styles["label"] = new GUIStyle()
        {
            fontSize = 20
        };
        Styles["Button"] = new GUIStyle(skin.button)
        {
            margin = new RectOffset(0, 0, 0, 0),
            alignment = TextAnchor.MiddleLeft,
            hover = new GUIStyleState(),
            normal = new GUIStyleState(),
            active = new GUIStyleState(),
        };
        Styles["Child"] = new GUIStyle(skin.box)
        {
            margin = new RectOffset(40, 0, 0, 0),
        };
        Styles["Center"] = new GUIStyle()
        {
            alignment = TextAnchor.MiddleCenter
        };
    }
}
