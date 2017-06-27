using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAPSInfo {
    public CardiovascularEnduranceInfo _cardiovascularEndurance;   // 심폐지구력
    public AgilityInfo _agility;                   // 순발력
    public MuscularEnduranceInfo _muscularEndurance;         // 근지구력
    public FlexibilityInfo _flexibility;               // 유연성
    public BMIInfo _BMI;                       // BMI

    public PAPSInfo()
    {
        _cardiovascularEndurance = new CardiovascularEnduranceInfo();
        _agility = new AgilityInfo();
        _muscularEndurance = new MuscularEnduranceInfo();
        _flexibility = new FlexibilityInfo();
        _BMI = new BMIInfo();
    }
}

// 심폐지구력
public class CardiovascularEnduranceInfo
{
    private int _repeatLongRunningCount = 0;
    private int _longRunningMinute = 0;
    private int _longRunningSecond = 0;

    public bool InitInfo(List<string> listString)
    {
        _repeatLongRunningCount = System.Convert.ToInt32(listString[0].Trim());
        _longRunningMinute = System.Convert.ToInt32(listString[1].Trim());
        _longRunningSecond = System.Convert.ToInt32(listString[2].Trim());

        return true;
    }

    public int GetRepeatLongRunningCount()
    {
        return _repeatLongRunningCount;
    }

    public int GetLongRunningSecond()
    {
        return (_longRunningMinute * 60) + _longRunningSecond;
    }
}

// 순발력
public class AgilityInfo
{
    private float _fiftyMRunningSecond = 0;
    private float _standingBroadJumpCm = 0;

    public bool InitInfo(List<string> listString)
    {
        _fiftyMRunningSecond = System.Convert.ToSingle(listString[0].Trim());
        _standingBroadJumpCm = System.Convert.ToSingle(listString[1].Trim());

        return true;
    }

    public float GetFiftyMRunningSecond()
    {
        return _fiftyMRunningSecond;
    }

    public float GetStandingBroadJumpCm()
    {
        return _standingBroadJumpCm;
    }
}

// 근지구력
public class MuscularEnduranceInfo
{
    private int _sitUpCount = 0;
    private float _gripRightKG = 0;
    private float _gripLeftKG = 0;

    public bool InitInfo(List<string> listString)
    {
        _sitUpCount = System.Convert.ToInt32(listString[0].Trim());
        _gripRightKG = System.Convert.ToSingle(listString[1].Trim());
        _gripLeftKG = System.Convert.ToSingle(listString[2].Trim());

        return true;
    }

    public int GetSitUpCount()
    {
        return _sitUpCount;
    }

    public float GetGrip()
    {
        return _gripLeftKG > _gripRightKG ? _gripLeftKG : _gripRightKG;
    }
}

// 유연성
public class FlexibilityInfo
{
    private float _sitUpperBodyFrontBendCm;

    public bool InitInfo(List<string> listString)
    {
        _sitUpperBodyFrontBendCm = System.Convert.ToSingle(listString[0].Trim());

        return true;
    }

    public float GetFlexibility()
    {
        return _sitUpperBodyFrontBendCm;
    }
}

public class BMIInfo
{
    private float _height = 0;
    private float _weight = 0;

    public bool InitInfo(List<string> listString)
    {
        _height = System.Convert.ToSingle(listString[0].Trim());
        _weight = System.Convert.ToSingle(listString[1].Trim());

        return true;
    }

    public float GetBMI()
    {
        return _weight / (_height / 0.01f * _height * 0.01f);
    }
}