using UnityEngine;
using UnityEditor;

public class EditorStyle
{ 
    public GUISkin skin;
    public GUIStyle top;
    public GUIStyle text;
    public GUIStyle text2;
    public GUIStyle hbox;
    public GUIStyle toggle;
    public EditorStyle()
    {
        skin = Resources.Load<GUISkin>("GUISkin");
        top = new GUIStyle(EditorStyles.label)
        {
            font = skin.font,
            fontSize = 20,
        };
        text = new GUIStyle(EditorStyles.label)
        {
            fontSize = 15,
        };
        hbox = new GUIStyle(EditorStyles.helpBox);
        toggle = new GUIStyle(skin.toggle);
        text2 = new GUIStyle(skin.label)
        {

        };
    }
}
