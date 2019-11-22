﻿using System.Collections;
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

    }
    public void Start()
    {
        mm = MainManager.GetInstance;
        //TimeSet(time);
    }
    public TimeData Time
    {
        get
        {
            if (time == null)
            {
                time = new TimeData();
            }
            return time;
        }

    }
    public void TimeCalc(float speed)
    {
        Time.minit += UnityEngine.Time.deltaTime * speed;
        if(Time.minit >= 60)
        {
            Time.minit -= 60;
            Time.hour++;
        }
        if(Time.hour == 24)
        {
            time.hour = 0;
            time.day++;
            FieldManager.GetInstance().PlantGrowth();
        }
        if(Time.day == 31)
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
        mm.View.TimeView(Time,speed);
    }
    public void TimeSet(TimeData newTime)
    {
        time = newTime;
        float longf = Mathf.Floor(time.minit) * 6;
        float shortf = time.hour * 30;
        time.preMinit = Mathf.Floor(time.minit);
        time.preHour = time.hour;
        MainManager.GetInstance.View.TimeSet(newTime);
    }
    public void TimeSet(TimeForSave newTime)
    {
        Time.TimeSet(newTime);
        //time.preHour = time.hour;
        //time.preMinit = time.minit;
        
    }
}
public class TimeData
{
    public int year;
    public int day;
    public int hour;
    public float minit;
    public int preHour;
    public float preMinit;
    public SeasonData nowSeason;
    public TimeData()
    {
        year = 1;
        day = 1;
        hour = 7;
        minit = 0;
        preHour = hour;
        preMinit = minit;
        nowSeason = SeasonData.Spring;
    }
    public void TimeSet(TimeForSave t)
    {
        year = t.year;
        day = t.day;
        hour = t.hour;
        minit = t.minit;
        preHour = t.preHour;
        preMinit = t.preMinit;
        nowSeason = t.nowSeason;
    }
    public string SeasonToStr
    {
        get
        {
            switch (nowSeason)
            {
                case SeasonData.Spring:
                    return"春";
                case SeasonData.Summer:
                    return "夏";
                case SeasonData.Autumn:
                    return "秋";
                case SeasonData.Winter:
                    return "冬";
                default:
                    return "空";
            }
        }
    }
}
[System.Serializable]
public class TimeForSave
{
    public int year;
    public int day;
    public int hour;
    public float minit;
    public int preHour;
    public float preMinit;
    public SeasonData nowSeason;
    public string SeasonToStr
    {
        get
        {
            switch (nowSeason)
            {
                case SeasonData.Spring:
                    return "春";
                case SeasonData.Summer:
                    return "夏";
                case SeasonData.Autumn:
                    return "秋";
                case SeasonData.Winter:
                    return "冬";
                default:
                    return "空";
            }
        }
    }
    public TimeForSave(TimeData t)
    {
        year = t.year;
        day = t.day;
        hour = t.hour;
        minit = t.minit;
        preHour = t.preHour;
        preMinit = t.preMinit;
        nowSeason = t.nowSeason;
    }
}
