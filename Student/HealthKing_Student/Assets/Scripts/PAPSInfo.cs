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

public class CardiovascularEndurance
{
    private int _longRunningCount;
    private int _longRunningMinute;
    private int _longRunningSecond;

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

    bool InitInfo(int fiftyMRunningSecond, int standingBroadJumpCm)
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

    public bool SetInfo(int height, int weight)
    {
        _height = height;
        _weight = weight;

        return true;
    }
}