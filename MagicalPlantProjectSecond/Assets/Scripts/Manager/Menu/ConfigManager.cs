using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class ConfigManager : MenuManagerBase
{
    public enum ConfigType
    {
        Sound,
        Key,
    }
    KeyCode Code
    {
        get
        {
            foreach(KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    return key;
                }
            }
            return KeyCode.None;
        }
    }
    ConfigType nowType;
    ConfigType Type
    {
        set
        {
            nowType = value;
            ConfigObj[ConfigType.Sound].SetActive(false);
            ConfigObj[ConfigType.Key].SetActive(false);
            ConfigObj[value].SetActive(true);
        }
    }
    Dictionary<ConfigType, GameObject> ConfigObj;
    ConfigData config;
    SoundManager sound;
    GameObject[] soundSliders;
    int keySetNum;
    KeyCode code;
    
    public ConfigManager(MenuManager m) : base(m)
    {
        myObjct = GameObject.Find("Menu").transform.Find("Config").gameObject;
        ConfigObj = new Dictionary<ConfigType, GameObject>();
        ConfigObj[ConfigType.Sound] = myObjct.transform.GetChild(2).gameObject;
        ConfigObj[ConfigType.Key] = myObjct.transform.GetChild(3).gameObject;
        soundSliders = new GameObject[myObjct.transform.GetChild(2).childCount];
        for(int i = 0;i <  myObjct.transform.GetChild(2).childCount; i++)
        {
            soundSliders[i] = myObjct.transform.GetChild(2).GetChild(i).gameObject;
        }
        myObjct.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() => ConfigChange(ConfigType.Sound));
        myObjct.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() => ConfigChange(ConfigType.Key));
        Type = ConfigType.Sound;
        sound = DontDestroyManager.my.Sound;
        keySetNum = -1;
        KeyConfigText();
    }
    public void UpDate()
    {
        if(keySetNum != -1)
        {
            code = Code;
            if(Code != KeyCode.None)
            {
                KeyCodeSet(keySetNum,code);
            }
        }
        else
        {
            MainManager.GetInstance.Key.shortcutActive = true;
        }

    }
    public override void Open()
    {
        base.Open();
        ConfigData c = new ConfigData();
        c.CoufigLoad();
        config = c;
    }
    public override void Submit()
    {

    }
    public override void Cancel()
    {

    }
    public override void Button(string state)
    {
        base.Button(state);
    }
    public override void SliderChange(float f)
    {
        switch (nowType)
        {
            case ConfigType.Sound:
                int num = -1;
                for(int i = 0;i < soundSliders.Length; i++)
                {
                    if(mManager.Cullent == soundSliders[i])
                    {
                        num = i;
                    }
                }
                switch (num)
                {
                    case 0:
                        sound.SetVolume(SoundType.Master, f);
                        break;
                    case 1:
                        sound.SetVolume(SoundType.BGM, f);
                        break;
                    case 2:
                        sound.SetVolume(SoundType.SE, f);
                        break;
                    default:
                        break;
                }
                break;
        }
    }
    public void ConfigChange(ConfigType c)
    {
        Type = c;
    }
    void KeyConfigText()
    {
        Transform t = ConfigObj[ConfigType.Key].transform;
        t.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ショップ";
        t.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "アイテム";
        t.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "実績";
        t.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "セーブ";
        t.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "ヘルプ";
        t.GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = "コンフィグ";
        t.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().text = "メニューを閉じる";
        t.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() => KeySetStart(0));
        t.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => KeySetStart(1));
        t.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => KeySetStart(2));
        t.GetChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(() => KeySetStart(3));
        t.GetChild(4).GetChild(1).GetComponent<Button>().onClick.AddListener(() => KeySetStart(4));
        t.GetChild(5).GetChild(1).GetComponent<Button>().onClick.AddListener(() => KeySetStart(5));
        t.GetChild(6).GetChild(1).GetComponent<Button>().onClick.AddListener(() => KeySetStart(6));
        KeyCodeButtonTxSet();
    }
    void KeyCodeButtonTxSet()
    {
        Transform t = ConfigObj[ConfigType.Key].transform;
        t.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = MainManager.GetInstance.Key.Data.ShopKey.ToString();
        t.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = MainManager.GetInstance.Key.Data.ItemKey.ToString();
        t.GetChild(2).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = MainManager.GetInstance.Key.Data.PeforManceKey.ToString();
        t.GetChild(3).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = MainManager.GetInstance.Key.Data.SaveKey.ToString();
        t.GetChild(4).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = MainManager.GetInstance.Key.Data.HelpKey.ToString();
        t.GetChild(5).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = MainManager.GetInstance.Key.Data.ConfigKey.ToString();
        t.GetChild(6).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = MainManager.GetInstance.Key.Data.ToMainKey.ToString();
    }
    public void KeySetStart(int code)
    {
        MainManager.GetInstance.Key.shortcutActive = false;
        keySetNum = code;
    }
    public void KeyCodeSet(int code,KeyCode c)
    {
        mManager.eventS.SetSelectedGameObject(null);
        switch (code)
        {
            case 0:
                MainManager.GetInstance.Key.Data.ShopKey = c;
                break;
            case 1:
                MainManager.GetInstance.Key.Data.ItemKey = c;
                break;
            case 2:
                MainManager.GetInstance.Key.Data.PeforManceKey = c;
                break;
            case 3:
                MainManager.GetInstance.Key.Data.SaveKey = c;
                break;
            case 4:
                MainManager.GetInstance.Key.Data.HelpKey = c;
                break;
            case 5:
                MainManager.GetInstance.Key.Data.ConfigKey = c;
                break;
            case 6:
                MainManager.GetInstance.Key.Data.ToMainKey = c;
                break;
        }
        keySetNum = -1;
        KeyCodeButtonTxSet();

    }
}
