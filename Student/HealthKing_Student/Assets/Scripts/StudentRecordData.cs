using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentRecordData
{
    private string id;                  // 유저정보
    private string recordDate;            // 기록한 날짜
    private int recordMeter;            // 기록한 총 미터수
    private int trackCount;             // 기록 총 바퀴수
    private List<string> trackTimeDate;   // 바퀴마다 돈 시간
    private string allTrackTimeDate;      // 총 바퀴시간

    public StudentRecordData(string id, string recordDate, int recordMeter, int trackCount, List<string> trackTimeDate, string allTrackTimeDate)
    {
        this.id = id;
        this.recordDate = recordDate;
        this.recordMeter = recordMeter;
        this.trackCount = trackCount;
        this.trackTimeDate = new List<string>();
        for (int i = 0; i < trackTimeDate.Count; ++i)
        {
            this.trackTimeDate.Add(trackTimeDate[i]);
        }
        this.allTrackTimeDate = allTrackTimeDate;
    }
    public string GetId()
    {
        return id;
    }

    public void SetId(string id)
    {
        this.id = id;
    }

    public string GetRecordDate()
    {
        return recordDate;
    }

    public void SetRecordDate(string recordDate)
    {
        this.recordDate = recordDate;
    }

    public int GetRecordMeter()
    {
        return recordMeter;
    }

    public void SetRecordMeter(int recordMeter)
    {
        this.recordMeter = recordMeter;
    }

    public int GetTrackCount()
    {
        return trackCount;
    }

    public void SetTrackCount(int trackCount)
    {
        this.trackCount = trackCount;
    }

    public List<string> GetTrackTimeDate()
    {
        return trackTimeDate;
    }

    public void SetTrackTimeDate(List<string> trackTimeDate)
    {
        this.trackTimeDate = trackTimeDate;
    }

    public string GetAllTrackTimeDate()
    {
        return allTrackTimeDate;
    }

    public void SetAllTrackTimeDate(string allTrackTimeDate)
    {
        this.allTrackTimeDate = allTrackTimeDate;
    }
}

