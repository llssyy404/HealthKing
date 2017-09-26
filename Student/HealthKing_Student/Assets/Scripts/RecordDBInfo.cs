using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordDBInfo
{
    protected long _recordUnique;
    protected DateTime _dateTime;

    public long recordUnique
    {
        get { return _recordUnique; }
        private set { _recordUnique = value; }
    }

    public DateTime dateTime
    {
        get { return _dateTime; }
        private set { _dateTime = value; }
    }

    public RecordDBInfo() { }
}

public class CardiRecordDBInfo : RecordDBInfo
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

    public CardiRecordDBInfo(long recordUnique, DateTime dateTime, int totalMeter, int totalTrackCount, int totalElapsedTime)
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

public class AgileRecordDBInfo : RecordDBInfo
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

    public AgileRecordDBInfo(long recordUnique, DateTime dateTime, int meter, int elapsedTime)
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

public class MuscRecordDBInfo : RecordDBInfo
{
    private int _count;

    public int count
    {
        get { return _count; }
        private set { _count = value; }
    }

    public MuscRecordDBInfo(long recordUnique, DateTime dateTime, int count)
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

public class TrackRecordDBInfo
{
    private long _trackRecordUnique;
    private long _cardiRecordUnique;
    private int _trackIndex;
    private int _elapsedTime;

    public long trackRecordUnique
    {
        get { return _trackRecordUnique; }
        private set { _trackRecordUnique = value; }
    }

    public long cardiRecordUnique
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

    public TrackRecordDBInfo(long trackRecordUnique, long cardiRecordUnique, int trackIndex, int elapsedTime)
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
