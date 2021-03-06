﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;  // JSON 파서 사용
using UnityEngine.UI;

public class NetworkManager
{
    static private NetworkManager _instance;
    static public NetworkManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new NetworkManager();
            _instance.Init();
        }
        return _instance;
    }

    private StudentDBInfo _studentInfo;
    private List<CardiRecordDBInfo> _cardiRecordList;
    private List<AgileRecordDBInfo> _agileRecordList;
    private List<MuscRecordDBInfo> _muscRecordList;
    private List<TrackRecordDBInfo> _trackRecordList;
    private List<SchoolMissionDBInfo> _schoolMissionList;
    private List<int> _avgTrackRecordList;
    private int _avgAgileRecord;
    private int _avgMuscRecord;
    private int _normalDistMyPercent;

    public StudentDBInfo studentInfo
    {
        get { return _studentInfo; }
        private set { _studentInfo = value; }
    }

    public List<CardiRecordDBInfo> cardiRecordList
    {
        get { return _cardiRecordList; }
        private set { _cardiRecordList = value; }
    }

    public List<AgileRecordDBInfo> agileRecordList
    {
        get { return _agileRecordList; }
        private set { _agileRecordList = value; }
    }

    public List<MuscRecordDBInfo> muscRecordList
    {
        get { return _muscRecordList; }
        private set { _muscRecordList = value; }
    }

    public List<TrackRecordDBInfo> trackRecordList
    {
        get { return _trackRecordList; }
        private set { _trackRecordList = value; }
    }

    public List<SchoolMissionDBInfo> schoolMissionList
    {
        get { return _schoolMissionList; }
        private set { _schoolMissionList = value; }
    }

    public List<int> avgTrackRecordList
    {
        get { return _avgTrackRecordList; }
        private set { _avgTrackRecordList = value; }
    }

    public int avgAgileRecord
    {
        get { return _avgAgileRecord; }
        private set { _avgAgileRecord = value; }
    }

    public int avgMuscRecord
    {
        get { return _avgMuscRecord; }
        private set { _avgMuscRecord = value; }
    }

    public int normalDistMyPercent
    {
        get { return _normalDistMyPercent; }
        private set { _normalDistMyPercent = value; }
    }

    public void Init()
    {
        _cardiRecordList = new List<CardiRecordDBInfo>();
        _agileRecordList = new List<AgileRecordDBInfo>();
        _muscRecordList = new List<MuscRecordDBInfo>();
        _trackRecordList = new List<TrackRecordDBInfo>();
        _schoolMissionList = new List<SchoolMissionDBInfo>();
        _avgTrackRecordList = new List<int>();
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

        if (!SetStudentInfo(www.text))
            return false;

        return true;
    }

    public bool LoadData()
    {
        if (!GetCardiRecord())
            return false;

        if (!GetAgileRecord())
            return false;

        if (!GetMuscRecord())
            return false;

        if (!GetMission())
            return false;

        return true;
    }

    public bool GetCardiRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _studentInfo.id);
        WWW www = new WWW("http://came1230.cafe24.com/GetCardiRecord.php", form);
        while (!www.isDone)
            continue;

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

        if (!SetMuscRecordInfo(www.text))
            return false;

        return true;
    }

    public bool GetTrackRecord(int cardiRecordUnique)
    {
        WWWForm form = new WWWForm();
        form.AddField("CardiRecordUnique", cardiRecordUnique);
        WWW www = new WWW("http://came1230.cafe24.com/GetTrackRecord.php", form);
        while (!www.isDone)
            continue;

        if (!SetTrackRecordInfo(www.text))
            return false;

        return true;
    }

    public bool GetCardiAvgRecord(int totalMeter, int totalTrackCount)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _studentInfo.id);
        form.AddField("TotalMeter", totalMeter);
        form.AddField("TotalTrackCount", totalTrackCount);
        WWW www = new WWW("http://came1230.cafe24.com/GetCardiAvgPerTrackRecord.php", form);
        while (!www.isDone)
            continue;

        if (!SetCardiAvgRecordInfo(www.text))
            return false;

        return true;
    }

    public bool GetAgileAvgRecord(int meter)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _studentInfo.id);
        form.AddField("Meter", meter);
        WWW www = new WWW("http://came1230.cafe24.com/GetAgileAvgRecord.php", form);
        while (!www.isDone)
            continue;

        if (!SetAgileAvgRecordInfo(www.text))
            return false;

        return true;
    }

    public bool GetMuscAvgRecord()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _studentInfo.id);
        WWW www = new WWW("http://came1230.cafe24.com/GetMuscAvgRecord.php", form);
        while (!www.isDone)
            continue;

        if (!SetMuscAvgRecordInfo(www.text))
            return false;

        return true;
    }

    public bool GetCardiNorDistRecord(int cardiRecordUnique, int totalMeter, int totalTrackCount, int totalElapsedTime)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _studentInfo.id);
        form.AddField("RecordUnique", cardiRecordUnique);
        form.AddField("TotalMeter", totalMeter);
        form.AddField("TotalTrackCount", totalTrackCount);
        form.AddField("TotalElapsedTime", totalElapsedTime);
        WWW www = new WWW("http://came1230.cafe24.com/GetCardiNormalDistributionRecord.php", form);
        while (!www.isDone)
            continue;

        if (!SetNorDistRecordInfo(www.text))
            return false;

        return true;
    }

    public bool GetAgileNorDistRecord(int agileRecordUnique, int meter, int elapsedTime)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _studentInfo.id);
        form.AddField("RecordUnique", agileRecordUnique);
        form.AddField("Meter", meter);
        form.AddField("ElapsedTime", elapsedTime);
        WWW www = new WWW("http://came1230.cafe24.com/GetAgileNormalDistributionRecord.php", form);
        while (!www.isDone)
            continue;

        if (!SetNorDistRecordInfo(www.text))
            return false;

        return true;
    }

    public bool GetMuscNorDistRecord(int muscRecordUnique, int count)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _studentInfo.id);
        form.AddField("RecordUnique", muscRecordUnique);
        form.AddField("Count", count);
        WWW www = new WWW("http://came1230.cafe24.com/GetMuscNormalDistributionRecord.php", form);
        while (!www.isDone)
            continue;

        if (!SetNorDistRecordInfo(www.text))
            return false;

        return true;
    }

    public bool GetMission()
    {
        WWWForm form = new WWWForm();
        form.AddField("SchoolUnique", _studentInfo.schoolUnique);
        form.AddField("Grade", _studentInfo.grade);
        WWW www = new WWW("http://came1230.cafe24.com/GetMission.php", form);
        while (!www.isDone)
            continue;

        if (!SetMissionInfo(www.text))
            return false;

        return true;
    }

    public bool SetFinMissionOfStudent(int missionUnique)
    {
        if (ExistFinMissionOfStudent(missionUnique))
            return false;

        WWWForm form = new WWWForm();
        form.AddField("MissionUnique", missionUnique);
        form.AddField("ID", _studentInfo.id);
        WWW www = new WWW("http://came1230.cafe24.com/SetFinishedMissionOfStudent.php", form);
        while (!www.isDone)
            continue;

        return true;
    }

    public bool ExistFinMissionOfStudent(int missionUnique)
    {
        WWWForm form = new WWWForm();
        form.AddField("MissionUnique", missionUnique);
        form.AddField("ID", _studentInfo.id);
        WWW www = new WWW("http://came1230.cafe24.com/ExistFinishedMissionOfStudent.php", form);
        while (!www.isDone)
            continue;

        if (!IsExistFinMissionOfStudent(www.text))
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
            string ID = jObject.GetString("ID");
            int schoolUnique = System.Convert.ToInt32(jObject.GetString("SchoolUnique").Trim());
            string schoolName = jObject.GetString("SchoolName");
            string schoolGrade = jObject.GetString("SchoolGrade");
            byte grade = System.Convert.ToByte(jObject.GetString("Grade").Trim());
            byte classNum = System.Convert.ToByte(jObject.GetString("Class").Trim());
            byte number = System.Convert.ToByte(jObject.GetString("Number").Trim());
            string gender = jObject.GetString("Gender");
            string name = jObject.GetString("Name");

            _studentInfo = new StudentDBInfo(ID, schoolUnique, schoolName, schoolGrade, grade, classNum, number, gender, name);
            _studentInfo.Print();
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
        _cardiRecordList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return true;

            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                long recordUnique = System.Convert.ToInt64(jObject.GetString("RecordUnique"));
                DateTime dateTime = System.Convert.ToDateTime(jObject.GetString("Date"));
                int totalMeter = System.Convert.ToInt32(jObject.GetString("TotalMeter"));
                int totalTrackCount = System.Convert.ToInt32(jObject.GetString("TotalTrackCount"));
                int totalElapsedTime = System.Convert.ToInt32(jObject.GetString("TotalElapsedTime"));

                CardiRecordDBInfo cardiRecord = new CardiRecordDBInfo(recordUnique, dateTime, totalMeter, totalTrackCount, totalElapsedTime);
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
        _agileRecordList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return true;

            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                long recordUnique = System.Convert.ToInt64(jObject.GetString("RecordUnique"));
                DateTime dateTime = System.Convert.ToDateTime(jObject.GetString("Date"));
                int meter = System.Convert.ToInt32(jObject.GetString("Meter"));
                int elapsedTime = System.Convert.ToInt32(jObject.GetString("ElapsedTime"));

                AgileRecordDBInfo agileRecord = new AgileRecordDBInfo(recordUnique, dateTime, meter, elapsedTime);
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
        _muscRecordList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return true;

            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                long recordUnique = System.Convert.ToInt64(jObject.GetString("RecordUnique"));
                DateTime dateTime = System.Convert.ToDateTime(jObject.GetString("Date"));
                int count = System.Convert.ToInt32(jObject.GetString("Count"));

                MuscRecordDBInfo muscRecord = new MuscRecordDBInfo(recordUnique, dateTime, count);
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
        _trackRecordList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                long trackRecordUnique = System.Convert.ToInt64(jObject.GetString("TrackRecordUnique"));
                long cardiRecordUnique = System.Convert.ToInt64(jObject.GetString("CardiRecordUnique"));
                int trackIndex = System.Convert.ToInt32(jObject.GetString("TrackIndex"));
                int elapsedTime = System.Convert.ToInt32(jObject.GetString("ElapsedTime"));

                TrackRecordDBInfo trackRecord = new TrackRecordDBInfo(trackRecordUnique, cardiRecordUnique, trackIndex, elapsedTime);
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

    public bool SetCardiAvgRecordInfo(string data)      // 같은 학교, 학년, 성별, 기준인 학생들 평균 기록
    {
        _avgTrackRecordList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                int trackIndex = System.Convert.ToInt32(jObject.GetString("TrackIndex"));
                int perTrackElapsedTime = (int)System.Convert.ToDouble(jObject.GetString("PerTrackElapsedTime"));
                _avgTrackRecordList.Add(perTrackElapsedTime);
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

            JSONObject jObject = jsonArray[0].Obj;
            int avgElapsedTime = (int)System.Convert.ToDouble(jObject.GetString("AvgElapsedTime"));
            _avgAgileRecord = avgElapsedTime;
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

            JSONObject jObject = jsonArray[0].Obj;
            int avgCount = (int)System.Convert.ToDouble(jObject.GetString("AvgCount"));
            _avgMuscRecord = avgCount;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        return true;
    }

    public bool SetNorDistRecordInfo(string data)   // 같은 학교, 학년, 성별, 기록인 학생들 중 해당 학생의 백분위
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            JSONObject jObject = jsonArray[0].Obj;
            int percentile = System.Convert.ToInt32(jObject.GetNumber("Percentile"));
            _normalDistMyPercent = percentile;
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
        _schoolMissionList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                long missionUnique = System.Convert.ToInt64(jObject.GetString("MissionUnique"));
                string missionDesc = jObject.GetString("MissionDesc");
                SchoolMissionDBInfo mission = new SchoolMissionDBInfo(missionUnique, missionDesc);
                _schoolMissionList.Add(mission);
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

    public bool IsExistFinMissionOfStudent(string data)
    {
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONObject obj = jsonObject.GetObject("response");
            bool exist = obj.GetBoolean("Exist");
            return exist;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }
    }
}
