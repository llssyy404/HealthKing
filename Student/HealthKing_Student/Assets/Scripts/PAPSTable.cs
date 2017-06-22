using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public enum SCHOOL_GRADE
{
    ELE_FOUR = 4,   // 초4
    ELE_FIVE,
    ELE_SIX
}

public enum PAPS_GRADE
{
    NONE,
    ONE,
    TWO,
    THREE,
    FOUR,
    FIVE
}

//
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

    private List<PAPSTableInfo> _PAPSScriptInfo;

    public PAPSTable()
    {
        Init();
    }

    public void Init()
    {
        _PAPSScriptInfo = new List<PAPSTableInfo>();
    }

    public void AddPAPSSriptInfo(int index, int gender, SCHOOL_GRADE schoolGrade, PAPS_GRADE PAPSGrade, float min, float max)
    {
        if(schoolGrade < SCHOOL_GRADE.ELE_FOUR || schoolGrade > SCHOOL_GRADE.ELE_SIX)
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
        _PAPSScriptInfo.Add(PAPSScriptInfo);
    }

    private PAPS_GRADE FindPAPSGrade(PAPSTableInfo PAPSScriptInfo, int gender, SCHOOL_GRADE schoolGrade, float value)
    {
        if (PAPSScriptInfo._gender != gender)
            return PAPS_GRADE.NONE;

        if (PAPSScriptInfo._schoolGrade != schoolGrade)
            return PAPS_GRADE.NONE;

        Debug.Log(PAPSScriptInfo._min + ", " + value + ", "+ PAPSScriptInfo._max);
        if (PAPSScriptInfo._min <= value && value <= PAPSScriptInfo._max)
            return PAPSScriptInfo._PAPSGrade;

        return PAPS_GRADE.NONE;
    }

    public PAPS_GRADE FindPAPSGrade(int gender, SCHOOL_GRADE schoolGrade, float value)
    {
        PAPS_GRADE grade = PAPS_GRADE.NONE;
        Debug.Log(_PAPSScriptInfo.Count);
        for (int i = 0; i < _PAPSScriptInfo.Count; ++i)
        {
            grade = FindPAPSGrade(_PAPSScriptInfo[i], gender, schoolGrade, value);
            if (grade != PAPS_GRADE.NONE)
                break;
        }

        return grade;
    }
}