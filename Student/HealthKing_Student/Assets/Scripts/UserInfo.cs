using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;  // JSON 파서 사용

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

public class StudentData
{
    private string id; //아이디
    private string password; // 패스워드
    private string name;  //이름
    private string gender; //성별
    private string school; // 초,중,고등학교
    private string grade; //학년
    private string classroomNumber; //반

    public StudentData(string id, string password, string name, string gender, string school, string grade, string classroomNumber)
    {
        this.id = id;
        this.password = password;
        this.name = name;
        this.gender = gender;
        this.school = school;
        this.grade = grade;
        this.classroomNumber = classroomNumber;
    }

    public string GetId()
    {
        return id;
    }

    public void SetId(string id)
    {
        this.id = id;
    }

    public string GetPassword()
    {
        return password;
    }

    public void SetPassword(string password)
    {
        this.password = password;
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

    public string GetSchool()
    {
        return school;
    }

    public void SetSchool(string school)
    {
        this.school = school;
    }

    public string GetGrade()
    {
        return grade;
    }

    public void SetGrade(string grade)
    {
        this.grade = grade;
    }

    public string GetClassroomNumber()
    {
        return classroomNumber;
    }

    public void SetClassroomNumber(string classroomNumber)
    {
        this.classroomNumber = classroomNumber;
    }

    public void Print()
    {
        Debug.Log(" id: " + id + " pw: " + password + " name: " + name + " gender: " + gender + " school: " + school + " grade: " + grade + " classroomNumber: " + classroomNumber);
    }
}

public class DataManager
{
    private List<StudentData> studentDataList;
    //private List<StudentRecordData> sendStudentRecordDataList; // 서버에 저장할 레코드 데이터 리스트
    //private List<StudentRecordData> studentRecordDataList; // 서버에서 가져온 모든 레코드 데이터 리스트

    static private DataManager _instance;

    static public DataManager getInstance()
    {
        if (_instance == null)
        {
            _instance = new DataManager();
            _instance.Init();
        }
        return _instance;
    }


    public void Init()
    {
        studentDataList = new List<StudentData>();
        //sendStudentRecordDataList = new List<StudentRecordData>();
        //studentRecordDataList = new List<StudentRecordData();
        string url = "http://came1230.cafe24.com/GetAllUserList.php";
        WWW webSite = new WWW(url);
        while (!webSite.isDone)
            Debug.Log(webSite.bytesDownloaded);

        Debug.Log(webSite.text);
        SetStudentDatas(webSite.text);
    }

    // 학생 정보 getter, setter

    public void SetStudentDatas(string data)
    {
        studentDataList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            int count = 0;
            string userID, userPassword, userName, userGender, userSchool, userGrade, userClassroom;
            while (count < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[count].Obj;
                userID = jObject.GetString("userID");
                userPassword = jObject.GetString("userPassword");
                userName = jObject.GetString("userName");
                userGender = jObject.GetString("userGender");
                userSchool = jObject.GetString("userSchool");
                userGrade = jObject.GetString("userGrade");
                userClassroom = jObject.GetString("userClassroom");
                StudentData studentData = new StudentData(userID, userPassword, userName, userGender, userSchool, userGrade, userClassroom);
                studentDataList.Add(studentData);
                studentData.Print();
                ++count;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

    }

    public List<StudentData> getStudentDataList()
    {
        return studentDataList;
    }
}
