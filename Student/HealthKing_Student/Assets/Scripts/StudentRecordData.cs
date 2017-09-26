using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record
{
    protected Int64 _recordUnique;
    protected DateTime _dateTime;

    public Int64 recordUnique
    {
        get { return _recordUnique; }
        private set { _recordUnique = value; }
    }

    public DateTime dateTime
    {
        get { return _dateTime; }
        private set { _dateTime = value; }
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
        private set { _totalMeter = value; }
    }

    public int totalTrackCount
    {
        get { return _totalTrackCount; }
        private set { _totalTrackCount = value; }
    }

    public int totalElapsedTime
    {
        get { return _totalElapsedTime; }
        private set { _totalElapsedTime = value; }
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
        private set { _meter = value; }
    }

    public int elapsedTime
    {
        get { return _elapsedTime; }
        private set { _elapsedTime = value; }
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
        private set { _count = value; }
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
        private set { _trackRecordUnique = value; }
    }

    public Int64 cardiRecordUnique
    {
        get { return _cardiRecordUnique; }
        private set { _cardiRecordUnique = value; }
    }

    public int trackIndex
    {
        get { return _trackIndex; }
        private set { _trackIndex = value; }
    }

    public int elapsedTime
    {
        get { return _elapsedTime; }
        private set { _elapsedTime = value; }
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

public class SchoolMission
{
    private Int64 _missionUnique;
    private string _missionDesc;

    public Int64 missionUnique
    {
        get { return _missionUnique; }
        private set { _missionUnique = value; }
    }

    public string missionDesc
    {
        get { return _missionDesc; }
        private set { _missionDesc = value; }
    }

    public SchoolMission(Int64 missionUnique, string missionDesc)
    {
        _missionUnique = missionUnique;
        _missionDesc = missionDesc;
    }
}