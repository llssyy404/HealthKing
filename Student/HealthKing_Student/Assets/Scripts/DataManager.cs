using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;  // JSON 파서 사용
using UnityEngine.UI;

public class DataManager
{
    private StudentInfo _studentInfo;

    private List<CardiRecord> _cardiRecordList;
    private List<AgileRecord> _agileRecordList;
    private List<MuscRecord> _muscRecordList;
    private List<TrackRecord> _trackRecordList;
    private List<SchoolMission> _schoolMissionList;
    private List<int> _avgTrackRecordList;
    private int _avgAgileRecord;
    private int _avgMuscRecord;
    private int _normalDistMyPercent;

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

    public List<TrackRecord> trackRecordList
    {
        get { return _trackRecordList; }
        private set { _trackRecordList = value; }
    }

    public List<SchoolMission> schoolMissionList
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
        _cardiRecordList = new List<CardiRecord>();
        _agileRecordList = new List<AgileRecord>();
        _muscRecordList = new List<MuscRecord>();
        _trackRecordList = new List<TrackRecord>();
        _schoolMissionList = new List<SchoolMission>();
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

    public bool SetCardiRecordInfo(string data)
    {
        _cardiRecordList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return true;

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
        _agileRecordList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return true;

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
        _muscRecordList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return true;

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
        _trackRecordList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

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
        _avgTrackRecordList.Clear();
        try
        {
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONArray jsonArray = jsonObject.GetArray("response");
            if (jsonArray.Length == 0)
                return false;

            int trackIndex, perTrackElapsedTime;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                trackIndex = System.Convert.ToInt32(jObject.GetString("TrackIndex"));
                perTrackElapsedTime = (int)System.Convert.ToDouble(jObject.GetString("PerTrackElapsedTime"));
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

            int avgElapsedTime;
            JSONObject jObject = jsonArray[0].Obj;
            avgElapsedTime = (int)System.Convert.ToDouble(jObject.GetString("AvgElapsedTime"));
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

            int avgCount;
            JSONObject jObject = jsonArray[0].Obj;
            avgCount = (int)System.Convert.ToDouble(jObject.GetString("AvgCount"));
            _avgMuscRecord = avgCount;
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

            int percentile = 0;
            JSONObject jObject = jsonArray[0].Obj;
            percentile = System.Convert.ToInt32(jObject.GetNumber("Percentile"));
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

            Int64 missionUnique;
            string missionDesc;
            int i = 0;
            while (i < jsonArray.Length)
            {
                JSONObject jObject = jsonArray[i].Obj;
                missionUnique = System.Convert.ToInt64(jObject.GetString("MissionUnique"));
                missionDesc = jObject.GetString("MissionDesc");
                SchoolMission mission = new SchoolMission(missionUnique, missionDesc);
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
            bool exist = false;
            JSONObject jsonObject = JSONObject.Parse(data);
            JSONObject obj = jsonObject.GetObject("response");
            exist = obj.GetBoolean("Exist");
            return exist;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }
    }
}
