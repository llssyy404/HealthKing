using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;  // JSON 파서 사용
using UnityEngine.UI;

public class DataManager
{
    private List<StudentData> _studentDataList;
    //private List<StudentRecordData> sendStudentRecordDataList; // 서버에 저장할 레코드 데이터 리스트
    private List<StudentRecordData> _studentRecordDataList; // 서버에서 가져온 모든 레코드 데이터 리스트
    static private DataManager _instance;

    static public DataManager GetInstance()
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
        _studentDataList = new List<StudentData>();
        //sendStudentRecordDataList = new List<StudentRecordData>();
        //studentRecordDataList = new List<StudentRecordData();
        string url = "http://came1230.cafe24.com/GetAllUserList.php";
        WWW webSite = new WWW(url);
        while (!webSite.isDone)
            Debug.Log(webSite.bytesDownloaded);

        //Debug.Log(webSite.text);
        //SetStudentDatas(webSite.text);
    }

    // 학생 정보 getter, setter
    public void SetStudentDatas(string data)
    {
        _studentDataList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            int count = 0;
            string userID, userPassword, userName, userGender, userSchoolName, userSchool, userGrade, userClassroom, userNumber;
            while (count < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[count].Obj;
                userID = jObject.GetString("userID");
                userPassword = jObject.GetString("userPassword");
                userName = jObject.GetString("userName");
                userGender = jObject.GetString("userGender");
                userSchoolName = jObject.GetString("userSchoolName");
                userSchool = jObject.GetString("userSchool");
                userGrade = jObject.GetString("userGrade");
                userClassroom = jObject.GetString("userClassroom");
                userNumber = jObject.GetString("userNumber");
                StudentData studentData = new StudentData(userID, userPassword, userName, userGender, userSchoolName, userSchool, userGrade, userClassroom, userNumber);
                _studentDataList.Add(studentData);
                //studentData.Print();
                ++count;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public List<StudentData> GetStudentDataList()
    {
        return _studentDataList;
    }

    public IEnumerator JoinStudent()
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", "IDDDDD");
        form.AddField("userPassword", "password");
        form.AddField("userName", "이름");
        form.AddField("userGender", "여자");
        form.AddField("userSchool", "초등학교");
        form.AddField("userGrade", "1학년");
        form.AddField("userClassroom", "1반");
        WWW www = new WWW("http://came1230.cafe24.com/UserRegister.php", form);
        yield return www;
        // StartCoroutine(DataManager.getInstance().JoinStudent()); // 웹서버로 데이터 보내기 테스트
    }

    public bool LoginStudent(List<InputField> id_pwInput)
    {
        if (id_pwInput.Count != 2)
            return false;

        WWWForm form = new WWWForm();
        form.AddField("userID", id_pwInput[0].text);
        form.AddField("userPassword", id_pwInput[1].text);
        WWW www = new WWW("http://came1230.cafe24.com/UserLogin.php", form);
        while (!www.isDone)
            Debug.Log(www.bytesDownloaded);

        Debug.Log(www.text);
        if (!SetStudentInfo(www.text))
            return false;

        return true;
    }

    public bool SetStudentInfo(string data)
    {
        _studentDataList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            string userID, userPassword, userName, userGender, userSchoolName, userSchool, userGrade, userClassroom, userNumber;
            if (jsonArray.Length == 0)
                return false;

            JSONObject jObject = jsonArray[0].Obj;
            userID = jObject.GetString("userID");
            userPassword = jObject.GetString("userPassword");
            userName = jObject.GetString("userName");
            userGender = jObject.GetString("userGender");
            userSchoolName = jObject.GetString("userSchoolName");
            userSchool = jObject.GetString("userSchool");
            userGrade = jObject.GetString("userGrade");
            userClassroom = jObject.GetString("userClassroom");
            userNumber = jObject.GetString("userNumber");
            StudentData studentData = new StudentData(userID, userPassword, userName, userGender, userSchoolName, userSchool, userGrade, userClassroom, userNumber);
            _studentDataList.Add(studentData);
            studentData.Print();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        return true;
    }
}
