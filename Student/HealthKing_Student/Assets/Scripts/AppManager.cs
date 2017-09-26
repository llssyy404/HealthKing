using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour {

    private static AppManager _instance;
    public static AppManager GetInstance()
    {
        return _instance;
    }

    private PAPSInfo _papsInfo;
    public PAPSInfo papsInfo { get { return _papsInfo; } }
    private MissionInfo _missionInfo;
    public MissionInfo missionInfo { get { return _missionInfo; } }
    private PAPSTableManager _papsTableManager;
    public PAPSTableManager papsTableManager { get { return _papsTableManager; } set { _papsTableManager = papsTableManager; } }

    private const int _MAXCOUNT = 6;
    private const int _MAX_MISSION_COUNT = 4;

    void Awake()
    {
        _instance = this;
        _papsInfo = new PAPSInfo();
        _missionInfo = new MissionInfo();
        _papsTableManager = new PAPSTableManager();
        _papsTableManager.ReadTableFile();
    }

    // Use this for initialization
    void Start()
    {
        NetworkManager.GetInstance();
    }

    //
    public void SetCardiovascularEnduranceInfo(List<string> listString)
    {
        if (listString.Count != (int)DEFINE.CARDI_INFO.MAX_CARDI_INFO)
        {
            Debug.Log("listString.Count != MAX_CARDI_INFO" + listString.Count);
            return;
        }

        PlayerPrefs.SetString("Cardi_RepeatLongRun", listString[(int)DEFINE.CARDI_INFO.R_LONGRUN_COUNT]);
        PlayerPrefs.SetString("Cardi_LongRunMinute", listString[(int)DEFINE.CARDI_INFO.LONGRUN_MINUTE]);
        PlayerPrefs.SetString("Cardi_LongRunSecond", listString[(int)DEFINE.CARDI_INFO.LONGRUN_SECOND]);

        _papsInfo._cardiovascularEndurance.InitInfo(listString);
    }

    public void SetAgilityInfo(List<string> listString)
    {
        if (listString.Count != (int)DEFINE.AGILE_INFO.MAX_AGILE_INFO)
        {
            Debug.Log("listString.Count != MAX_AGILE_INFO" + listString.Count);
            return;
        }

        PlayerPrefs.SetString("Agile_StandingBroadJump", listString[(int)DEFINE.AGILE_INFO.STAND_JUMP_CM]);
        PlayerPrefs.SetString("Agile_FiftyMRun", listString[(int)DEFINE.AGILE_INFO.FIFTY_M_RUN_SECOND]);

        _papsInfo._agility.InitInfo(listString);
    }

    public void SetMuscularEnduranceInfo(List<string> listString)
    {
        if (listString.Count != (int)DEFINE.MUS_ENDU_INFO.MAX_MUS_ENDU_INFO)
        {
            Debug.Log("listString.Count != MAX_MUS_ENDU_INFO" + listString.Count);
            return;
        }

        PlayerPrefs.SetString("Musc_SitUp", listString[(int)DEFINE.MUS_ENDU_INFO.SITUP_COUNT]);
        PlayerPrefs.SetString("Musc_GripRight", listString[(int)DEFINE.MUS_ENDU_INFO.GRIP_R_KG]);
        PlayerPrefs.SetString("Musc_GripLeft", listString[(int)DEFINE.MUS_ENDU_INFO.GRIP_L_KG]);

        _papsInfo._muscularEndurance.InitInfo(listString);
    }

    public void SetFlexibilityInfo(List<string> listString)
    {
        if (listString.Count != (int)DEFINE.FLEXIBLE_INFO.MAX_FLEXIBLE_INFO)
        {
            Debug.Log("listString.Count != MAX_FLEXIBLE_INFO" + listString.Count);
            return;
        }

        PlayerPrefs.SetString("Flexibility_FrontBend", listString[(int)DEFINE.FLEXIBLE_INFO.FRONT_BEND_CM]);

        _papsInfo._flexibility.InitInfo(listString);
    }

    public void SetBMIInfo(List<string> listString)
    {
        if (listString.Count != (int)DEFINE.BMI_INFO.MAX_BMI_INFO)
        {
            Debug.Log("listString.Count != MAX_BMI_INFO" + listString.Count);
            return;
        }

        PlayerPrefs.SetString("BMI_Height", listString[(int)DEFINE.BMI_INFO.HEIGHT]);
        PlayerPrefs.SetString("BMI_Weight", listString[(int)DEFINE.BMI_INFO.WEIGHT]);

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
        {
            PlayerPrefs.SetString("Mission" + i, listString[i]);
            PlayerPrefs.SetString("ClearMission" + i, missionInfo.GetClearMission(i).ToString());
        }

        _missionInfo.InitMissionInfo(listString);
    }

    // Update is called once per frame
    void Update () {
        
    }
}
