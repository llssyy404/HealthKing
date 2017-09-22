using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentRecordData
{
    //private string id;                  // 유저정보
    private string recordDate;            // 기록한 날짜
    private int recordMeter;            // 기록한 총 미터수
    private int trackCount;             // 기록 총 바퀴수
    private List<string> trackTimeDate;   // 바퀴마다 돈 시간
    private string allTrackTimeDate;      // 총 바퀴시간

    public StudentRecordData(string recordDate, int recordMeter, int trackCount, List<string> trackTimeDate, string allTrackTimeDate)
    {
        //this.id = id;
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

    //public string GetId()
    //{
    //    return id;
    //}

    //public void SetId(string id)
    //{
    //    this.id = id;
    //}

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

public struct Key
{
    int _count;
    int _sumMeter;
    public Key(int count, int sumMeter)
    {
        _count = count;
        _sumMeter = sumMeter;
    }

    public int GetCount()
    {
        return _count;
    }

    public int GetSumMeter()
    {
        return _sumMeter;
    }
}

public class MyRecordData
{
    private Dictionary<Key, List<StudentRecordData>> _dicMyRecordData;
    public void Init(List<StudentRecordData> stuRecordData)
    {
        _dicMyRecordData = new Dictionary<Key, List<StudentRecordData>>();
        for (int i = 0; i < stuRecordData.Count; ++i)
        {
            Key key = new Key(stuRecordData[i].GetTrackCount(), stuRecordData[i].GetRecordMeter());
            if (!_dicMyRecordData.ContainsKey(key))
            {
                _dicMyRecordData.Add(key, new List<StudentRecordData>());
            }

            _dicMyRecordData[key].Add(stuRecordData[i]);    // 하나씩만 저장됨 수정하기
        }
    }

    public Dictionary<Key, List<StudentRecordData>> GetDicRecordData()
    {
        return _dicMyRecordData;
    }
}

public class Record
{
    protected Int64 _recordUnique;
    protected DateTime _dateTime;

    public Int64 recordUnique
    {
        get { return _recordUnique; }
        set { _recordUnique = value; }
    }

    public DateTime dateTime
    {
        get { return _dateTime; }
        set { _dateTime = value; }
    }

    public Record() { }
}

public class CardiRecord : Record
{
    private int _totalMeter;
    private int _totalTrackCount;
    private int _totalElapsedTime;

    public int totalMeter
    {
        get { return _totalMeter; }
        set { _totalMeter = value; }
    }

    public int totalTrackCount
    {
        get { return _totalTrackCount; }
        set { _totalTrackCount = value; }
    }

    public int totalElapsedTime
    {
        get { return _totalElapsedTime; }
        set { _totalElapsedTime = value; }
    }


    public CardiRecord(Int64 recordUnique, DateTime dateTime, int totalMeter, int totalTrackCount, int totalElapsedTime)
    {
        _recordUnique = recordUnique;
        _dateTime = dateTime;
        _totalMeter = totalMeter;
        _totalTrackCount = totalTrackCount;
        _totalElapsedTime = totalElapsedTime;
    }

    public void Print()
    {
        Debug.Log(_recordUnique + " " + _dateTime + " " + _totalMeter + " " + _totalTrackCount + " " + _totalElapsedTime);
    }
}

public class AgileRecord : Record
{
    private int _meter;
    private int _elapsedTime;

    public int meter
    {
        get { return _meter; }
        set { _meter = value; }
    }

    public int elapsedTime
    {
        get { return _elapsedTime; }
        set { _elapsedTime = value; }
    }

    public AgileRecord(Int64 recordUnique, DateTime dateTime, int meter, int elapsedTime)
    {
        _recordUnique = recordUnique;
        _dateTime = dateTime;
        _meter = meter;
        _elapsedTime = elapsedTime;
    }

    public void Print()
    {
        Debug.Log(_recordUnique + " " + _dateTime + " " + _meter + " " + _elapsedTime);
    }
}

public class MuscRecord : Record
{
    private int _count;

    public int count
    {
        get { return _count; }
        set { _count = value; }
    }

    public MuscRecord(Int64 recordUnique, DateTime dateTime, int count)
    {
        _recordUnique = recordUnique;
        _dateTime = dateTime;
        _count = count;
    }

    public void Print()
    {
        Debug.Log(_recordUnique + " " + _dateTime + " " + _count);
    }
}

public class TrackRecord
{
    private Int64 _trackRecordUnique;
    private Int64 _cardiRecordUnique;
    private int _trackIndex;
    private int _elapsedTime;

    public Int64 trackRecordUnique
    {
        get { return _trackRecordUnique; }
        set { _trackRecordUnique = value; }
    }

    public Int64 cardiRecordUnique
    {
        get { return _cardiRecordUnique; }
        set { _cardiRecordUnique = value; }
    }

    public int trackIndex
    {
        get { return _trackIndex; }
        set { _trackIndex = value; }
    }

    public int elapsedTime
    {
        get { return _elapsedTime; }
        set { _elapsedTime = value; }
    }


    public TrackRecord(Int64 trackRecordUnique, Int64 cardiRecordUnique, int trackIndex, int elapsedTime)
    {
        _trackRecordUnique = trackRecordUnique;
        _cardiRecordUnique = cardiRecordUnique;
        _trackIndex = trackIndex;
        _elapsedTime = elapsedTime;
    }

    public void Print()
    {
        Debug.Log(_trackRecordUnique + " " + _cardiRecordUnique + " " + _trackIndex + " " + _elapsedTime);
    }
}
