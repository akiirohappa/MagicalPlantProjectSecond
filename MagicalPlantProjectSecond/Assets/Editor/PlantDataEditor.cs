using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlantData))]
public class PlantDataEditor : Editor
{
    EditorStyle style;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlantData pd = target as PlantData;
        if (style == null) style = new EditorStyle();
        if(pd.data == null)
        {
            pd.data = new Plant();
        }
        pd.data.icon = EditorGUILayout.ObjectField("アイコン",pd.data.icon,typeof(Sprite),false)as Sprite;
        pd.data.name = EditorGUILayout.TextField("名前", pd.data.name);
        pd.data.growthSpeed = EditorGUILayout.FloatField("初期成長度", pd.data.growthSpeed);
    }
}
