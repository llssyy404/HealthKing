using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionInfo
{
    private List<string> _mission;
    private List<bool> _clearMission;

    public MissionInfo()
    {
        _mission = new List<string>();
        _clearMission = new List<bool>();
        for (int i = 0; i < 4; ++i)
        {
            _mission.Add("");
            _clearMission.Add(false);
        }
    }

    public bool GetClearMission(int index)
    {
        if (index >= 4)
            return false;

        return _clearMission[index];
    }

    public void SetClearMission(int index, bool isClear)
    {
        if (index >= 4)
            return;

        _clearMission[index] = isClear;
    }

    public void InitMissionInfo(List<string> listString)
    {
        _mission[0] = listString[0];
        _mission[1] = listString[1];
        _mission[2] = listString[2];
        _mission[3] = listString[3];
    }

    public List<string> GetInfo()
    {
        List<string> list = new List<string>();
        list.Add(_mission[0]);
        list.Add(_mission[1]);
        list.Add(_mission[2]);
        list.Add(_mission[3]);

        return list;
    }
}