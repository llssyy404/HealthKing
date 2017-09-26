using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo {
    private string _schoolName = "";
    private int _schoolGrade = 4;
    private int _classNum = 0;
    private int _number = 0;
    private string _name = "";
    private int _gender = 0;

    public bool InitUserInfo(List<string> listString)
    {
        _schoolName = listString[0];
        _schoolGrade = System.Convert.ToInt32(listString[1].Trim());
        _classNum = System.Convert.ToInt32(listString[2].Trim());
        _number = System.Convert.ToInt32(listString[3].Trim());
        _name = listString[4];
        _gender = System.Convert.ToInt32(listString[5].Trim());

        if (IsInitInfo() == false)
            return false;

        return true;
    }

    public bool IsInitInfo()
    {
        if (_schoolName == "")
        {
            Debug.Log("학교이름 안적었어");
            return false;
        }

        if (_schoolGrade == 0)
        {
            Debug.Log("학년 안적었어");
            return false;
        }

        if (_classNum == 0)
        {
            Debug.Log("반 안적었어");
            return false;
        }

        if (_number == 0)
        {
            Debug.Log("번호 안적었어");
            return false;
        }

        if (_name == "")
        {
            Debug.Log("이름 안적었어");
            return false;
        }

        return true;
    }

    public List<string> GetInfo()
    {
        List<string> list = new List<string>();

        list.Add(_schoolName.ToString());
        list.Add(_schoolGrade.ToString());
        list.Add(_classNum.ToString());
        list.Add(_number.ToString());
        list.Add(_name.ToString());
        list.Add(_gender.ToString());

        return list;
    }

    public int GetGender() { return _gender; }
    public int GetSchoolGrade() { return _schoolGrade; } 
}

public class MissionInfo
{
    private List<string> _mission;
    private List<bool> _clearMission;

    public MissionInfo()
    {
        _mission = new List<string>();
        _clearMission = new List<bool>();
        for (int i = 0; i < 4; ++i)
        {
            _mission.Add("");
            _clearMission.Add(false);
        }

        Debug.Log(_mission.Count);
    }

    public bool GetClearMission(int index)
    {
        if (index >= 4)
            return false;

        return _clearMission[index];
    }

    public void SetClearMission(int index, bool isClear)
    {
        if (index >= 4)
            return;

        _clearMission[index] = isClear;
    }

    public void InitMissionInfo(List<string> listString)
    {
        _mission[0] = listString[0];
        _mission[1] = listString[1];
        _mission[2] = listString[2];
        _mission[3] = listString[3];
    }

    public List<string> GetInfo()
    {
        List<string> list = new List<string>();
        list.Add(_mission[0]);
        list.Add(_mission[1]);
        list.Add(_mission[2]);
        list.Add(_mission[3]);

        return list;
    }
}

public class StudentInfo
{
    private string _id;          // 아이디
    private int _schoolUnique;   // 학교번호
    private string _schoolName;  // 학교이름
    private string _schoolGrade; // 초,중,고등학교
    private byte _grade;        // 학년
    private byte _classNum;     // 반
    private byte _number;       // 번호
    private string _gender;      // 성별
    private string _name;        // 이름

    public string id
    {
        get { return _id; }
        set { _id = value; }
    }

    public int schoolUnique
    {
        get { return _schoolUnique; }
        set { _schoolUnique = value; }
    }

    public string schoolName
    {
        get { return _schoolName; }
        set { _schoolName = value; }
    }

    public string schoolGrade
    {
        get { return _schoolGrade; }
        set { _schoolGrade = value; }
    }

    public byte grade
    {
        get { return _grade; }
        set { _grade = value; }
    }

    public byte classNum
    {
        get { return _classNum; }
        set { _classNum = value; }
    }

    public byte number
    {
        get { return _number; }
        set { _number = value; }
    }

    public string gender
    {
        get { return _gender; }
        set { _gender = value; }
    }

    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    public StudentInfo(string id, int schoolUnique, string schoolName, string schoolGrade, byte grade, byte classNum, byte number, string gender, string name)
    {
        _id = id;
        _schoolUnique = schoolUnique;
        _schoolName = schoolName;
        _schoolGrade = schoolGrade;
        _grade = grade;
        _classNum = classNum;
        _number = number;
        _gender = gender;
        _name = name;
    }

    public void Print()
    {
        Debug.Log("id: " + _id + " schoolUnique: " + _schoolUnique.ToString() + " schoolName: " + _schoolName +
            ' ' + _schoolGrade + " name: " + _name + " gender: " + _gender + " grade: " + _grade + " classNumber: " + _classNum.ToString() + " number: " + _number);
    }
}

