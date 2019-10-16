//-------------------------------------------------------------------
//マップのクリックしたときの処理、のベース
//-------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapEventBase
{
    //マネージャーから参照用の番号
    public int eventNum;
    //コンストラクタ君
    public MapEventBase(int num)
     {
         eventNum = num;
     }
    //上にポインターを置いた時の描写とか
    public abstract void OnHoverRun(Vector3Int pos);
    //クリックしたときの処理
    public abstract void OnClickRun();
}

