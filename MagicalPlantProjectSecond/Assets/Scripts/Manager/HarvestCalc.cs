using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestCalc
{
    public Item Harvest(Plant pl)
    {
        Item it = new Item(pl);
        it.sellPrice = (int)(((float)it.defaltValue /50) * it.quality);
        return it;
    }
}
