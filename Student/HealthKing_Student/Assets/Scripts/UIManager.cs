using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CP.ProChart;

enum PAGE_TYPE
{
    LOGIN,
    MAIN,
    BASE_INFORM,
    PAPS,
    CARDI_ENDU,
    AGILITY,
    MUSC_ENDU,
    FLEXIBILITY,
    BMI,
    PAPS_RESULT,
    FITNESS_UP_TIP,
    MY_RECORD,
    MY_MISSION,
    RECORD_CARDI,
    RECORD_CARDI_DATE,
    RECORD_CARDI_BAR_GRAPH,
    RECORD_CARDI_LINE_GRAPH,
    RECORD_CARDI_NORMAL_DISTRIB,
    RECORD_AGILE_MUSC_BAR_GRAPH,
    MAX_PAGE_TYPE
}

enum URL_TYPE
{
    CARDI_ENDU,
    FLEXIBILITY,
    MUSC_ENDU,
    AGILITY,    // 순발력
    QUCKNESS,   // 민첩성
    WARMING_UP,
    NOTICE_BOARD,
    MAX_URL_TYPE
}

enum STUDENT_INFO_TEXT
{
    SCHOOLNAME,
    SCHOOLGRADE,
    GRADE,
    CLASS,
    NUMBER,
    NAME,
    GENDER
}

enum RECORD_TYPE
{
    CARDI,
    AGILE,
    MUSC
}

public class UIManager : MonoBehaviour {

    public List<Text> _studentInput;
    public List<InputField> _cardiInput;
    public List<InputField> _agilityInput;
    public List<InputField> _muscInput;
    public List<InputField> _flexibilityInput;
    public List<InputField> _bmiInput;
    public List<InputField> _missionInput;
    public List<InputField> _id_pwInput;
    public Button _meterButton;
    public GameObject _meterContent;
    public Button _dateButton;
    public GameObject _dateContent;
    public GameObject _schoolMissionObj;
    public GameObject _schoolMissionContent;

    private GameObject[] _obj = null;
    private GameObject[] _missionObj = null;
    private int _selNum = 0;
    private int _prevSelNum = 0;
    private int _curMissionCount = 0;
    private List<string> _listString;
    private string[] _strPAPSGrade;
    private string[] _strBMIGrade;
    private string _input = null;
    private RECORD_TYPE _selRecordType = RECORD_TYPE.CARDI;

    const int _MAX_GRADE_COUNT = 6;
    const int _SCH_GRADE_VALUE = 4;
    const int _MAX_MISSION = 4;

    // Chart
    public BarChart _barChart;
    ChartData2D _dataSet;

    // Test
    Key testKey = new Key(4, 4);

    // Use this for initialization
    void Start () {
        InitObject();
        InitGrade();
        InitInput();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackPage();
    }

    // Init
    void InitObject()
    {
        _obj = new GameObject[(int)PAGE_TYPE.MAX_PAGE_TYPE];
        _obj[(int)PAGE_TYPE.LOGIN] = GameObject.Find("Canvas").transform.Find("Login").gameObject;
        _obj[(int)PAGE_TYPE.MAIN] = GameObject.Find("Canvas").transform.Find("Main").gameObject;
        _obj[(int)PAGE_TYPE.BASE_INFORM] = GameObject.Find("Canvas").transform.Find("BaseInform").gameObject;
        _obj[(int)PAGE_TYPE.PAPS] = GameObject.Find("Canvas").transform.Find("PAPS").gameObject;
        _obj[(int)PAGE_TYPE.CARDI_ENDU] = GameObject.Find("Canvas").transform.Find("CardiovascularEndurance").gameObject;
        _obj[(int)PAGE_TYPE.AGILITY] = GameObject.Find("Canvas").transform.Find("Agility").gameObject;
        _obj[(int)PAGE_TYPE.MUSC_ENDU] = GameObject.Find("Canvas").transform.Find("MuscularEndurance").gameObject;
        _obj[(int)PAGE_TYPE.FLEXIBILITY] = GameObject.Find("Canvas").transform.Find("Flexibility").gameObject;
        _obj[(int)PAGE_TYPE.BMI] = GameObject.Find("Canvas").transform.Find("BMI").gameObject;
        _obj[(int)PAGE_TYPE.PAPS_RESULT] = GameObject.Find("Canvas").transform.Find("PAPSResult").gameObject;
        _obj[(int)PAGE_TYPE.FITNESS_UP_TIP] = GameObject.Find("Canvas").transform.Find("FitnessUpTip").gameObject;
        _obj[(int)PAGE_TYPE.MY_RECORD] = GameObject.Find("Canvas").transform.Find("MyRecord").gameObject;
        _obj[(int)PAGE_TYPE.MY_MISSION] = GameObject.Find("Canvas").transform.Find("MyMission").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_CARDI] = GameObject.Find("Canvas").transform.Find("Record_Cardi").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_CARDI_DATE] = GameObject.Find("Canvas").transform.Find("Record_Cardi_Date").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH] = GameObject.Find("Canvas").transform.Find("Record_Cardi_BarGraph").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_CARDI_LINE_GRAPH] = GameObject.Find("Canvas").transform.Find("Record_Cardi_LineGraph").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB] = GameObject.Find("Canvas").transform.Find("Record_Cardi_Normal_Distribution").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_AGILE_MUSC_BAR_GRAPH] = GameObject.Find("Canvas").transform.Find("Record_Agile_Musc_BarGraph").gameObject;

        // 미션
        _missionObj = new GameObject[_MAX_MISSION];
        _missionObj[0] = GameObject.Find("Mission1");
        _missionObj[1] = GameObject.Find("Mission2");
        _missionObj[2] = GameObject.Find("Mission3");
        _missionObj[3] = GameObject.Find("Mission4");
        Debug.Log("미션초기화");
        _dateButtonList = new List<Button>();
        _meterButtonList = new List<Button>();
        _schoolMissionObjList = new List<GameObject>();
    }

    void InitGrade()
    {
        _listString = new List<string>();
        _strPAPSGrade = new string[_MAX_GRADE_COUNT];
        _strPAPSGrade[0] = "1등급";
        _strPAPSGrade[1] = "2등급";
        _strPAPSGrade[2] = "3등급";
        _strPAPSGrade[3] = "4등급";
        _strPAPSGrade[4] = "5등급";
        _strPAPSGrade[5] = "NONE";

        _strBMIGrade = new string[_MAX_GRADE_COUNT];
        _strBMIGrade[0] = "마름";
        _strBMIGrade[1] = "정상";
        _strBMIGrade[2] = "과체중";
        _strBMIGrade[3] = "경도비만";
        _strBMIGrade[4] = "고도비만";
        _strBMIGrade[5] = "NONE";
    }

    void InitInput()
    {
        InitCardiInput();
        InitAgileInput();
        InitMuscInput();
        InitFlexibilityInput();
        InitBMIInput();
        InitMissionInput();
    }

    void InitCardiInput()
    {
        _listString = AppManager.GetInstance().papsInfo._cardiovascularEndurance.GetInfo();
        if (PlayerPrefs.HasKey("Cardi_RepeatLongRun"))
        {
            _cardiInput[(int)DEFINE.CARDI_INFO.R_LONGRUN_COUNT].text = _listString[(int)DEFINE.CARDI_INFO.R_LONGRUN_COUNT] = PlayerPrefs.GetString("Cardi_RepeatLongRun");
        }
        if (PlayerPrefs.HasKey("Cardi_LongRunMinute"))
        {
            _cardiInput[(int)DEFINE.CARDI_INFO.LONGRUN_MINUTE].text = _listString[(int)DEFINE.CARDI_INFO.LONGRUN_MINUTE] = PlayerPrefs.GetString("Cardi_LongRunMinute");
        }
        if (PlayerPrefs.HasKey("Cardi_LongRunSecond"))
        {
            _cardiInput[(int)DEFINE.CARDI_INFO.LONGRUN_SECOND].text = _listString[(int)DEFINE.CARDI_INFO.LONGRUN_SECOND] = PlayerPrefs.GetString("Cardi_LongRunSecond");
        }
        AppManager.GetInstance().SetCardiovascularEnduranceInfo(_listString);
        _listString.Clear();
    }

    void InitAgileInput()
    {
        _listString = AppManager.GetInstance().papsInfo._agility.GetInfo();
        if (PlayerPrefs.HasKey("Agile_StandingBroadJump"))
        {
            _agilityInput[(int)DEFINE.AGILE_INFO.STAND_JUMP_CM].text = _listString[(int)DEFINE.AGILE_INFO.STAND_JUMP_CM] = PlayerPrefs.GetString("Agile_StandingBroadJump");
        }
        if (PlayerPrefs.HasKey("Agile_FiftyMRun"))
        {
            _agilityInput[(int)DEFINE.AGILE_INFO.FIFTY_M_RUN_SECOND].text = _listString[(int)DEFINE.AGILE_INFO.FIFTY_M_RUN_SECOND] = PlayerPrefs.GetString("Agile_FiftyMRun");
        }
        AppManager.GetInstance().SetAgilityInfo(_listString);
        _listString.Clear();
    }

    void InitMuscInput()
    {
        _listString = AppManager.GetInstance().papsInfo._muscularEndurance.GetInfo();
        if (PlayerPrefs.HasKey("Musc_SitUp"))
        {
            _muscInput[(int)DEFINE.MUS_ENDU_INFO.SITUP_COUNT].text = _listString[(int)DEFINE.MUS_ENDU_INFO.SITUP_COUNT] = PlayerPrefs.GetString("Musc_SitUp");
        }
        if (PlayerPrefs.HasKey("Musc_GripRight"))
        {
            _muscInput[(int)DEFINE.MUS_ENDU_INFO.GRIP_R_KG].text = _listString[(int)DEFINE.MUS_ENDU_INFO.GRIP_R_KG] = PlayerPrefs.GetString("Musc_GripRight");
        }
        if (PlayerPrefs.HasKey("Musc_GripLeft"))
        {
            _muscInput[(int)DEFINE.MUS_ENDU_INFO.GRIP_L_KG].text = _listString[(int)DEFINE.MUS_ENDU_INFO.GRIP_L_KG] = PlayerPrefs.GetString("Musc_GripLeft");
        }
        AppManager.GetInstance().SetMuscularEnduranceInfo(_listString);
        _listString.Clear();
    }

    void InitFlexibilityInput()
    {
        _listString = AppManager.GetInstance().papsInfo._flexibility.GetInfo();
        if (PlayerPrefs.HasKey("Flexibility_FrontBend"))
        {
            _flexibilityInput[(int)DEFINE.FLEXIBLE_INFO.FRONT_BEND_CM].text = _listString[(int)DEFINE.FLEXIBLE_INFO.FRONT_BEND_CM] = PlayerPrefs.GetString("Flexibility_FrontBend");
        }
        AppManager.GetInstance().SetFlexibilityInfo(_listString);
        _listString.Clear();
    } 

    void InitBMIInput()
    {
        _listString = AppManager.GetInstance().papsInfo._BMI.GetInfo();
        if (PlayerPrefs.HasKey("BMI_Height"))
        {
            _bmiInput[(int)DEFINE.BMI_INFO.HEIGHT].text = _listString[(int)DEFINE.BMI_INFO.HEIGHT] = PlayerPrefs.GetString("BMI_Height");
        }
        if (PlayerPrefs.HasKey("BMI_Weight"))
        {
            _bmiInput[(int)DEFINE.BMI_INFO.WEIGHT].text = _listString[(int)DEFINE.BMI_INFO.WEIGHT] = PlayerPrefs.GetString("BMI_Weight");
        }
        AppManager.GetInstance().SetBMIInfo(_listString);
        _listString.Clear();
    }

    void InitMissionInput()
    {
        _listString = AppManager.GetInstance().missionInfo.GetInfo();
        for(int i = 0; i < _MAX_MISSION; ++i)
        {
            if (PlayerPrefs.HasKey("Mission" + i))
            {
                _missionInput[i].text = _listString[i] = PlayerPrefs.GetString("Mission" + i);
            }

            if(PlayerPrefs.HasKey("ClearMission" + i))
            {
                Debug.Log(System.Convert.ToBoolean(PlayerPrefs.GetString("ClearMission" + i)));
                AppManager.GetInstance().missionInfo.SetClearMission(i, System.Convert.ToBoolean(PlayerPrefs.GetString("ClearMission" + i)));
            }
        }
        AppManager.GetInstance().SetMissionInfo(_listString);
        _listString.Clear();

    }

    // UI Function
    public void OnEndEdit(string str)
    {
        if (str == null)
            return;

        _input = str;
    }

    public void EditNum(int num)
    {
        if (_listString.Count < num)
            return;

        _listString[num] = _input;
    }

    public void OnValueChanged(int value)
    {
        if (_selNum != (int)PAGE_TYPE.BASE_INFORM)
            return;

        int input = value + _SCH_GRADE_VALUE;  // 4학년부터라...
        _listString[1] = input.ToString();
    }

    public void OnValueChanged(bool isCheck)
    {
        if (_selNum != (int)PAGE_TYPE.BASE_INFORM)
            return;

        if (isCheck)
            _listString[5] = "0";
        else
            _listString[5] = "1";
    }

    public void OnClickBtnPlayVideo(int sel)
    {
        switch((URL_TYPE)sel)
        {
            case URL_TYPE.CARDI_ENDU:       // 심폐지구력
                Application.OpenURL("https://www.youtube.com/playlist?list=PLVrMekaPceTrNqnsEVHQUhymcUoKECInN");
                break;
            case URL_TYPE.FLEXIBILITY:      // 유연성
                Application.OpenURL("https://www.youtube.com/playlist?list=PLVrMekaPceTrT6hIPPPNk2owGjk52fcD9");
                break;
            case URL_TYPE.MUSC_ENDU:        // 근력근지구력
                Application.OpenURL("https://www.youtube.com/playlist?list=PLVrMekaPceTrBFtg67BV5LurK8DoWfjXx");
                break;
            case URL_TYPE.AGILITY:          // 순발력
                Application.OpenURL("https://www.youtube.com/playlist?list=PLVrMekaPceTqrsl0yeS0S2yf__-PFmq3x");
                break;
            case URL_TYPE.QUCKNESS:         // 민첩성
                Application.OpenURL("https://www.youtube.com/playlist?list=PLVrMekaPceTp7lWrtcTUrMRkK4nSmp0TN");
                break;
            case URL_TYPE.WARMING_UP:      // 준비운동
                Application.OpenURL("https://www.youtube.com/playlist?list=PLVrMekaPceToV4wJr1WXwjc8QkNhwPVFH");
                break;
            case URL_TYPE.NOTICE_BOARD:      // 게시판
                Application.OpenURL("https://cafe.naver.com/redu5tc5");
                break;
            default:
                Debug.Log("Invalid URL_TYPE");
                break;
        }
    }

    public void OnClickStartBtn(int sel)
    {
        if (_selNum == sel || sel >= (int)PAGE_TYPE.MAX_PAGE_TYPE)
            return;

        if (_obj[_prevSelNum] == null || _obj[_selNum] == null)
            return;

        if (!PreSettingPage(sel))
            return;

        _prevSelNum = _selNum;
        _selNum = sel;
        _obj[_prevSelNum].SetActive(false);
        _obj[_selNum].SetActive(true);
        _listString.Clear();

        AfterSettingPage();
    }

    public void OnClickRecordTypeBtn(int recordType)
    {
        _selRecordType = (RECORD_TYPE)recordType;
    }

    public void DeleteMission(int index)
    {
        if (_listString.Count < index)
            return;

        _listString[index] = "";
        _missionInput[index].text = "";
        GameObject obj = _missionInput[index].transform.parent.gameObject;
        GameObject isClear = obj.transform.Find("IsClear").gameObject;
        isClear.SetActive(false);
        AppManager.GetInstance().missionInfo.SetClearMission(index, false);
    }

    public void ClearMission(int index)
    {
        if (_listString.Count < index)
            return;

        if (_missionInput[index].text == "")
            return;

        GameObject obj = _missionInput[index].transform.parent.gameObject;
        GameObject isClear = obj.transform.Find("IsClear").gameObject;
        isClear.SetActive(true);
        AppManager.GetInstance().missionInfo.SetClearMission(index, true);
    }

    //

    bool PreSettingPage(int sel)
    {
        switch ((PAGE_TYPE)_selNum)
        {
            case PAGE_TYPE.LOGIN:
                {
                    if (!DataManager.GetInstance().LoginStudent(_id_pwInput))
                    {
                        ShowMessageBox("ID 또는 비밀번호가 일치하지 않습니다.");
                        return false;
                    }

                    if (!DataManager.GetInstance().LoadData())
                    {
                        ShowMessageBox("데이터 로딩에 실패하였습니다.");
                        return false;
                    }

                    SetStudentInfo();
                }
                break;
            case PAGE_TYPE.PAPS:
                {
                    if(DataManager.GetInstance().studentInfo.schoolGrade == "초등학교" && DataManager.GetInstance().studentInfo.grade < 4)
                    {
                        ShowMessageBox("PAPS 조회는 초등학교 4학년부터 가능합니다.");
                        return false;
                    }
                }
                break;
            case PAGE_TYPE.CARDI_ENDU:
                AppManager.GetInstance().SetCardiovascularEnduranceInfo(_listString);
                break;
            case PAGE_TYPE.AGILITY:
                AppManager.GetInstance().SetAgilityInfo(_listString);
                break;
            case PAGE_TYPE.MUSC_ENDU:
                AppManager.GetInstance().SetMuscularEnduranceInfo(_listString);
                break;
            case PAGE_TYPE.FLEXIBILITY:
                AppManager.GetInstance().SetFlexibilityInfo(_listString);
                break;
            case PAGE_TYPE.BMI:
                AppManager.GetInstance().SetBMIInfo(_listString);
                break;
            case PAGE_TYPE.MY_MISSION:
                AppManager.GetInstance().SetMissionInfo(_listString);
                break;
            case PAGE_TYPE.RECORD_CARDI:
                {
                    switch(_selRecordType)
                    {
                        case RECORD_TYPE.CARDI:
                            {
                                if(DataManager.GetInstance().cardiRecordList.Count == 0)
                                {
                                    ShowMessageBox("심폐지구력 측정 기록이 없습니다.");
                                    return false;
                                }
                            }
                            break;
                        case RECORD_TYPE.AGILE:
                            {
                                if (DataManager.GetInstance().agileRecordList.Count == 0)
                                {
                                    ShowMessageBox("순발력 측정 기록이 없습니다.");
                                    return false;
                                }
                            }
                            break;
                        case RECORD_TYPE.MUSC:
                            {
                                if (DataManager.GetInstance().muscRecordList.Count == 0)
                                {
                                    ShowMessageBox("근력근지구력 측정 기록이 없습니다.");
                                    return false;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                break;
            default:
                break;
        }

        return true;
    }

    void AfterSettingPage()
    {
        switch ((PAGE_TYPE)_selNum)
        {
            case PAGE_TYPE.PAPS:
                PAPSUISetting();
                break;
            case PAGE_TYPE.BASE_INFORM:
                _listString = AppManager.GetInstance().userInfo.GetInfo();
                break;
            case PAGE_TYPE.CARDI_ENDU:
                _listString = AppManager.GetInstance().papsInfo._cardiovascularEndurance.GetInfo();
                break;
            case PAGE_TYPE.AGILITY:
                _listString = AppManager.GetInstance().papsInfo._agility.GetInfo();
                break;
            case PAGE_TYPE.MUSC_ENDU:
                _listString = AppManager.GetInstance().papsInfo._muscularEndurance.GetInfo();
                break;
            case PAGE_TYPE.FLEXIBILITY:
                _listString = AppManager.GetInstance().papsInfo._flexibility.GetInfo();
                break;
            case PAGE_TYPE.BMI:
                _listString = AppManager.GetInstance().papsInfo._BMI.GetInfo();
                break;
            case PAGE_TYPE.MY_MISSION:
                {
                    _listString = AppManager.GetInstance().missionInfo.GetInfo();
                    SetMissionUI();
                    SetSchoolMission();
                }
                break;
            case PAGE_TYPE.PAPS_RESULT:
                PAPSResultUISetting();
                break;
            case PAGE_TYPE.RECORD_CARDI:
                CreateMeterButton();
                break;
            case PAGE_TYPE.RECORD_CARDI_DATE:
                //CreateDateButton();
                break;
            case PAGE_TYPE.RECORD_CARDI_BAR_GRAPH:
                //SetBarGraphData();
                break;
            default:
                break;
        }
    }

    void PAPSUISetting()
    {
        StudentInfo studentInfo = DataManager.GetInstance().studentInfo;
        GameObject obj = GameObject.Find("UserInform");
        obj.GetComponent<Text>().text = studentInfo.schoolName + " " +studentInfo.schoolGrade + " " + studentInfo.grade + "학년 " 
            + studentInfo.classNum + "반 " + studentInfo.number + "번 " + studentInfo.name + "(" + studentInfo.gender + ")";
    }

    void PAPSResultUISetting()
    {
        GameObject cardiGrade = GameObject.Find("CardiGrade");
        GameObject agilityGrade = GameObject.Find("AgilityGrade");
        GameObject musGrade = GameObject.Find("MusGrade");
        GameObject flexibilityGrade = GameObject.Find("FlexibilityGrade");
        GameObject bmiGrade = GameObject.Find("BMIGrade");

        cardiGrade.GetComponent<Text>().text = _strPAPSGrade[(int)Grade.GetCardiGrade() - 1];
        agilityGrade.GetComponent<Text>().text = _strPAPSGrade[(int)Grade.GetAgilityGrade() - 1];
        musGrade.GetComponent<Text>().text = _strPAPSGrade[(int)Grade.GetMusGrade() - 1];
        flexibilityGrade.GetComponent<Text>().text = _strPAPSGrade[(int)Grade.GetFlexibilityGrade() - 1];
        bmiGrade.GetComponent<Text>().text = _strBMIGrade[(int)Grade.GetBMIGrade() - 1];
    }

    // MessageBox
    public void ShowMessageBox(string str)
    {
        GameObject obj = GameObject.Find("Canvas").transform.Find("MessageBox").gameObject;
        obj.SetActive(true);
        GameObject message = GameObject.Find("Message");
        message.GetComponent<Text>().text = str;
    }

    public void ExitMessageBox()
    {
        GameObject obj = GameObject.Find("Canvas").transform.Find("MessageBox").gameObject;
        obj.SetActive(false);
    }
    //

    void BackPage()     // 0 - 1 - 2, 3, 10 / 3 - 4, 5, 6, 7, 8, 9 / 10 - 11, 12, 13, 14
    {
        switch ((PAGE_TYPE)_selNum)
        {
            case PAGE_TYPE.LOGIN:
            case PAGE_TYPE.MAIN:
                Application.Quit();
                break;
            case PAGE_TYPE.BASE_INFORM:
                OnClickStartBtn((int)PAGE_TYPE.MAIN);
                break;
            case PAGE_TYPE.PAPS:
                OnClickStartBtn((int)PAGE_TYPE.MAIN);
                break;
            case PAGE_TYPE.CARDI_ENDU:
                OnClickStartBtn((int)PAGE_TYPE.PAPS);
                break;
            case PAGE_TYPE.AGILITY:
                OnClickStartBtn((int)PAGE_TYPE.PAPS);
                break;
            case PAGE_TYPE.MUSC_ENDU:
                OnClickStartBtn((int)PAGE_TYPE.PAPS);
                break;
            case PAGE_TYPE.FLEXIBILITY:
                OnClickStartBtn((int)PAGE_TYPE.PAPS);
                break;
            case PAGE_TYPE.BMI:
                OnClickStartBtn((int)PAGE_TYPE.PAPS);
                break;
            case PAGE_TYPE.PAPS_RESULT:
                OnClickStartBtn((int)PAGE_TYPE.PAPS);
                break;
            case PAGE_TYPE.FITNESS_UP_TIP:
                OnClickStartBtn((int)PAGE_TYPE.MAIN);
                break;
            case PAGE_TYPE.MY_RECORD:
                OnClickStartBtn((int)PAGE_TYPE.MAIN);
                break;
            case PAGE_TYPE.MY_MISSION:
                OnClickStartBtn((int)PAGE_TYPE.MAIN);
                break;
            case PAGE_TYPE.RECORD_CARDI:
                OnClickStartBtn((int)PAGE_TYPE.MY_RECORD);
                break;
            case PAGE_TYPE.RECORD_CARDI_DATE:
                {
                    if(_selRecordType == RECORD_TYPE.MUSC)
                        OnClickStartBtn((int)PAGE_TYPE.MY_RECORD);
                    else
                        OnClickStartBtn((int)PAGE_TYPE.RECORD_CARDI);
                }
                break;
            case PAGE_TYPE.RECORD_CARDI_BAR_GRAPH:
                OnClickStartBtn((int)PAGE_TYPE.RECORD_CARDI_DATE);
                break;
            case PAGE_TYPE.RECORD_CARDI_LINE_GRAPH:
                OnClickStartBtn((int)PAGE_TYPE.RECORD_CARDI_DATE);
                break;
            case PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB:
                OnClickStartBtn((int)PAGE_TYPE.RECORD_CARDI_DATE);
                break;
            case PAGE_TYPE.RECORD_AGILE_MUSC_BAR_GRAPH:
                OnClickStartBtn((int)PAGE_TYPE.RECORD_CARDI);   // 나중에 수정
                break;
            default:
                Debug.Log("Invalid PAGE_TYPE");
                break;
        }
    }

    void SetStudentInfo()
    {
        _studentInput[(int)STUDENT_INFO_TEXT.SCHOOLNAME].text = DataManager.GetInstance().studentInfo.schoolName;
        _studentInput[(int)STUDENT_INFO_TEXT.SCHOOLGRADE].text = DataManager.GetInstance().studentInfo.schoolGrade;
        _studentInput[(int)STUDENT_INFO_TEXT.GRADE].text = DataManager.GetInstance().studentInfo.grade.ToString();
        _studentInput[(int)STUDENT_INFO_TEXT.CLASS].text = DataManager.GetInstance().studentInfo.classNum.ToString();
        _studentInput[(int)STUDENT_INFO_TEXT.NUMBER].text = DataManager.GetInstance().studentInfo.number.ToString();
        _studentInput[(int)STUDENT_INFO_TEXT.NAME].text = DataManager.GetInstance().studentInfo.name;
        _studentInput[(int)STUDENT_INFO_TEXT.GENDER].text = DataManager.GetInstance().studentInfo.gender;
    }

    private List<Button> _dateButtonList;
    void CreateCardiDateButton(int count, int meter)
    {
        if (_selRecordType != RECORD_TYPE.CARDI)
            return;

        foreach (var btn in _dateButtonList)
        {
            btn.onClick.RemoveAllListeners();
            Destroy(btn.gameObject);
        }
        _dateButtonList.Clear();

        GameObject titleObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_DATE].transform.Find("LittleTitle").gameObject;
        Text titleTxt = titleObj.GetComponent<Text>();
        titleTxt.text = "심폐지구력";
        GameObject meterObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_DATE].transform.Find("MeterTitle").gameObject;
        Text meterTxt = meterObj.GetComponent<Text>();
        meterTxt.text = count.ToString() + "바퀴, 총 " + meter.ToString() + "m";

        List<CardiRecord> list = DataManager.GetInstance().cardiRecordList;
        for (int i = 0; i < list.Count; ++i)
        {
            if (list[i].totalTrackCount != count || list[i].totalMeter != meter)
                continue;

            Button button = Instantiate(_dateButton, _dateButton.transform);
            Text dateText = button.GetComponentInChildren<Text>();
            dateText.text = System.Convert.ToDateTime(list[i].dateTime).ToString("yyyy-MM-dd")+ " " + list[i].dateTime.Hour + "시 : " + list[i].totalElapsedTime;
            button.transform.SetParent(_dateContent.transform);
            button.gameObject.SetActive(true);
            int cardiRecordUnique = (int)list[i].recordUnique;
            int totalElapsedTime = list[i].totalElapsedTime;
            button.onClick.AddListener(
                () =>
                {
                    if (!DataManager.GetInstance().GetTrackRecord(cardiRecordUnique))
                        return;

                    if (!DataManager.GetInstance().GetCardiAvgRecord(meter, count))
                        return;

                    if (!DataManager.GetInstance().GetCardiNorDistRecord(cardiRecordUnique, meter, count, totalElapsedTime))
                        return;

                    GameObject tObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH].transform.Find("LittleTitle").gameObject;
                    Text tTxt = tObj.GetComponent<Text>();
                    tTxt.text = titleTxt.text;
                    GameObject mObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH].transform.Find("MeterTitle").gameObject;
                    Text mTxt = mObj.GetComponent<Text>();
                    mTxt.text = meterTxt.text;
                    GameObject dObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH].transform.Find("DateTitle").gameObject;
                    Text dTxt = dObj.GetComponent<Text>();
                    dTxt.text = dateText.text;

                    tObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_LINE_GRAPH].transform.Find("LittleTitle").gameObject;
                    tTxt = tObj.GetComponent<Text>();
                    tTxt.text = titleTxt.text;
                    mObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_LINE_GRAPH].transform.Find("MeterTitle").gameObject;
                    mTxt = mObj.GetComponent<Text>();
                    mTxt.text = meterTxt.text;
                    dObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_LINE_GRAPH].transform.Find("DateTitle").gameObject;
                    dTxt = dObj.GetComponent<Text>();
                    dTxt.text = dateText.text;

                    tObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("LittleTitle").gameObject;
                    tTxt = tObj.GetComponent<Text>();
                    tTxt.text = titleTxt.text;
                    mObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("MeterTitle").gameObject;
                    mTxt = mObj.GetComponent<Text>();
                    mTxt.text = meterTxt.text;
                    dObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("DateTitle").gameObject;
                    dTxt = dObj.GetComponent<Text>();
                    dTxt.text = dateText.text;

                    GameObject percentObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("PercentileText").gameObject;
                    Text t = percentObj.GetComponent<Text>();
                    t.text = "나의 기록은 상위 " + DataManager.GetInstance().normalDistMyPercent + "%입니다";

                    ChartManager obj = FindObjectOfType<ChartManager>();
                    obj.SetCardiTrackRecordBarAndLineGraph();
                });
            _dateButtonList.Add(button);
        }
    }

    void CreateAgileDateButton(int meter)
    {
        if (_selRecordType != RECORD_TYPE.AGILE)
            return;

        foreach (Button btn in _dateButtonList)
        {
            btn.onClick.RemoveAllListeners();
            Destroy(btn.gameObject);
        }
        _dateButtonList.Clear();

        GameObject titleObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_DATE].transform.Find("LittleTitle").gameObject;
        Text titleTxt = titleObj.GetComponent<Text>();
        titleTxt.text = "순발력";
        GameObject meterObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_DATE].transform.Find("MeterTitle").gameObject;
        Text meterTxt = meterObj.GetComponent<Text>();
        meterTxt.text = meter.ToString() + "m";

        List<AgileRecord> list = DataManager.GetInstance().agileRecordList;
        for (int i = 0; i < list.Count; ++i)
        {
            if (list[i].meter != meter)
                continue;

            Button button = Instantiate(_dateButton, _dateButton.transform);
            Text dateText = button.GetComponentInChildren<Text>();
            dateText.text = System.Convert.ToDateTime(list[i].dateTime).ToString("yyyy-MM-dd") + " " + list[i].dateTime.Hour + "시 : " + list[i].elapsedTime;
            button.transform.SetParent(_dateContent.transform);
            button.gameObject.SetActive(true);
            int agileRecordUnique = (int)list[i].recordUnique;
            int elapsedTime = list[i].elapsedTime;
            button.onClick.AddListener(
                () =>
                {
                    if (!DataManager.GetInstance().GetAgileAvgRecord(meter))
                        return;

                    if (!DataManager.GetInstance().GetAgileNorDistRecord(agileRecordUnique, meter, elapsedTime))
                        return;

                    GameObject tObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH].transform.Find("LittleTitle").gameObject;
                    Text tTxt = tObj.GetComponent<Text>();
                    tTxt.text = titleTxt.text;
                    GameObject mObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH].transform.Find("MeterTitle").gameObject;
                    Text mTxt = mObj.GetComponent<Text>();
                    mTxt.text = meterTxt.text;
                    GameObject dObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH].transform.Find("DateTitle").gameObject;
                    Text dTxt = dObj.GetComponent<Text>();
                    dTxt.text = dateText.text;

                    tObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("LittleTitle").gameObject;
                    tTxt = tObj.GetComponent<Text>();
                    tTxt.text = titleTxt.text;
                    mObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("MeterTitle").gameObject;
                    mTxt = mObj.GetComponent<Text>();
                    mTxt.text = meterTxt.text;
                    dObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("DateTitle").gameObject;
                    dTxt = dObj.GetComponent<Text>();
                    dTxt.text = dateText.text;

                    GameObject percentObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("PercentileText").gameObject;
                    Text t = percentObj.GetComponent<Text>();
                    t.text = "나의 기록은 상위 " + DataManager.GetInstance().normalDistMyPercent + "%입니다";

                    ChartManager obj = FindObjectOfType<ChartManager>();
                    obj.SetAgileRecordBarGraph(elapsedTime);
                });
            _dateButtonList.Add(button);
        }
    }

    public void CreateMuscDateButton()
    {
        if (_selRecordType != RECORD_TYPE.MUSC)
            return;

        foreach (Button btn in _dateButtonList)
        {
            btn.onClick.RemoveAllListeners();
            Destroy(btn.gameObject);
        }
        _dateButtonList.Clear();

        GameObject titleObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_DATE].transform.Find("LittleTitle").gameObject;
        Text titleTxt = titleObj.GetComponent<Text>();
        titleTxt.text = "근력근지구력";
        GameObject meterObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_DATE].transform.Find("MeterTitle").gameObject;
        Text meterTxt = meterObj.GetComponent<Text>();
        meterTxt.text = "";

        List<MuscRecord> list = DataManager.GetInstance().muscRecordList;
        for (int i = 0; i < list.Count; ++i)
        {
            Button button = Instantiate(_dateButton, _dateButton.transform);
            Text dateText = button.GetComponentInChildren<Text>();
            dateText.text = System.Convert.ToDateTime(list[i].dateTime).ToString("yyyy-MM-dd") + " " + list[i].dateTime.Hour + "시 : " + list[i].count;
            button.transform.SetParent(_dateContent.transform);
            button.gameObject.SetActive(true);
            int muscRecordUnique = (int)list[i].recordUnique;
            int count = list[i].count;
            button.onClick.AddListener(
                () =>
                {
                    if (!DataManager.GetInstance().GetMuscAvgRecord())
                        return;

                    if (!DataManager.GetInstance().GetMuscNorDistRecord(muscRecordUnique, count))
                        return;

                    GameObject tObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH].transform.Find("LittleTitle").gameObject;
                    Text tTxt = tObj.GetComponent<Text>();
                    tTxt.text = titleTxt.text;
                    GameObject mObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH].transform.Find("MeterTitle").gameObject;
                    Text mTxt = mObj.GetComponent<Text>();
                    mTxt.text = meterTxt.text;
                    GameObject dObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH].transform.Find("DateTitle").gameObject;
                    Text dTxt = dObj.GetComponent<Text>();
                    dTxt.text = dateText.text;

                    tObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("LittleTitle").gameObject;
                    tTxt = tObj.GetComponent<Text>();
                    tTxt.text = titleTxt.text;
                    mObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("MeterTitle").gameObject;
                    mTxt = mObj.GetComponent<Text>();
                    mTxt.text = meterTxt.text;
                    dObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("DateTitle").gameObject;
                    dTxt = dObj.GetComponent<Text>();
                    dTxt.text = dateText.text;

                    GameObject percentObj = _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB].transform.Find("PercentileText").gameObject;
                    Text t = percentObj.GetComponent<Text>();
                    t.text = "나의 기록은 상위 " + DataManager.GetInstance().normalDistMyPercent + "%입니다";

                    ChartManager obj = FindObjectOfType<ChartManager>();
                    obj.SetMuscRecordBarGraph(count);
                });
            _dateButtonList.Add(button);
        }
    }

    private List<Button> _meterButtonList;
    void CreateMeterButton()
    {
        foreach(var btn in _meterButtonList)
        {
            btn.onClick.RemoveAllListeners();
            Destroy(btn.gameObject);
        }
        _meterButtonList.Clear();

        switch(_selRecordType)
        {
            case RECORD_TYPE.CARDI:
                {
                    GameObject obj = _obj[(int)PAGE_TYPE.RECORD_CARDI].transform.Find("LittleTitle").gameObject;
                    Text txt = obj.GetComponent<Text>();
                    txt.text = "심폐지구력";

                    Dictionary<Key, int> dic = new Dictionary<Key, int>();
                    List<CardiRecord> list = DataManager.GetInstance().cardiRecordList;
                    for (int i = 0; i < list.Count; ++i)
                    {
                        Key key = new Key(list[i].totalTrackCount, list[i].totalMeter);
                        if (!dic.ContainsKey(key))
                            dic.Add(key, list[i].totalMeter);
                    }

                    int count = 0;
                    foreach (KeyValuePair<Key, int> pair in dic)
                    {
                        Button button = Instantiate(_meterButton, _meterButton.transform);
                        Text text = button.GetComponentInChildren<Text>();
                        text.text = pair.Key.GetCount().ToString() + "바퀴, 총 " + pair.Key.GetSumMeter().ToString() + "m";
                        button.transform.SetParent(_meterContent.transform);
                        button.gameObject.SetActive(true);
                        button.onClick.AddListener(
                            () =>
                            {
                                CreateCardiDateButton(pair.Key.GetCount(), pair.Key.GetSumMeter());
                            });
                        _meterButtonList.Add(button);
                        ++count;
                    }
                }
                break;
            case RECORD_TYPE.AGILE:
                {
                    GameObject obj = _obj[(int)PAGE_TYPE.RECORD_CARDI].transform.Find("LittleTitle").gameObject;
                    Text txt = obj.GetComponent<Text>();
                    txt.text = "순발력";

                    List<AgileRecord> list = DataManager.GetInstance().agileRecordList;
                    Dictionary<int, int> dic = new Dictionary<int, int>();
                    for (int i = 0; i < list.Count; ++i)
                    {
                        if (!dic.ContainsKey(list[i].meter))
                            dic.Add(list[i].meter, i);
                    }

                    int count = 0;
                    foreach (KeyValuePair<int, int> pair in dic)
                    {
                        Button button = Instantiate(_meterButton, _meterButton.transform);
                        Text text = button.GetComponentInChildren<Text>();
                        text.text = pair.Key.ToString() + "m";
                        button.transform.SetParent(_meterContent.transform);
                        button.gameObject.SetActive(true);
                        button.onClick.AddListener(
                            () =>
                            {
                                CreateAgileDateButton(pair.Key);
                            });
                        _meterButtonList.Add(button);
                        ++count;
                    }
                }
                break;
            default:
                return;
        }
    }

    private List<GameObject> _schoolMissionObjList;
    void SetSchoolMission()
    {
        foreach (var obj in _schoolMissionObjList)
        {
            Destroy(obj);
        }
        _schoolMissionObjList.Clear();

        List<SchoolMission> schoolMission = DataManager.GetInstance().schoolMissionList;
        for(int i = 0; i < schoolMission.Count; ++i)
        {
            GameObject obj = Instantiate(_schoolMissionObj, _schoolMissionObj.transform);
            Text text = obj.transform.Find("Text").gameObject.GetComponent<Text>();
            text.text = schoolMission[i].missionDesc;
            Button btn = obj.transform.Find("Button").gameObject.GetComponent<Button>();
            GameObject isClear = obj.transform.Find("IsClear").gameObject;
            int missionUnique = (int)schoolMission[i].missionUnique;
            if(DataManager.GetInstance().ExistFinMissionOfStudent(missionUnique))
            {
                btn.gameObject.SetActive(false);
                isClear.SetActive(true);
            }
            else
            {
                btn.onClick.AddListener(
                    () =>
                    {
                        DataManager.GetInstance().SetFinMissionOfStudent(missionUnique);
                        btn.gameObject.SetActive(false);
                        isClear.SetActive(true);
                    });
            }
            obj.transform.SetParent(_schoolMissionContent.transform);
            obj.gameObject.SetActive(true);
            _schoolMissionObjList.Add(obj);
        }
    }

    void SetMissionUI()
    {
        for(int i = 0; i < _MAX_MISSION; ++i)
        {
            if (!AppManager.GetInstance().missionInfo.GetClearMission(i))
                continue;

            GameObject obj = _missionInput[i].transform.parent.gameObject;
            GameObject isClear = obj.transform.Find("IsClear").gameObject;
            isClear.SetActive(true);
            AppManager.GetInstance().missionInfo.SetClearMission(i, true);
        }
    }
}
