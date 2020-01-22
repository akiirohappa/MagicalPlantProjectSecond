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
    Vector3 pp;
    ItemList list;
    ItemType type;
    public ItemSetManager(MenuManager m):base(m)
    {
        pd = PlayerData.GetInstance();
        list = pd.Item;
        myObjct = GameObject.Find("Menu").transform.Find("PlantSetWindow").gameObject;
        selectButton = Resources.Load<GameObject>("Prefabs/SeedButton");
        field = FieldManager.GetInstance();
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
        field.SetPlantData(pos,item);
        MainManager.GetInstance.Particle.PaticleMake(MainManager.GetInstance.Particle.Particle[0], pp);
        DontDestroyManager.my.Sound.PlaySE("Dig");
        PlayerData.GetInstance().Item.ItemGet(item, -1);
        mManager.ButtonToMain();
    }
    public void PlessFtButton(Item item)
    {
        field.GetPlantData(pos).FertilizerAdd(new FertilizerData(item));
        MainManager.GetInstance.Particle.PaticleMake(MainManager.GetInstance.Particle.Particle[0], pp);
        DontDestroyManager.my.Sound.PlaySE("Dig");
        PlayerData.GetInstance().Item.ItemGet(item, -1);
        mManager.ButtonToMain();
    }
    public override void Open(Vector3Int vec)
    {
        Debug.Log(vec);
        pos = vec;
        pp = TileManager.GetInstance().CellToWorldPos(pos);
        pp.x += 0.5f;
        pp.y += 0.5f;
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
                if(type == ItemType.Fertilizer)
                {
                    FertilizerButtonMake(i);
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
        g.GetComponent<Button>().onClick.RemoveAllListeners();
        g.GetComponent<Button>().onClick.AddListener(() => PlessItemButton(i));
    }
    void FertilizerButtonMake(Item i)
    {
        GameObject g = ButtonMake(i);
        g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "成長速度:" + i.growthSpeed + "%/日";
        g.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "";
        g.GetComponent<Button>().onClick.RemoveAllListeners();
        g.GetComponent<Button>().onClick.AddListener(() => PlessFtButton(i));
    }
}
