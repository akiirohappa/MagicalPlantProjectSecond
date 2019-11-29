//------------------------------------------------------------------------------
//メニュー画面：アイテムを植えたりする編。
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemSetManager :MenuManagerBase
{
    PlayerData pd;
    GameObject buttonField;
    GameObject selectButton;
    FieldManager field;
    Vector3Int pos;
    ItemList list;
    ItemType type;
    public ItemSetManager(MenuManager m):base(m)
    {
        pd = PlayerData.GetInstance();
        list = pd.Item;
        myObjct = GameObject.Find("Menu").transform.Find("PlantSetWindow").gameObject;
        selectButton = Resources.Load<GameObject>("Prefabs/SeedButton");
    }
    public override void Submit()
    {

    }
    public override void Cancel()
    {

    }
    public override void Button(string state)
    {

    }
    public override void PlessItemButton(Item item)
    {
        field.SetPlantData(pos,new Plant(item));
        MainManager.GetInstance.Particle.PaticleMake(MainManager.GetInstance.Particle.Particle[0], new Vector3(pos.x, pos.y + 0.75f, pos.z));
        DontDestroyManager.my.Sound.PlaySE("Dig");
        PlayerData.GetInstance().Item.ItemGet(item, -1);
        mManager.ButtonToMain();
    }
    public override void Open(Vector3Int vec)
    {
        Debug.Log(vec);
        pos = vec;
        field = FieldManager.GetInstance();
        base.Open();
    }
    public void TypeSet(ItemType t)
    {
        type = t;
        
        buttonField = myObjct.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        foreach (Transform tr in buttonField.transform)
        {
            GameObject.Destroy(tr.gameObject);
        }
        foreach(Item i in list.Item)
        {
            if(i.itemType == type)
            {
                if(type == ItemType.Seed)
                {
                    SeedButtonMake(i);
                }
            }
        }
    }
    GameObject ButtonMake(Item i)
    {
        GameObject g = GameObject.Instantiate(selectButton, buttonField.transform);
        g.GetComponent<Button>().onClick.AddListener(mManager.ButtonItem);
        g.GetComponent<ItemButton>().item = i;
        g.transform.GetChild(0).GetComponent<Image>().sprite = i.icon;
        g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = i.itemName;
        g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = i.itemNum + "個";
        return g;
    }
    void SeedButtonMake(Item i)
    {
        GameObject g = ButtonMake(i);
        g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "成長速度:"+i.growthSpeed + "%/日";
        g.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "";
    }
}
