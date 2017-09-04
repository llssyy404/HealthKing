using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;  // JSON 파서 사용
using UnityEngine.UI;

public class DataManager
{
    private StudentData _studentData;
    private List<StudentRecordData> _studentRecordDataList; // 서버에서 가져온 모든 레코드 데이터 리스트
    private MyRecordData _myRecordData;
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

    public MyRecordData GetMyRecordData()
    {
        return _myRecordData;
    }

    public List<StudentRecordData> GetStudentRecord()
    {
        return _studentRecordDataList;
    }

    public void Init()
    {
        _studentRecordDataList = new List<StudentRecordData>();
        _myRecordData = new MyRecordData();
        //string url = "http://came1230.cafe24.com/GetAllUserList.php";
        //WWW webSite = new WWW(url);
        //while (!webSite.isDone)
        //    Debug.Log(webSite.bytesDownloaded);

        //Debug.Log(webSite.text);
        //SetStudentDatas(webSite.text);
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
            continue;

        Debug.Log(www.text);
        if (!SetStudentInfo(www.text))
            return false;

        return true;
    }

    public bool SetStudentInfo(string data)
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            JSONObject jObject = jsonArray[0].Obj;
            string userID = jObject.GetString("userID");
            string userPassword = jObject.GetString("userPassword");
            string userName = jObject.GetString("userName");
            string userGender = jObject.GetString("userGender");
            string userSchoolName = jObject.GetString("userSchoolName");
            string userSchool = jObject.GetString("userSchool");
            string userGrade = jObject.GetString("userGrade");
            string userClassroom = jObject.GetString("userClassroom");
            string userNumber = jObject.GetString("userNumber");
            _studentData = new StudentData(userID, userPassword, userName, userGender, userSchoolName, 
                userSchool, userGrade, userClassroom, userNumber);
            _studentData.Print();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        return true;
    }

    public bool GetStudentRecordData()
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", _studentData.GetId());
        WWW www = new WWW("http://came1230.cafe24.com/GetRecordData.php", form);
        while (!www.isDone)
            Debug.Log(www.bytesDownloaded);

        Debug.Log(www.text);
        if (!SetStudentRecordInfo(www.text))
            return false;

        return true;
    }

    public bool SetStudentRecordInfo(string data)    // test
    {
        _studentRecordDataList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            string userID, recordDate, allTrackTimeDate;
            int recordMeter, trackCount;
            List<string> trackTimeDate = new List<string>();
            int count = 0;
            if (jsonArray.Length == 0)
                return false;

            while (count < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[count].Obj;
                userID = jObject.GetString("userID");
                recordDate = jObject.GetString("recordDate");
                recordMeter = System.Convert.ToInt32(jObject.GetString("recordMeter"));
                trackCount = System.Convert.ToInt32(jObject.GetString("trackCount"));
                string[] trackTimeDateString = jObject.GetString("trackTimeDate").Split(',');
                for (int i = 0; i < trackTimeDateString.Length; ++i)
                {
                    trackTimeDate.Add(trackTimeDateString[i]);
                }
                allTrackTimeDate = jObject.GetString("allTrackTimeDate");
                StudentRecordData studentData = new StudentRecordData(recordDate, recordMeter, trackCount, trackTimeDate, allTrackTimeDate);
                _studentRecordDataList.Add(studentData);
                ++count;
            }

            _myRecordData.Init(_studentRecordDataList);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        return true;
    }

}
