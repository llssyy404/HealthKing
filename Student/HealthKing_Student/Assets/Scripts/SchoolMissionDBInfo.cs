using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolMissionDBInfo
{
    private long _missionUnique;
    private string _missionDesc;

    public long missionUnique
    {
        get { return _missionUnique; }
        private set { _missionUnique = value; }
    }

    public string missionDesc
    {
        get { return _missionDesc; }
        private set { _missionDesc = value; }
    }

    public SchoolMissionDBInfo(long missionUnique, string missionDesc)
    {
        _missionUnique = missionUnique;
        _missionDesc = missionDesc;
    }
}