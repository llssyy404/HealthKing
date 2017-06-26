using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAPSInfo {
    private CardiovascularEnduranceInfo _cardiovascularEndurance;   // 심폐지구력
    private AgilityInfo _agility;                   // 순발력
    private MuscularEnduranceInfo _muscularEndurance;         // 근지구력
    private FlexibilityInfo _flexibility;               // 유연성
    private BMIInfo _BMI;                       // BMI
}

// 심폐지구력
public class CardiovascularEnduranceInfo
{
    private int _repeatLongRunningCount = 0;
    private int _longRunningMinute = 0;
    private int _longRunningSecond = 0;
    //
    private int _grade = 0;

    public bool InitInfo(int repeatLongRunningCount, int longRunningMinute, int longRunningSecond)
    {
        _repeatLongRunningCount = repeatLongRunningCount;
        _longRunningMinute = longRunningMinute;
        _longRunningSecond = longRunningSecond;

        return true;
    }
}

// 순발력
public class AgilityInfo
{
    private int _fiftyMRunningSecond = 0;
    private float _standingBroadJumpCm = 0;
    //
    private int _grade = 0;

    public bool InitInfo(int fiftyMRunningSecond, float standingBroadJumpCm)
    {
        _fiftyMRunningSecond = fiftyMRunningSecond;
        _standingBroadJumpCm = standingBroadJumpCm;

        return true;
    }
}

// 근지구력
public class MuscularEnduranceInfo
{
    private int _sitUpCount = 0;
    private float _gripRightKG = 0;
    private float _gripLeftKG = 0;
    //
    private int _grade = 0;

    public bool SetInfo(int sitUpCount, float gripRightKG, float gripLeftKG)
    {
        _sitUpCount = sitUpCount;
        _gripRightKG = gripRightKG;
        _gripLeftKG = gripLeftKG;

        return true;
    }
}

// 유연성
public class FlexibilityInfo
{
    private float _sitUpperBodyFrontBendCm;
    //
    private int _grade = 0;

    public bool SetInfo(float sitUpperBodyFrontBendCm)
    {
        _sitUpperBodyFrontBendCm = sitUpperBodyFrontBendCm;

        return true;
    }
}

public class BMIInfo
{
    private float _height = 0;
    private float _weight = 0;
    //
    private int _grade = 0;

    public bool SetInfo(int height, int weight)
    {
        _height = height;
        _weight = weight;

        return true;
    }
}