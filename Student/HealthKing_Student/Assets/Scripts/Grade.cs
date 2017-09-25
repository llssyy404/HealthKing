using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grade
{
    static private void GetGenderAndGrade(ref int gender, ref int grade)
    {
        gender = DataManager.GetInstance().studentInfo.gender == "남" ? 0 : 1;
        int schoolGrade = 0;
        if (DataManager.GetInstance().studentInfo.schoolGrade == "초등학교")
            schoolGrade = 0;
        else if (DataManager.GetInstance().studentInfo.schoolGrade == "중학교")
            schoolGrade = 2;
        else
            schoolGrade = 3;
        grade = DataManager.GetInstance().studentInfo.grade + schoolGrade * 3;
    }

    static public PAPS_GRADE GetCardiGrade()
    {
        int gender = 0, grade = 0;
        GetGenderAndGrade(ref gender, ref grade);
        int value1 = AppManager.GetInstance().papsInfo._cardiovascularEndurance.GetRepeatLongRunningCount();
        int value2 = AppManager.GetInstance().papsInfo._cardiovascularEndurance.GetLongRunningSecond();

        PAPS_GRADE grade1 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.REPEAT_LONG_RUNNING, gender, (SCHOOL_GRADE)grade, value1);
        PAPS_GRADE grade2 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.LONG_RUNNING, gender, (SCHOOL_GRADE)grade, value2);
        return grade1 > grade2 ? grade2 : grade1;
    }

    static public PAPS_GRADE GetAgilityGrade()
    {
        int gender = 0, grade = 0;
        GetGenderAndGrade(ref gender, ref grade);
        float value1 = AppManager.GetInstance().papsInfo._agility.GetFiftyMRunningSecond();
        float value2 = AppManager.GetInstance().papsInfo._agility.GetStandingBroadJumpCm();

        PAPS_GRADE grade1 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.FIFTY_M_RUNNING, gender, (SCHOOL_GRADE)grade, value1);
        PAPS_GRADE grade2 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.STANDING_BROAD_JUMP, gender, (SCHOOL_GRADE)grade, value2);
        return grade1 > grade2 ? grade2 : grade1;
    }

    static public PAPS_GRADE GetMusGrade()
    {
        int gender = 0, grade = 0;
        GetGenderAndGrade(ref gender, ref grade);
        int value1 = AppManager.GetInstance().papsInfo._muscularEndurance.GetSitUpCount();
        float value2 = AppManager.GetInstance().papsInfo._muscularEndurance.GetGrip();

        PAPS_GRADE grade1 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.SIT_UP, gender, (SCHOOL_GRADE)grade, value1);
        PAPS_GRADE grade2 = AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.GRIP, gender, (SCHOOL_GRADE)grade, value2);
        return grade1 > grade2 ? grade2 : grade1;
    }

    static public PAPS_GRADE GetFlexibilityGrade()
    {
        int gender = 0, grade = 0;
        GetGenderAndGrade(ref gender, ref grade);
        float value = AppManager.GetInstance().papsInfo._flexibility.GetFlexibility();

        return AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.SIT_UPPERBODY_FRONTBEND, gender, (SCHOOL_GRADE)grade, value);
    }

    static public PAPS_GRADE GetBMIGrade()
    {
        int gender = 0, grade = 0;
        GetGenderAndGrade(ref gender, ref grade);
        float value = AppManager.GetInstance().papsInfo._BMI.GetBMI();

        return AppManager.GetInstance().papsTableManager.FindPAPSGrade(TABLE_TYPE.BMI, gender, (SCHOOL_GRADE)grade, value);
    }
}