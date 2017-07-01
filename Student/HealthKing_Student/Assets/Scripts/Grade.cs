using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grade
{
    static public PAPS_GRADE GetCardiGrade()
    {
        int gender = AppManager.GetInstance().userInfo.GetGender();
        int schoolGrade = AppManager.GetInstance().userInfo.GetSchoolGrade();
        int value1 = AppManager.GetInstance().papsInfo._cardiovascularEndurance.GetRepeatLongRunningCount();
        int value2 = AppManager.GetInstance().papsInfo._cardiovascularEndurance.GetLongRunningSecond();

        PAPS_GRADE grade1 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.REPEAT_LONG_RUNNING, gender, (SCHOOL_GRADE)schoolGrade, value1);
        PAPS_GRADE grade2 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.LONG_RUNNING, gender, (SCHOOL_GRADE)schoolGrade, value2);
        return grade1 > grade2 ? grade2 : grade1;
    }

    static public PAPS_GRADE GetAgilityGrade()
    {
        int gender = AppManager.GetInstance().userInfo.GetGender();
        int schoolGrade = AppManager.GetInstance().userInfo.GetSchoolGrade();
        float value1 = AppManager.GetInstance().papsInfo._agility.GetFiftyMRunningSecond();
        float value2 = AppManager.GetInstance().papsInfo._agility.GetStandingBroadJumpCm();

        PAPS_GRADE grade1 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.FIFTY_M_RUNNING, gender, (SCHOOL_GRADE)schoolGrade, value1);
        PAPS_GRADE grade2 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.STANDING_BROAD_JUMP, gender, (SCHOOL_GRADE)schoolGrade, value2);
        return grade1 > grade2 ? grade2 : grade1;
    }

    static public PAPS_GRADE GetMusGrade()
    {
        int gender = AppManager.GetInstance().userInfo.GetGender();
        int schoolGrade = AppManager.GetInstance().userInfo.GetSchoolGrade();
        int value1 = AppManager.GetInstance().papsInfo._muscularEndurance.GetSitUpCount();
        float value2 = AppManager.GetInstance().papsInfo._muscularEndurance.GetGrip();

        PAPS_GRADE grade1 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.SIT_UP, gender, (SCHOOL_GRADE)schoolGrade, value1);
        PAPS_GRADE grade2 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.GRIP, gender, (SCHOOL_GRADE)schoolGrade, value2);
        return grade1 > grade2 ? grade2 : grade1;
    }

    static public PAPS_GRADE GetFlexibilityGrade()
    {
        int gender = AppManager.GetInstance().userInfo.GetGender();
        int schoolGrade = AppManager.GetInstance().userInfo.GetSchoolGrade();
        float value = AppManager.GetInstance().papsInfo._flexibility.GetFlexibility();

        return AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.SIT_UPPERBODY_FRONTBEND, gender, (SCHOOL_GRADE)schoolGrade, value);
    }

    static public PAPS_GRADE GetBMIGrade()
    {
        int gender = AppManager.GetInstance().userInfo.GetGender();
        int schoolGrade = AppManager.GetInstance().userInfo.GetSchoolGrade();
        float value = AppManager.GetInstance().papsInfo._BMI.GetBMI();

        return AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.BMI, gender, (SCHOOL_GRADE)schoolGrade, value);
    }
}