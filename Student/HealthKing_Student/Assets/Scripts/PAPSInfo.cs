using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DEFINE
{
    public enum CARDI_INFO
    {
        R_LONGRUN_COUNT,
        LONGRUN_MINUTE,
        LONGRUN_SECOND,
        MAX_CARDI_INFO
    }

    public enum AGILE_INFO
    {
        STAND_JUMP_CM,
        FIFTY_M_RUN_SECOND,
        MAX_AGILE_INFO
    }

    public enum MUS_ENDU_INFO
    {
        SITUP_COUNT,
        GRIP_R_KG,
        GRIP_L_KG,
        MAX_MUS_ENDU_INFO
    }

    public enum FLEXIBLE_INFO
    {
        FRONT_BEND_CM,
        MAX_FLEXIBLE_INFO
    }

    public enum BMI_INFO
    {
        HEIGHT,
        WEIGHT,
        MAX_BMI_INFO
    }
}

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
        _repeatLongRunningCount = System.Convert.ToInt32(listString[(int)DEFINE.CARDI_INFO.R_LONGRUN_COUNT].Trim());
        _longRunningMinute = System.Convert.ToInt32(listString[(int)DEFINE.CARDI_INFO.LONGRUN_MINUTE].Trim());
        _longRunningSecond = System.Convert.ToInt32(listString[(int)DEFINE.CARDI_INFO.LONGRUN_SECOND].Trim());

        return true;
    }

    public List<string> GetInfo()
    {
        List<string> list = new List<string>();

        list.Add(_repeatLongRunningCount.ToString());
        list.Add(_longRunningMinute.ToString());
        list.Add(_longRunningSecond.ToString());

        return list;
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
    private float _standingBroadJumpCm = 0;
    private float _fiftyMRunningSecond = 0;

    public bool InitInfo(List<string> listString)
    {
        _standingBroadJumpCm = System.Convert.ToSingle(listString[(int)DEFINE.AGILE_INFO.STAND_JUMP_CM].Trim());
        _fiftyMRunningSecond = System.Convert.ToSingle(listString[(int)DEFINE.AGILE_INFO.FIFTY_M_RUN_SECOND].Trim());

        return true;
    }

    public List<string> GetInfo()
    {
        List<string> list = new List<string>();

        list.Add(_standingBroadJumpCm.ToString());
        list.Add(_fiftyMRunningSecond.ToString());

        return list;
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
        _sitUpCount = System.Convert.ToInt32(listString[(int)DEFINE.MUS_ENDU_INFO.SITUP_COUNT].Trim());
        _gripRightKG = System.Convert.ToSingle(listString[(int)DEFINE.MUS_ENDU_INFO.GRIP_R_KG].Trim());
        _gripLeftKG = System.Convert.ToSingle(listString[(int)DEFINE.MUS_ENDU_INFO.GRIP_L_KG].Trim());

        return true;
    }

    public List<string> GetInfo()
    {
        List<string> list = new List<string>();

        list.Add(_sitUpCount.ToString());
        list.Add(_gripRightKG.ToString());
        list.Add(_gripLeftKG.ToString());

        return list;
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
        _sitUpperBodyFrontBendCm = System.Convert.ToSingle(listString[(int)DEFINE.FLEXIBLE_INFO.FRONT_BEND_CM].Trim());

        return true;
    }

    public List<string> GetInfo()
    {
        List<string> list = new List<string>();

        list.Add(_sitUpperBodyFrontBendCm.ToString());

        return list;
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
        _height = System.Convert.ToSingle(listString[(int)DEFINE.BMI_INFO.HEIGHT].Trim());
        _weight = System.Convert.ToSingle(listString[(int)DEFINE.BMI_INFO.WEIGHT].Trim());

        return true;
    }

    public List<string> GetInfo()
    {
        List<string> list = new List<string>();

        list.Add(_height.ToString());
        list.Add(_weight.ToString());

        return list;
    }

    public float GetBMI()
    {
        if (_weight == 0 || _height == 0)
            return 0;

        return _weight / (_height * 0.01f * _height * 0.01f);
    }
}