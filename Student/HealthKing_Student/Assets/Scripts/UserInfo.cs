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

    public MissionInfo()
    {
        _mission = new List<string>();
        for(int i = 0; i < 4; ++i)
            _mission.Add("");

        Debug.Log(_mission.Count);
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
    private string id;          // 아이디
    private int schoolUnique;   // 학교번호
    private string schoolName;  // 학교이름
    private string schoolGrade; // 초,중,고등학교
    private short grade;        // 학년
    private short classNum;     // 반
    private short number;       // 번호
    private string gender;      // 성별
    private string name;        // 이름

    public StudentInfo(string id, int schoolUnique, string schoolName, string schoolGrade, short grade, short classNum, short number, string gender, string name)
    {
        this.id = id;
        this.schoolUnique = schoolUnique;
        this.schoolName = schoolName;
        this.schoolGrade = schoolGrade;
        this.grade = grade;
        this.classNum = classNum;
        this.number = number;
        this.gender = gender;
        this.name = name;
    }

    public string GetId()
    {
        return id;
    }

    public void SetId(string id)
    {
        this.id = id;
    }

    public string GetName()
    {
        return name;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public string GetGender()
    {
        return gender;
    }

    public void SetGender(string gender)
    {
        this.gender = gender;
    }

    public string GetSchoolName()
    {
        return schoolName;
    }

    public void SetSchoolName(string schoolName)
    {
        this.schoolName = schoolName;
    }

    public string GetSchool()
    {
        return schoolGrade;
    }

    public void SetSchool(string schoolGrade)
    {
        this.schoolGrade = schoolGrade;
    }

    public short GetGrade()
    {
        return grade;
    }

    public void SetGrade(short grade)
    {
        this.grade = grade;
    }

    public short GetClassroomNumber()
    {
        return classNum;
    }

    public void SetClassroomNumber(short classroomNumber)
    {
        this.classNum = classroomNumber;
    }

    public short GetNumber()
    {
        return number;
    }

    public void SetNumber(short number)
    {
        this.number = number;
    }

    public void Print()
    {
        Debug.Log("id: " + id + " schoolUnique: " + schoolUnique.ToString() + " schoolName: " + schoolName +
            ' ' + schoolGrade + " name: " + name + " gender: " + gender  + " grade: " + grade + " classNumber: " + classNum.ToString() + " number: " + number);
    }
}

