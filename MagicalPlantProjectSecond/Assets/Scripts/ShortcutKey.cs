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
    public KeyCode GetCode(int i)
    {
        switch (i)
        {
            case 0:
                return data.ShopKey;
            case 1:
                return data.ItemKey;
            case 2:
                return data.PeforManceKey;
            case 3:
                return data.SaveKey;
            case 4:
                return data.HelpKey;
            case 5:
                return data.ConfigKey;
            case 6:
                return data.ToMainKey;
            default:
                return KeyCode.None;
        }
    }
    public void SetCode(KeyCode code,int i)
    {
        switch (i)
        {
            case 0:
                data.ShopKey = code;
                break;
            case 1:
                data.ItemKey = code;
                break;
            case 2:
                data.PeforManceKey = code;
                break;
            case 3:
                data.SaveKey = code;
                break;
            case 4:
                data.HelpKey = code;
                break;
            case 5:
                data.ConfigKey = code;
                break;
            case 6:
                data.ToMainKey = code;
                break;
            default:
                break;
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
            m.State = MenuState.None;
            m.SendMenuButton("Shop");
        }
        if (Input.GetKeyDown(data.ItemKey))
        {
            m.State = MenuState.None;
            m.SendMenuButton("Item");
        }
        if (Input.GetKeyDown(data.PeforManceKey))
        {
            m.State = MenuState.None;
            m.SendMenuButton("Peformance");
        }
        if (Input.GetKeyDown(data.SaveKey))
        {
            m.State = MenuState.None;
            m.SendMenuButton("Save");
        }
        if (Input.GetKeyDown(data.HelpKey))
        {
            m.State = MenuState.None;
            m.SendMenuButton("Help");
        }
        if (Input.GetKeyDown(data.ConfigKey))
        {
            m.State = MenuState.None;
            m.SendMenuButton("Config");
        }
        if (Input.GetKeyDown(data.ToMainKey))
        {
            m.State = MenuState.None;
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