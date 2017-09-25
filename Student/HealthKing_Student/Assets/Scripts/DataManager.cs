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

    private List<CardiRecord> _cardiRecordList;
    private List<AgileRecord> _agileRecordList;
    private List<MuscRecord> _muscRecordList;
    private List<TrackRecord> _trackRecordList;
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

    public StudentInfo studentInfo
    {
        get { return _studentInfo; }
        private set { _studentInfo = value; }
    }

    public List<CardiRecord> cardiRecordList
    {
        get { return _cardiRecordList; }
        private set { _cardiRecordList = value; }
    }

    public List<AgileRecord> agileRecordList
    {
        get { return _agileRecordList; }
        private set { _agileRecordList = value; }
    }

    public List<MuscRecord> muscRecordList
    {
        get { return _muscRecordList; }
        private set { _muscRecordList = value; }
    }

    public void Init()
    {
        _studentRecordDataList = new List<StudentRecordData>();
        _myRecordData = new MyRecordData();
        _cardiRecordList = new List<CardiRecord>();
        _agileRecordList = new List<AgileRecord>();
        _muscRecordList = new List<MuscRecord>();
        _trackRecordList = new List<TrackRecord>();

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

        GetCardiRecord();
        GetAgileRecord();
        GetMuscRecord();
        GetTrackRecord();
        GetCardiAvgRecord();
        GetAgileAvgRecord();
        GetMuscAvgRecord();
        GetCardiNorDistRecord();
        GetAgileNorDistRecord();
        GetMuscNorDistRecord();
        GetMission();
        //SetFinMissionOfStudent();

        return true;
    }

    public bool GetCardiRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _studentInfo.id);
        WWW www = new WWW("http://came1230.cafe24.com/GetCardiRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);
        if (!SetCardiRecordInfo(www.text))
            return false;

        return true;
    }

    public bool GetAgileRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _studentInfo.id);
        WWW www = new WWW("http://came1230.cafe24.com/GetAgileRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);
        if (!SetAgileRecordInfo(www.text))
            return false;

        return true;
    }

    public bool GetMuscRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _studentInfo.id);
        WWW www = new WWW("http://came1230.cafe24.com/GetMuscRecord.php", form);
        while (!www.isDone)
            continue;

        Debug.Log(www.text);
        if (!SetMuscRecordInfo(www.text))
            return false;

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
        if (!SetTrackRecordInfo(www.text))
            return false;

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
        if (!SetCardiAvgRecordInfo(www.text))
            return false;

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
        if (!SetAgileAvgRecordInfo(www.text))
            return false;

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
        if (!SetMuscAvgRecordInfo(www.text))
            return false;

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
        if (!SetNorDistRecordInfo(www.text))
            return false;

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
        if (!SetNorDistRecordInfo(www.text))
            return false;

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
        if (!SetNorDistRecordInfo(www.text))
            return false;

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
        if (!SetMissionInfo(www.text))
            return false;

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
            byte grade = System.Convert.ToByte(jObject.GetString("Grade").Trim());
            byte classNum = System.Convert.ToByte(jObject.GetString("Class").Trim());
            byte number = System.Convert.ToByte(jObject.GetString("Number").Trim());
            string gender = jObject.GetString("Gender");
            string name = jObject.GetString("Name");

            _studentInfo = new StudentInfo(ID, schoolUnique, schoolName, schoolGrade, grade, classNum, number, gender, name);
            _studentInfo.Print();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
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
            return false;
        }

        return true;
    }

    public bool SetCardiRecordInfo(string data)
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return true;

            Debug.Log(jsonArray.Length);

            Int64 recordUnique;
            DateTime dateTime;
            int totalMeter, totalTrackCount, totalElapsedTime;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                recordUnique = System.Convert.ToInt64(jObject.GetString("RecordUnique"));
                dateTime = System.Convert.ToDateTime(jObject.GetString("Date"));
                totalMeter = System.Convert.ToInt32(jObject.GetString("TotalMeter"));
                totalTrackCount = System.Convert.ToInt32(jObject.GetString("TotalTrackCount"));
                totalElapsedTime = System.Convert.ToInt32(jObject.GetString("TotalElapsedTime"));

                CardiRecord cardiRecord = new CardiRecord(recordUnique, dateTime, totalMeter, totalTrackCount, totalElapsedTime);
                _cardiRecordList.Add(cardiRecord);
                cardiRecord.Print();
                ++i;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        return true;
    }

    public bool SetAgileRecordInfo(string data)
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return true;

            Debug.Log(jsonArray.Length);

            Int64 recordUnique;
            DateTime dateTime;
            int meter, elapsedTime;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                recordUnique = System.Convert.ToInt64(jObject.GetString("RecordUnique"));
                dateTime = System.Convert.ToDateTime(jObject.GetString("Date"));
                meter = System.Convert.ToInt32(jObject.GetString("Meter"));
                elapsedTime = System.Convert.ToInt32(jObject.GetString("ElapsedTime"));

                AgileRecord agileRecord = new AgileRecord(recordUnique, dateTime, meter, elapsedTime);
                _agileRecordList.Add(agileRecord);
                agileRecord.Print();
                ++i;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        return true;
    }

    public bool SetMuscRecordInfo(string data)
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return true;

            Debug.Log(jsonArray.Length);

            Int64 recordUnique;
            DateTime dateTime;
            int count;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                recordUnique = System.Convert.ToInt64(jObject.GetString("RecordUnique"));
                dateTime = System.Convert.ToDateTime(jObject.GetString("Date"));
                count = System.Convert.ToInt32(jObject.GetString("Count"));

                MuscRecord muscRecord = new MuscRecord(recordUnique, dateTime, count);
                _muscRecordList.Add(muscRecord);
                muscRecord.Print();
                ++i;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        return true;
    }

    public bool SetTrackRecordInfo(string data)
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            Debug.Log(jsonArray.Length);

            Int64 trackRecordUnique, cardiRecordUnique;
            int trackIndex, elapsedTime;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                trackRecordUnique = System.Convert.ToInt64(jObject.GetString("TrackRecordUnique"));
                cardiRecordUnique = System.Convert.ToInt64(jObject.GetString("CardiRecordUnique"));
                trackIndex = System.Convert.ToInt32(jObject.GetString("TrackIndex"));
                elapsedTime = System.Convert.ToInt32(jObject.GetString("ElapsedTime"));

                TrackRecord trackRecord = new TrackRecord(trackRecordUnique, cardiRecordUnique, trackIndex, elapsedTime);
                _trackRecordList.Add(trackRecord);
                trackRecord.Print();
                ++i;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        return true;
    }

    public bool SetCardiAvgRecordInfo(string data)
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            Debug.Log(jsonArray.Length);

            int trackIndex, perTrackElapsedTime;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                trackIndex = System.Convert.ToInt32(jObject.GetString("TrackIndex"));
                perTrackElapsedTime = (int)System.Convert.ToDouble(jObject.GetString("PerTrackElapsedTime"));
                Debug.Log(trackIndex + " " + perTrackElapsedTime);
                ++i;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        return true;
    }

    public bool SetAgileAvgRecordInfo(string data)
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            int avgElapsedTime;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                avgElapsedTime = (int)System.Convert.ToDouble(jObject.GetString("AvgElapsedTime"));
                Debug.Log(avgElapsedTime);
                ++i;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        return true;
    }

    public bool SetMuscAvgRecordInfo(string data)
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            int avgCount;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                avgCount = (int)System.Convert.ToDouble(jObject.GetString("AvgCount"));
                Debug.Log(avgCount);
                ++i;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        return true;
    }

    public bool SetNorDistRecordInfo(string data)   // 정규분포 기준 학생의 퍼센트
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            Debug.Log(jsonArray.Length);

            double percentile = 0;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                percentile = System.Convert.ToInt32(jObject.GetNumber("Percentile"));
                Debug.Log(percentile);
                ++i;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        return true;
    }

    public bool SetMissionInfo(string data)
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            Debug.Log(jsonArray.Length);

            Int64 missionUnique;
            string missionDesc;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                missionUnique = System.Convert.ToInt64(jObject.GetString("MissionUnique"));
                missionDesc = jObject.GetString("MissionDesc");
                Debug.Log(missionUnique + " " + missionDesc);
                ++i;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        return true;
    }
}
