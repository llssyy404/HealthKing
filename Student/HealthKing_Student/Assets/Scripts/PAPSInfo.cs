using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAPSInfo {
    private CardiovascularEndurance _cardiovascularEndurance;
    private Agility                 _agility;
    private MuscularEndurance       _muscularEndurance;
    private Flexibility             _flexibility;
    private BMI                     _BMI;
}

//
public class CardiovascularEndurance
{
    private int _longRunningCount;
    private int _longRunningMinute;
    private int _longRunningSecond;
    //
    private int _grade;

    public bool InitInfo(int longRunningCount, int longRunningMinute, int longRunningSecond)
    {
        _longRunningCount = longRunningCount;
        _longRunningMinute = longRunningMinute;
        _longRunningSecond = longRunningSecond;

        return true;
    }
}

public class Agility
{
    private int _fiftyMRunningSecond;
    private int _standingBroadJumpCm;
    //
    private int _grade;

    public bool InitInfo(int fiftyMRunningSecond, int standingBroadJumpCm)
    {
        _fiftyMRunningSecond = fiftyMRunningSecond;
        _standingBroadJumpCm = standingBroadJumpCm;

        return true;
    }
}

public class MuscularEndurance {
    private int _pushUpCount;
    private int _sitUpCount;
    private int _gripRightKG;
    private int _gripLeftKG;
    //
    private int _grade;

    public bool SetInfo(int pushUpCount, int sitUpCount, int gripRightKG, int gripLeftKG)
    {
        _pushUpCount = pushUpCount;
        _sitUpCount = sitUpCount;
        _gripRightKG = gripRightKG;
        _gripLeftKG = gripLeftKG;

        return true;
    }
}

public class Flexibility
{
    private int _sitUpperBodyFrontBendCm;
    //
    private int _grade;

    public bool SetInfo(int sitUpperBodyFrontBendCm)
    {
        _sitUpperBodyFrontBendCm = sitUpperBodyFrontBendCm;

        return true;
    }
}

public class BMI
{
    private int _height;
    private int _weight;
    //
    private int _grade;

    public bool SetInfo(int height, int weight)
    {
        _height = height;
        _weight = weight;

        return true;
    }
}

//
enum SCHOOL_GRADE
{
    ELE_FOUR,
    ELE_FIVE,
    ELE_SIX
}

enum GRADE_NUM
{
    ONE,
    TWO,
    THREE,
    FOUR,
    FIVE
}

//
public class PAPSScript
{
    public struct MinMax
    {
        public float _min;
        public float _max;
    }

    public class PAPSScriptInfo
    {
        private int _index;
        private MinMax _minMax;
        private GRADE_NUM _gradeNum = GRADE_NUM.ONE;

        public PAPSScriptInfo(int index, float min, float max, int gradeNum)
        {
            _index = index;
            _minMax._min = min;
            _minMax._max = max;
            _gradeNum = (GRADE_NUM)gradeNum;
        }
    }

    private List<PAPSScriptInfo> _PAPSScriptInfo;

    public void AddPAPSSriptInfo(int index, float min, float max, int gradeNum)
    {
        PAPSScriptInfo PAPSScriptInfo = new PAPSScriptInfo(index, min, max, gradeNum);
        _PAPSScriptInfo.Add(PAPSScriptInfo);
    }
}