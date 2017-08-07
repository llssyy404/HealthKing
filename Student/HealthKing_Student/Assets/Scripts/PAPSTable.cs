using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public enum SCHOOL_GRADE
{
    ELE_FOUR = 4,   // 초4
    ELE_FIVE,
    ELE_SIX,
    MID_ONE,
    MID_TWO,
    MID_THREE,
    HIGH_ONE,
    HIGH_TWO,
    HIGH_THREE,
    MAX_SCHOOL_GRADE
}

public enum PAPS_GRADE
{
    ONE = 1,
    TWO,
    THREE,
    FOUR,
    FIVE,
    NONE
}

public class PAPSTable
{
    public class PAPSTableInfo
    {
        public int _index;
        public int _gender;
        public SCHOOL_GRADE _schoolGrade;
        public PAPS_GRADE _PAPSGrade;
        public float _min;
        public float _max;

        public PAPSTableInfo(int index, int gender, SCHOOL_GRADE schoolGrade, PAPS_GRADE PAPSGrade, float min, float max)
        {
            _index = index;
            _gender = gender;
            _schoolGrade = schoolGrade;
            _PAPSGrade = PAPSGrade;
            _min = min;
            _max = max;
        }
    }

    private List<PAPSTableInfo> _listPAPSScriptInfo;
    private float _min = 0;
    private float _max = 0;

    public PAPSTable()
    {
        Init();
    }

    public void Init()
    {
        _listPAPSScriptInfo = new List<PAPSTableInfo>();
    }

    public void AddPAPSSriptInfo(int index, int gender, SCHOOL_GRADE schoolGrade, PAPS_GRADE PAPSGrade, float min, float max)
    {
        if(schoolGrade < SCHOOL_GRADE.ELE_FOUR || schoolGrade >= SCHOOL_GRADE.MAX_SCHOOL_GRADE)
        {
            Debug.Log("학년값을 벗어남");
            return;
        }

        if (PAPSGrade < PAPS_GRADE.ONE || PAPSGrade > PAPS_GRADE.FIVE)
        {
            Debug.Log("PAPS등급값을 벗어남");
            return;
        }

        PAPSTableInfo PAPSScriptInfo = new PAPSTableInfo(index, gender, schoolGrade, PAPSGrade, min, max);
        _listPAPSScriptInfo.Add(PAPSScriptInfo);
    }

    private PAPS_GRADE FindPAPSGrade(PAPSTableInfo PAPSScriptInfo, int gender, SCHOOL_GRADE schoolGrade, float value)
    {
        if (PAPSScriptInfo._gender != gender)
            return PAPS_GRADE.NONE;

        if (PAPSScriptInfo._schoolGrade != schoolGrade)
            return PAPS_GRADE.NONE;

        if (PAPSScriptInfo._PAPSGrade == PAPS_GRADE.ONE)
            _min = PAPSScriptInfo._min;

        if (PAPSScriptInfo._PAPSGrade == PAPS_GRADE.FIVE)
            _max = PAPSScriptInfo._max;

        if (PAPSScriptInfo._min <= value && value <= PAPSScriptInfo._max)
            return PAPSScriptInfo._PAPSGrade;

        return PAPS_GRADE.NONE;
    }

    public PAPS_GRADE FindPAPSGrade(int gender, SCHOOL_GRADE schoolGrade, float value)
    {
        _min = 0; _max = 0;
        PAPS_GRADE grade = PAPS_GRADE.NONE;
        for (int i = 0; i < _listPAPSScriptInfo.Count; ++i)
        {
            grade = FindPAPSGrade(_listPAPSScriptInfo[i], gender, schoolGrade, value);
            if (grade != PAPS_GRADE.NONE)
                return grade;
        }

        if (_min == 0 && _max == 0)
            return grade;

        if (_min < _max)
        {
            grade = _min > value ? PAPS_GRADE.ONE : PAPS_GRADE.FIVE;
        }
        else
        {
            grade = _min < value ? PAPS_GRADE.ONE : PAPS_GRADE.FIVE;
        }

        return grade;
    }
}