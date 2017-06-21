using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        public string _gender;
        public SCHOOL_GRADE _schoolGrade;
        public PAPS_GRADE _PAPSGrade;
        public float _min;
        public float _max;

        public PAPSTableInfo(int index, string gender, SCHOOL_GRADE schoolGrade, PAPS_GRADE PAPSGrade, float min, float max)
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

        PAPSTableInfo PAPSScriptInfo = new PAPSTableInfo(index, gender, schoolGrade, PAPSGrade, min, max);
        _PAPSScriptInfo.Add(PAPSScriptInfo);
    }

    private PAPS_GRADE FindPAPSGrade(PAPSTableInfo PAPSScriptInfo, string gender, SCHOOL_GRADE schoolGrade, float value)
    {
        if (PAPSScriptInfo._gender != gender)
            return PAPS_GRADE.NONE;

        if (PAPSScriptInfo._schoolGrade != schoolGrade)
            return PAPS_GRADE.NONE;

        if (PAPSScriptInfo._max >= value && PAPSScriptInfo._PAPSGrade != PAPS_GRADE.ONE)
            return PAPS_GRADE.NONE;

        if (PAPSScriptInfo._min <= value && PAPSScriptInfo._PAPSGrade != PAPS_GRADE.FIVE)
            return PAPS_GRADE.NONE;

        return PAPSScriptInfo._PAPSGrade;
    }

    public PAPS_GRADE FindPAPSGrade(string gender, SCHOOL_GRADE schoolGrade, float value)
    {
        PAPS_GRADE grade = PAPS_GRADE.NONE;
        for (int i = 0; i < _PAPSScriptInfo.Count; ++i)
        {
            grade = FindPAPSGrade(_PAPSScriptInfo[i], gender, schoolGrade, value);
            if (grade != PAPS_GRADE.NONE)
                break;
        }

        return grade;
    }
}

// PAPSScriptManager
public enum TABLE_TYPE
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

public class PAPSTableManager
{
    private Dictionary<TABLE_TYPE, PAPSTable> _dicPAPSScript;
    private string[] _tableName;

    public PAPSTableManager()
    {
        _dicPAPSScript = new Dictionary<TABLE_TYPE, PAPSTable>();
        _tableName = new string[(int)TABLE_TYPE.MAX_SCRIPT_TYPE];
        _tableName[(int)TABLE_TYPE.REPEAT_LONG_RUNNING] = "RepeatLongRunning.txt";
        _tableName[(int)TABLE_TYPE.LONG_RUNNING] = "LongRunning.txt";
        _tableName[(int)TABLE_TYPE.FIFTY_M_RUNNING] = "FiftyMRunning.txt";
        _tableName[(int)TABLE_TYPE.STANDING_BROAD_JUMP] = "StandingBroadJump.txt";
        _tableName[(int)TABLE_TYPE.SIT_UP] = "Situp.txt";
        _tableName[(int)TABLE_TYPE.GRIP] = "Grip.txt";
        _tableName[(int)TABLE_TYPE.SIT_UPPERBODY_FRONTBEND] = "SitUpperBodyFrontBend.txt";
        _tableName[(int)TABLE_TYPE.BMI] = "BMI.txt";
    }

    public void ReadTableFile()
    {
        for (int i = 0; i < _tableName.Length; ++i)
        {
            //TextAsset data = Resources.Load(_tableName[i]) as TextAsset;

        }
    }
}