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
    ZERO,
    ONE,
    TWO,
    THREE,
    FOUR,
    FIVE
}

//
public class PAPSScript
{
    public class PAPSScriptInfo
    {
        public int _index;
        public string _gender;
        public SCHOOL_GRADE _schoolGrade;
        public PAPS_GRADE _PAPSGrade;
        public float _min;
        public float _max;

        public PAPSScriptInfo(int index, string gender, SCHOOL_GRADE schoolGrade, PAPS_GRADE PAPSGrade, float min, float max)
        {
            _index = index;
            _gender = gender;
            _schoolGrade = schoolGrade;
            _PAPSGrade = PAPSGrade;
            _min = min;
            _max = max;
        }
    }

    private List<PAPSScriptInfo> _PAPSScriptInfo;

    public PAPSScript()
    {
        Init();
    }

    public void Init()
    {
        _PAPSScriptInfo = new List<PAPSScriptInfo>();
    }

    public void AddPAPSSriptInfo(int index, string gender, SCHOOL_GRADE schoolGrade, PAPS_GRADE PAPSGrade, float min, float max)
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

        PAPSScriptInfo PAPSScriptInfo = new PAPSScriptInfo(index, gender, schoolGrade, PAPSGrade, min, max);
        _PAPSScriptInfo.Add(PAPSScriptInfo);
    }

    private PAPS_GRADE FindPAPSGrade(PAPSScriptInfo PAPSScriptInfo, string gender, SCHOOL_GRADE schoolGrade, float value)
    {
        if (PAPSScriptInfo._gender != gender)
            return PAPS_GRADE.ZERO;

        if (PAPSScriptInfo._schoolGrade != schoolGrade)
            return PAPS_GRADE.ZERO;

        if (PAPSScriptInfo._min > value && PAPSScriptInfo._PAPSGrade != PAPS_GRADE.ONE)
            return PAPS_GRADE.ZERO;

        if (PAPSScriptInfo._max < value && PAPSScriptInfo._PAPSGrade != PAPS_GRADE.FIVE)
            return PAPS_GRADE.ZERO;

        return PAPSScriptInfo._PAPSGrade;
    }

    public PAPS_GRADE FindPAPSGrade(string gender, SCHOOL_GRADE schoolGrade, float value)
    {
        PAPS_GRADE grade = PAPS_GRADE.ZERO;
        for (int i = 0; i < _PAPSScriptInfo.Count; ++i)
        {
            grade = FindPAPSGrade(_PAPSScriptInfo[i], gender, schoolGrade, value);
            if (grade != PAPS_GRADE.ZERO)
                break;
        }

        return grade;
    }
}

// PAPSScriptManager
public enum SCRIPT_TYPE
{
    REPEAT_LONG_RUNNING,        // 왕복오래달리기
    LONG_RUNNING,               // 오래달리기
    FIFTY_M_RUNNING,            // 50m달리기
    STANDING_BROAD_JUMP,        // 제자리멀리뛰기
    SIT_UP,                     // 윗몸일으키기
    GRIP,                       // 악력
    SIT_UPPERBODY_FRONTBEND,    // 앉아서윗몸앞으로굽히기
    BMI,                        // BMI
    MAX_SCRIPT_TYPE
}

public class PAPSScriptManager
{
    private Dictionary<SCRIPT_TYPE, PAPSScript> dicPAPSScript;

    public PAPSScriptManager()
    {
        dicPAPSScript = new Dictionary<SCRIPT_TYPE, PAPSScript>();
    }
}