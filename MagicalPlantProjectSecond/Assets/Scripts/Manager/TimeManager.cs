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
    MainManager mm;
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
        mm = GameObject.Find("Manager").GetComponent<MainManager>();
        time.preHour = 0;
        time.preMinit = 0;
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
            FieldManager.GetInstance().PlantGrowth();
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
        mm.View.TimeView(time,speed);
    }
    void TimeSet(TimeData newTime)
    {
        time = newTime;
        float longf = Mathf.Floor(time.minit) * 6;
        float shortf = time.hour * 30;
        time.preMinit = Mathf.Floor(time.minit);
        time.preHour = time.hour;
        mm.View.TimeSet(newTime);
    }
}
public class TimeData
{
    public int day;
    public int hour;
    public float minit;
    public int preHour;
    public float preMinit;
    public SeasonData nowSeason;
    public TimeData()
    {
        day = 1;
        hour = 7;
        minit = 0;
        nowSeason = SeasonData.Spring;
    }
}
