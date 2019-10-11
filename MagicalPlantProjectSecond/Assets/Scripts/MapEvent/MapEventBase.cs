//-------------------------------------------------------------------
//マップのクリックしたときの処理、のベース
//-------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MapEvent
{
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
        public abstract void OnHoverRun();
        //クリックしたときの処理
        public abstract void OnClickRun();
    }
}

