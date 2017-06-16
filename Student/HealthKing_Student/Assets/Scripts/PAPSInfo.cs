using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAPSInfo {
    private CardiovascularEndurance _cardiovascularEndurance;   // 심폐지구력
    private Agility                 _agility;                   // 순발력
    private MuscularEndurance       _muscularEndurance;         // 근지구력
    private Flexibility             _flexibility;               // 유연성
    private BMI                     _BMI;                       // BMI
}

// 심폐지구력
public class CardiovascularEndurance
{
    private int _repeatLongRunningCount;
    private int _longRunningMinute;
    private int _longRunningSecond;
    //
    private int _grade;

    public bool InitInfo(int repeatLongRunningCount, int longRunningMinute, int longRunningSecond)
    {
        _repeatLongRunningCount = repeatLongRunningCount;
        _longRunningMinute = longRunningMinute;
        _longRunningSecond = longRunningSecond;

        return true;
    }
}

// 순발력
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

// 근지구력
public class MuscularEndurance {
    private int _sitUpCount;
    private int _gripRightKG;
    private int _gripLeftKG;
    //
    private int _grade;

    public bool SetInfo(int sitUpCount, int gripRightKG, int gripLeftKG)
    {
        _sitUpCount = sitUpCount;
        _gripRightKG = gripRightKG;
        _gripLeftKG = gripLeftKG;

        return true;
    }
}

// 유연성
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