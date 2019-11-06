using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeaderDataView
{
    TextMeshProUGUI seasonText;
    TextMeshProUGUI dayText;
    TextMeshProUGUI timeText;
    GameObject clockShort;
    GameObject clockLong;
    TextMeshProUGUI MoneyText;
    public HeaderDataView()
    {
        seasonText = GameObject.Find("Season").GetComponent<TextMeshProUGUI>();
        dayText = GameObject.Find("Day").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        clockLong = GameObject.Find("Long");
        clockShort = GameObject.Find("Short");
        MoneyText = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
    }
    public void TimeView(TimeData time,float speed)
    {
        string text;
        switch (time.nowSeason)
        {
            case SeasonData.Spring:
                text = "春";
                break;
            case SeasonData.Summer:
                text = "夏";
                break;
            case SeasonData.Autumn:
                text = "秋";
                break;
            case SeasonData.Winter:
                text = "冬";
                break;
            default:
                text = "空";
                break;
        }
        seasonText.text = text;
        dayText.text = time.day + "日";
        timeText.text = time.hour + "：" + (time.minit < 10 ? "0" : "") + Mathf.Floor(time.minit);
        if (Mathf.Floor(time.minit) != time.preMinit)
        {
            time.preMinit = Mathf.Floor(time.minit);
            float longf = 6;
            clockLong.transform.Rotate(0, 0, -longf);
        }
        if (time.preHour != time.hour)
        {
            time.preHour = time.hour;
            float shortf = 30;
            clockShort.transform.Rotate(0, 0, -shortf);
        }
    }
    public void TimeSet(TimeData time)
    {
        float longf = Mathf.Floor(time.minit) * 6;
        clockLong.transform.Rotate(0, 0, -longf);
        float shortf = time.hour * 30;
        clockShort.transform.Rotate(0, 0, -shortf);
    }
    public void MoneySet(long money)
    {
        MoneyText.text = 
            (money  / 1000000000000 != 0 ? money % 10000000000000000 / 1000000000000 + "兆" : "") + 
            (money % 1000000000000 / 100000000 != 0 ? money % 1000000000000 / 100000000 + "億" : "") + 
            (money % 100000000 / 10000 != 0 ? money% 100000000 / 10000 + "万":"")+ 
            (money % 10000 != 0 ? (money %10000).ToString(): "") + "株";
    }
}
