using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentDBInfo
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

    public StudentDBInfo(string id, int schoolUnique, string schoolName, string schoolGrade, byte grade, byte classNum, byte number, string gender, string name)
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

