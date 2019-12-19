using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeforManceMoney:PeforManceDataBase
{
    public PeforManceMoney()
    {
        conditions = new long[] 
        {
            10000,
            100000,
            1000000,
            5000000000000000,
        };
        Clear = new bool[conditions.Length];
        for(int i = 0;i < Clear.Length; i++)
        {
            Clear[i] = false;
        }
        Title = new string[]
        {
            "小金持ち",
            "大金持ち",
            "大富豪",
            "5000兆株欲しい！"
        };
    }
    public override bool PeConditions()
    {
        GetNowValue();
        for(int i = nowLevel;i < conditions.Length; i++)
        {
            if(conditions[i] <= nowState)
            {
                Clear[i] = true;
            }
        }
        return false;
    }
    public override long GetNowValue()
    {
        nowState = PlayerData.GetInstance().Money;
        return nowState;
    }

}
