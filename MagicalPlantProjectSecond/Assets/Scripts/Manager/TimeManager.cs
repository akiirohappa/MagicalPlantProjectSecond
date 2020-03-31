using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SeasonData
{
    None,
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
    public float accelTime = 5f;
    public float AccelTime
    {
        set
        {
            accelTime = value;
            AccelPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "現在 " + (accelTime < 100 ? (accelTime < 10 ? "  " + accelTime.ToString():" " +accelTime.ToString()):accelTime.ToString()) + "分/秒";
        }
    }
    public float[] accelTimeSet = new float[] {5, 30, 60, 120 };
    GameObject AccelPanel;
    Image BackGround;
    Slider accelSl;
    public bool sleep = false;
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
        AccelPanel = GameObject.Find("Canvas").transform.Find("TimeAccel").gameObject;
        BackGround = GameObject.Find("Canvas").transform.Find("SleepBack").GetComponent<Image>();
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
		set
		{
			if (time == null)
			{
				time = new TimeData();
			}
			time = value;
		}
    }
    public void TimeCalc(float speed)
    {
        Time.minit += UnityEngine.Time.deltaTime * speed;
        if(Time.minit >= 60)
        {
            Time.minit -= 60;
            FieldManager.GetInstance().WaterDown();
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
                    DontDestroyManager.my.Sound.BGMChange("Main_Summer");
                    break;
                case SeasonData.Summer:
                    time.nowSeason = SeasonData.Autumn;
                    DontDestroyManager.my.Sound.BGMChange("Main_Autumn");
                    break;
                case SeasonData.Autumn:
                    time.nowSeason = SeasonData.Winter;
                    DontDestroyManager.my.Sound.BGMChange("Main_Winter");
                    break;
                case SeasonData.Winter:
                    time.nowSeason = SeasonData.Spring;
                    DontDestroyManager.my.Sound.BGMChange("Main_Spring");
                    PlayerData.GetInstance().PD.DataUnlock(PeforManceType.Time, 1);
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
        Time = newTime;
        float longf = Mathf.Floor(Time.minit) * 6;
        float shortf = Time.hour * 30;
        Time.preMinit = Mathf.Floor(Time.minit);
        Time.preHour = Time.hour;
        MainManager.GetInstance.View.TimeSet(newTime);
        //switch (Time.nowSeason)
        //{
        //    case SeasonData.Spring:
        //        DontDestroyManager.my.Sound.BGMChange("Main_Spring");
        //        break;
        //    case SeasonData.Summer:
        //        DontDestroyManager.my.Sound.BGMChange("Main_Summer");
        //        break;
        //    case SeasonData.Autumn:
        //        DontDestroyManager.my.Sound.BGMChange("Main_Autumn");
        //        break;
        //    case SeasonData.Winter:
        //        DontDestroyManager.my.Sound.BGMChange("Main_Winter");
        //        break;
        //    default:

        //        break;
        //}
    }
    public void TimeSet(TimeForSave newTime)
    {
        Time.TimeSet(newTime);
        //time.preHour = time.hour;
        //time.preMinit = time.minit;
    }
    public void AccelStart(FaData_Bed data)
    {
        AccelPanel.SetActive(true);
        accelSl = AccelPanel.transform.GetChild(0).GetComponent<Slider>();
        accelSl.minValue = accelTimeSet[0];
        accelSl.maxValue = accelTimeSet[data.nowLevel];
        accelSl.value = accelTimeSet[0];
        AccelPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "最小 " +accelTimeSet[0].ToString() + "分/秒";
        AccelPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "最大 " + accelTimeSet[data.nowLevel].ToString() + "分/秒";
        AccelTime = accelTimeSet[0];
        sleep = true;
        mm.BackFadeOut();
    }
    public void AccelEnd()
    {
        accelTime = accelTimeSet[0];
        AccelPanel.SetActive(false);
        BackGround.gameObject.SetActive(false);
        sleep = false;
    }
    public void AccelCheck()
    {
        if(sleep)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                AccelEnd();
            }
        }
    }
    public IEnumerator BackGroundFade()
    {
        BackGround.gameObject.SetActive(true);
        Color c = BackGround.color;
        c.a = 0;
        BackGround.color = c;
        for(int i = 1;i <= 40; i++)
        {
            c.a = (float)i/100;
            BackGround.color = c;
            yield return null;
        }
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
