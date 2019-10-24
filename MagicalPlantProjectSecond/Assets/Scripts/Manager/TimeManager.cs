using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SeasonData
{
    Spring,
    Summer,
    Autumn,
    Winter,
}
public class TimeManager
{
    private static TimeManager timeM;
    private TimeData time;
    TextMeshProUGUI seasonText;
    TextMeshProUGUI dayText;
    TextMeshProUGUI timeText;
    GameObject clockShort;
    GameObject clockLong;
    public int preHour;
    public float preMinit;
    public static TimeManager GetInstance()
    {
        if(timeM == null)
        {
            timeM = new TimeManager();
        }
        return timeM;
    }
    private TimeManager()
    {
        time = new TimeData();
        seasonText = GameObject.Find("Season").GetComponent<TextMeshProUGUI>();
        dayText = GameObject.Find("Day").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        clockLong = GameObject.Find("Long");
        clockShort = GameObject.Find("Short");
        preHour = 0;
        preMinit = 0;
        TimeSet(time);
    }
    public TimeData GetTime()
    {
        if(time == null)
        {
            time = new TimeData();
        }
        return time;
    }
    public void TimeCalc(float speed)
    {
        time.minit += Time.deltaTime * speed;
        if(time.minit >= 60)
        {
            time.minit -= 60;
            time.hour++;
        }
        if(time.hour == 24)
        {
            time.hour = 0;
            time.day++;
        }
        if(time.day == 31)
        {
            time.day = 1;
            switch (time.nowSeason)
            {
                case SeasonData.Spring:
                    time.nowSeason = SeasonData.Summer;
                    break;
                case SeasonData.Summer:
                    time.nowSeason = SeasonData.Autumn;
                    break;
                case SeasonData.Autumn:
                    time.nowSeason = SeasonData.Winter;
                    break;
                case SeasonData.Winter:
                    time.nowSeason = SeasonData.Spring;
                    break;
                default:
                    time.nowSeason = SeasonData.Spring;
                    break;
            }
        }
        TimeView(speed);
    }
    void TimeView(float speed)
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
        timeText.text = time.hour + "：" + (time.minit < 10 ? "0" : "")+Mathf.Floor(time.minit);
        if(Mathf.Floor(time.minit) != preMinit)
        {
            preMinit = Mathf.Floor(time.minit);
            float longf = 6;
            clockLong.transform.Rotate(0, 0, -longf);
        }
        if (preHour != time.hour)
        {
            preHour = time.hour;
            float shortf = 30;
            clockShort.transform.Rotate(0, 0, -shortf);
        }
    }
    void TimeSet(TimeData newTime)
    {
        time = newTime;
        float longf = Mathf.Floor(time.minit) *6; 
        clockLong.transform.Rotate(0, 0, -longf);
        float shortf = time.hour * 30;
        clockShort.transform.Rotate(0, 0, -shortf);
        preMinit = Mathf.Floor(time.minit);
        preHour = time.hour;
    }
}
public class TimeData
{
    public int day;
    public int hour;
    public float minit;
    public SeasonData nowSeason;
    public TimeData()
    {
        day = 1;
        hour = 7;
        minit = 0;
        nowSeason = SeasonData.Spring;
    }
}
