using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;  // JSON 파서 사용
using UnityEngine.UI;

public class DataManager
{
    private StudentInfo _studentInfo;
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
        form.AddField("ID", id_pwInput[0].text);
        form.AddField("Password", id_pwInput[1].text);
        WWW www = new WWW("http://came1230.cafe24.com/StudentLogin.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);
        if (!SetStudentInfo(www.text))
            return false;

        return true;
    }

    public bool GetCardiRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", "22");
        WWW www = new WWW("http://came1230.cafe24.com/GetCardiRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool GetAgileRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", "22");
        WWW www = new WWW("http://came1230.cafe24.com/GetAgileRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool GetMuscRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", "22");
        WWW www = new WWW("http://came1230.cafe24.com/GetMuscRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool GetTrackRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("CardiRecordUnique", 1);
        WWW www = new WWW("http://came1230.cafe24.com/GetTrackRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool GetCardiAvgRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", "22");
        form.AddField("TotalMeter", 1000);
        form.AddField("TotalTrackCount", 5);
        WWW www = new WWW("http://came1230.cafe24.com/GetCardiAvgPerTrackRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool GetAgileAvgRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", "22");
        form.AddField("Meter", 50);
        WWW www = new WWW("http://came1230.cafe24.com/GetAgileAvgRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool GetMuscAvgRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", "22");
        WWW www = new WWW("http://came1230.cafe24.com/GetMuscAvgRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool GetCardiNorDistRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", "22");
        form.AddField("RecordUnique", 2);
        form.AddField("TotalMeter", 1000);
        form.AddField("TotalTrackCount", 5);
        form.AddField("TotalElapsedTime", 400000);
        WWW www = new WWW("http://came1230.cafe24.com/GetCardiNormalDistributionRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool GetAgileNorDistRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", "22");
        form.AddField("RecordUnique", 2);
        form.AddField("Meter", 50);
        form.AddField("ElapsedTime", 8210);
        WWW www = new WWW("http://came1230.cafe24.com/GetAgileNormalDistributionRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool GetMuscNorDistRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", "22");
        form.AddField("RecordUnique", 1);
        form.AddField("Count", 40);
        WWW www = new WWW("http://came1230.cafe24.com/GetMuscNormalDistributionRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool GetMission()
    {
        WWWForm form = new WWWForm();
        form.AddField("SchoolUnique", 1);
        form.AddField("Grade", 6);
        WWW www = new WWW("http://came1230.cafe24.com/GetMission.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

        return true;
    }

    public bool SetFinMissionOfStudent()
    {
        WWWForm form = new WWWForm();
        form.AddField("MissionUnique", 1);
        form.AddField("ID", "22");
        WWW www = new WWW("http://came1230.cafe24.com/SetFinishedMissionOfStudent.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);

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
            string ID = jObject.GetString("ID");
            int schoolUnique = System.Convert.ToInt32(jObject.GetString("SchoolUnique").Trim());
            string schoolName = jObject.GetString("SchoolName");
            string schoolGrade = jObject.GetString("SchoolGrade");
            short grade = System.Convert.ToInt16(jObject.GetString("Grade").Trim());
            short classNum = System.Convert.ToInt16(jObject.GetString("Class").Trim());
            short number = System.Convert.ToInt16(jObject.GetString("Number").Trim());
            string gender = jObject.GetString("Gender");
            string name = jObject.GetString("Name");

            _studentInfo = new StudentInfo(ID, schoolUnique, schoolName, schoolGrade, grade, classNum, number, gender, name);
            _studentInfo.Print();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        return true;
    }

    public bool GetStudentRecordData()
    {
        //WWWForm form = new WWWForm();
        //form.AddField("userID", _studentData.GetId());
        //WWW www = new WWW("http://came1230.cafe24.com/GetRecordData.php", form);
        //while (!www.isDone)
        //    Debug.Log(www.bytesDownloaded);

        //Debug.Log(www.text);
        //if (!SetStudentRecordInfo(www.text))
        //    return false;

        return false;
    }

    public bool SetStudentRecordInfo(string data)
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
