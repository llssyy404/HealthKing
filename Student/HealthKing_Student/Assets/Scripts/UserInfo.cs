using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo {
    private string _schoolName;
    private int _classGrade;
    private int _classNum;
    private int _number;
    private string _name;
    private string _gender;

    public bool InitUserInfo(string schoolName, int classGrade, int classNum
        , int number, string name, string gender)
    {
        if (schoolName == null)
            return false;

        if (classGrade == 0)
            return false;

        if (classNum == 0)
            return false;

        if (number == 0)
            return false;

        if (name == null)
            return false;

        if (gender == null)
            return false;

        _schoolName = schoolName;
        _classGrade = classGrade;
        _classNum = classNum;
        _number = number;
        _name = name;
        _gender = gender;

        return true;
    }

    public void Render()
    {
    }
}
