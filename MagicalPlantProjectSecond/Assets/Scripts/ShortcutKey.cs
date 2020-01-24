using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutKey : MonoBehaviour
{
    public bool shortcutActive;
    ShortcutKeyData data;
    MenuManager m;
    public ShortcutKeyData Data
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
        }
    }
    private void Awake()
    {
        data = new ShortcutKeyData();
    }
    void Start()
    {
        shortcutActive = true;
        m = GameObject.Find("Manager").GetComponent<MenuManager>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!shortcutActive)
        {
            return;
        }
        if (Input.GetKeyDown(data.ShopKey))
        {
            m.SendMenuButton("Shop");
        }
        if (Input.GetKeyDown(data.ItemKey))
        {
            m.SendMenuButton("Item");
        }
        if (Input.GetKeyDown(data.PeforManceKey))
        {
            m.SendMenuButton("PeforMance");
        }
        if (Input.GetKeyDown(data.SaveKey))
        {
            m.SendMenuButton("Save");
        }
        if (Input.GetKeyDown(data.HelpKey))
        {
            m.SendMenuButton("Help");
        }
        if (Input.GetKeyDown(data.ConfigKey))
        {
            m.SendMenuButton("Config");
        }
        if (Input.GetKeyDown(data.ToMainKey))
        {
            m.ButtonToMain();
        }
    }

}
public class ShortcutKeyData
{
    public KeyCode ShopKey = KeyCode.S;
    public KeyCode ItemKey = KeyCode.I;
    public KeyCode PeforManceKey = KeyCode.P;
    public KeyCode SaveKey = KeyCode.V;
    public KeyCode HelpKey = KeyCode.H;
    public KeyCode ConfigKey = KeyCode.C;
    public KeyCode ToMainKey = KeyCode.B;
    public void SaveDataSet(ShortcutKeySaveData s)
    {
        ShopKey = s.ShopKey;
        ItemKey = s.ItemKey;
        PeforManceKey = s.PeforManceKey;
        SaveKey = s.SaveKey;
        HelpKey = s.HelpKey;
        ConfigKey = s.ConfigKey;
        ToMainKey = s.ToMainKey;
    }
}
[System.Serializable]
public class ShortcutKeySaveData: ShortcutKeyData
{
    public ShortcutKeySaveData(ShortcutKeyData s)
    {
        ShopKey = s.ShopKey;
        ItemKey = s.ItemKey;
        PeforManceKey = s.PeforManceKey;
        SaveKey = s.SaveKey;
        HelpKey = s.HelpKey;
        ConfigKey = s.ConfigKey;
        ToMainKey = s.ToMainKey;
    }
}