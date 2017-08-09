using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour {

    private static AppManager _instance;
    public static AppManager GetInstance()
    {
        return _instance;
    }

    private UserInfo _userInfo;
    public UserInfo userInfo { get { return _userInfo; } }
    private PAPSInfo _papsInfo;
    public PAPSInfo papsInfo { get { return _papsInfo; } }
    private MissionInfo _missionInfo;
    public MissionInfo missionInfo { get { return _missionInfo; } }
    private PAPSTableManager _papsTableManager;
    public PAPSTableManager papsTableManager { get { return _papsTableManager; } set { _papsTableManager = papsTableManager; } }

    private const int _MAXCOUNT = 6;
    private const int _MAXCOUNT1 = 3;
    private const int _MAXCOUNT2 = 2;
    private const int _MAXCOUNT3 = 3;
    private const int _MAXCOUNT4 = 1;
    private const int _MAXCOUNT5 = 2;
    private const int _MAX_MISSION_COUNT = 4;

    void Awake()
    {
        _instance = this;
        _userInfo = new UserInfo();
        _papsInfo = new PAPSInfo();
        _missionInfo = new MissionInfo();
        _papsTableManager = new PAPSTableManager();
        _papsTableManager.ReadTableFile();
    }

    // Use this for initialization
    void Start()
    {
        DataManager.getInstance();
    }

    //
    public bool SetUserInfo(List<string> listString)
    {
        if (listString.Count != _MAXCOUNT)
        {
            Debug.Log("정보를 입력해주세요");
            return false;
        }

        if (false == _userInfo.InitUserInfo(listString))
        {
            Debug.Log("정보를 입력해주세요");
            return false;
        }

        PlayerPrefs.SetString("User_SchoolName", listString[0]);
        PlayerPrefs.SetString("User_SchoolGrade", listString[1]);
        PlayerPrefs.SetString("User_ClassNum", listString[2]);
        PlayerPrefs.SetString("User_Number", listString[3]);
        PlayerPrefs.SetString("User_Name", listString[4]);
        PlayerPrefs.SetString("User_Gender", listString[5]);

        return true;
    }

    public void SetCardiovascularEnduranceInfo(List<string> listString)
    {
        if(listString.Count != _MAXCOUNT1)
        {
            Debug.Log("listString.Count != _maxCount1" + listString.Count);
            return;
        }

        PlayerPrefs.SetString("Cardi_RepeatLongRun", listString[0]);
        PlayerPrefs.SetString("Cardi_LongRunMinute", listString[1]);
        PlayerPrefs.SetString("Cardi_LongRunSecond", listString[2]);

        _papsInfo._cardiovascularEndurance.InitInfo(listString);
    }

    public void SetAgilityInfo(List<string> listString)
    {
        if (listString.Count != _MAXCOUNT2)
        {
            Debug.Log("listString.Count != _maxCount2" + listString.Count);
            return;
        }

        PlayerPrefs.SetString("Agile_StandingBroadJump", listString[0]);
        PlayerPrefs.SetString("Agile_FiftyMRun", listString[1]);

        _papsInfo._agility.InitInfo(listString);
    }

    public void SetMuscularEnduranceInfo(List<string> listString)
    {
        if (listString.Count != _MAXCOUNT3)
        {
            Debug.Log("listString.Count != _maxCount3" + listString.Count);
            return;
        }

        PlayerPrefs.SetString("Musc_SitUp", listString[0]);
        PlayerPrefs.SetString("Musc_GripRight", listString[1]);
        PlayerPrefs.SetString("Musc_GripLeft", listString[2]);

        _papsInfo._muscularEndurance.InitInfo(listString);
    }

    public void SetFlexibilityInfo(List<string> listString)
    {
        if (listString.Count != _MAXCOUNT4)
        {
            Debug.Log("listString.Count != _maxCount4" + listString.Count);
            return;
        }

        PlayerPrefs.SetString("Flexibility_FrontBend", listString[0]);

        _papsInfo._flexibility.InitInfo(listString);
    }

    public void SetBMIInfo(List<string> listString)
    {
        if (listString.Count != _MAXCOUNT5)
        {
            Debug.Log("listString.Count != _maxCount5" + listString.Count);
            return;
        }

        PlayerPrefs.SetString("BMI_Height", listString[0]);
        PlayerPrefs.SetString("BMI_Weight", listString[1]);

        _papsInfo._BMI.InitInfo(listString);
    }

    public void SetMissionInfo(List<string> listString)
    {
        if(listString.Count != _MAX_MISSION_COUNT)
        {
            Debug.Log("listString.Count != _MAX_MISSION_COUNT" + listString.Count);
            return;
        }

        for(int i = 0; i < _MAX_MISSION_COUNT; ++i)
            PlayerPrefs.SetString("Mission"+i, listString[i]);

        _missionInfo.InitMissionInfo(listString);
    }

    // Update is called once per frame
    void Update () {
        
    }
}
