using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    EditorStyle style;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ItemData id = target as ItemData;
        if (style == null) style = new EditorStyle();
        if(id.data == null)
        {
            id.data = new Item("空");
        }
        id.data.icon = EditorGUILayout.ObjectField("アイコン",id.data.icon,typeof(Sprite),false)as Sprite;
        id.data.itemName = EditorGUILayout.TextField("名前", id.data.itemName);
        id.data.itemType = (ItemType)EditorGUILayout.EnumFlagsField("アイテムの種類", id.data.itemType);
        id.data.defaltValue = EditorGUILayout.IntField("値段", id.data.defaltValue);
    }
}
