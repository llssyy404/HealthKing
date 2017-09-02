using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    FITNESS_UP_TIP_C,
    FITNESS_UP_TIP_M,
    FITNESS_UP_TIP_A,
    FITNESS_UP_TIP_B,
    MY_RECORD,
    MY_MISSION,
    RECORD_CARDI,
    RECORD_CARDI_DATE,
    RECORD_CARDI_BAR_GRAPH,
    RECORD_CARDI_LINE_GRAPH,
    RECORD_CARDI_NORMAL_DISTRIB,
    MAX_PAGE_TYPE
}

enum URL_TYPE
{
    CARDI_ENDU_1,
    CARDI_ENDU_2,
    FLEXIBILITY,
    MUSC_ENDU_1,
    MUSC_ENDU_2,
    MUSC_ENDU_3,
    MUSC_ENDU_4,
    AGILITY_1,
    AGILITY_2,
    BMI_1,
    BMI_2,
    WARMING_UP,
    NOTICE_BOARD,
    MAX_URL_TYPE
}

public class UIManager : MonoBehaviour {

    public List<InputField> _userInput;
    public Dropdown _dropdownSchGrade;
    public Toggle _toggleBoy;
    public Toggle _toggleGirl;
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

    private GameObject[] _obj = null;
    private GameObject[] _missionObj = null;
    private int _selNum = 0;
    private int _prevSelNum = 0;
    private int _curMissionCount = 0;
    private List<string> _listString;
    private string[] _strPAPSGrade;
    private string[] _strBMIGrade;
    private string _input = null;
    const int _MAX_GRADE_COUNT = 6;
    const int _SCH_GRADE_VALUE = 4;
    const int _MAX_MISSION = 4;

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
        _obj[(int)PAGE_TYPE.FITNESS_UP_TIP_C] = GameObject.Find("Canvas").transform.Find("FitnessUpTip_Cardi").gameObject;
        _obj[(int)PAGE_TYPE.FITNESS_UP_TIP_M] = GameObject.Find("Canvas").transform.Find("FitnessUpTip_Mus").gameObject;
        _obj[(int)PAGE_TYPE.FITNESS_UP_TIP_A] = GameObject.Find("Canvas").transform.Find("FitnessUpTip_Agile").gameObject;
        _obj[(int)PAGE_TYPE.FITNESS_UP_TIP_B] = GameObject.Find("Canvas").transform.Find("FitnessUpTip_BMI").gameObject;
        _obj[(int)PAGE_TYPE.MY_RECORD] = GameObject.Find("Canvas").transform.Find("MyRecord").gameObject;
        _obj[(int)PAGE_TYPE.MY_MISSION] = GameObject.Find("Canvas").transform.Find("MyMission").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_CARDI] = GameObject.Find("Canvas").transform.Find("Record_Cardi").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_CARDI_DATE] = GameObject.Find("Canvas").transform.Find("Record_Cardi_Date").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH] = GameObject.Find("Canvas").transform.Find("Record_Cardi_BarGraph").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_CARDI_LINE_GRAPH] = GameObject.Find("Canvas").transform.Find("Record_Cardi_LineGraph").gameObject;
        _obj[(int)PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB] = GameObject.Find("Canvas").transform.Find("Record_Cardi_Normal_Distribution").gameObject;

        _missionObj = new GameObject[_MAX_MISSION];
        _missionObj[0] = GameObject.Find("Mission1");
        _missionObj[1] = GameObject.Find("Mission2");
        _missionObj[2] = GameObject.Find("Mission3");
        _missionObj[3] = GameObject.Find("Mission4");
        Debug.Log("미션초기화");
        _dateButtonList = new List<Button>();
        _meterButtonList = new List<Button>();
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
        InitUserInput();
        InitCardiInput();
        InitAgileInput();
        InitMuscInput();
        InitFlexibilityInput();
        InitBMIInput();
        InitMissionInput();
    }

    void InitUserInput()
    {
        _listString = AppManager.GetInstance().userInfo.GetInfo();
        if (PlayerPrefs.HasKey("User_SchoolName"))
            _userInput[0].text = _listString[0] = PlayerPrefs.GetString("User_SchoolName");
        if (PlayerPrefs.HasKey("User_SchoolGrade"))
        {
            _dropdownSchGrade.value = System.Convert.ToInt32(PlayerPrefs.GetString("User_SchoolGrade").Trim()) - _SCH_GRADE_VALUE;
            _listString[1] = PlayerPrefs.GetString("User_SchoolGrade");
        }
        if (PlayerPrefs.HasKey("User_ClassNum"))
            _userInput[1].text = _listString[2] = PlayerPrefs.GetString("User_ClassNum");
        if (PlayerPrefs.HasKey("User_Number"))
            _userInput[2].text = _listString[3] = PlayerPrefs.GetString("User_Number");
        if (PlayerPrefs.HasKey("User_Name"))
            _userInput[3].text = _listString[4] = PlayerPrefs.GetString("User_Name");
        if (PlayerPrefs.HasKey("User_Gender"))
        {
            if (PlayerPrefs.GetString("User_Gender") == "0")
            {
                _toggleBoy.isOn = true;
                _toggleGirl.isOn = false;
            }
            else
            {
                _toggleBoy.isOn = false;
                _toggleGirl.isOn = true;
            }

            _listString[5] = PlayerPrefs.GetString("User_Gender");
        }
        AppManager.GetInstance().SetUserInfo(_listString);
        _listString.Clear();
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
            case URL_TYPE.CARDI_ENDU_1:    // 오래달리기 자세
                Application.OpenURL("https://youtu.be/dA5lJ4p1hL8");
                break;
            case URL_TYPE.CARDI_ENDU_2:    // 오래달리기 호흡법
                Application.OpenURL("https://youtu.be/tlTLmCZ6GsQ");
                break;
            case URL_TYPE.FLEXIBILITY:     // 유연성
                Application.OpenURL("https://youtu.be/mt_QKF-axdc");
                break;
            case URL_TYPE.MUSC_ENDU_1:     // 팔굽혀펴기
                Application.OpenURL("https://youtu.be/jTG2Gqivtu0");
                break;
            case URL_TYPE.MUSC_ENDU_2:     // 철봉
                Application.OpenURL("https://youtu.be/47SxMjjCr20");
                break;
            case URL_TYPE.MUSC_ENDU_3:     // 런지
                Application.OpenURL("https://youtu.be/liO2ZbTrudI");
                break;
            case URL_TYPE.MUSC_ENDU_4:     // 플랭크
                Application.OpenURL("https://youtu.be/5l9Jt_SEdD0");
                break;
            case URL_TYPE.AGILITY_1:       // 제자리 높이뛰기
                Application.OpenURL("https://youtu.be/MOfFtp9Xbn4");
                break;
            case URL_TYPE.AGILITY_2:       // 제자리 멀리뛰기
                Application.OpenURL("https://youtu.be/qZAD2a0AhjI");
                break;
            case URL_TYPE.BMI_1:           // 플랭크
                Application.OpenURL("https://youtu.be/5l9Jt_SEdD0");
                break;
            case URL_TYPE.BMI_2:           // 버핏
                Application.OpenURL("https://youtu.be/7rowIMNUW9s");
                break;
            case URL_TYPE.WARMING_UP:      // 준비운동
                Application.OpenURL("https://youtu.be/Iybe05oOMGw");
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

    public void OnClickMissionRegBtn()
    {
        if (_curMissionCount >= _MAX_MISSION)
            return;

        _missionObj[_curMissionCount].GetComponent<Text>().text = "뇽뇽";
        ++_curMissionCount;
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
                    DataManager.GetInstance().GetStudentRecordData();
                }
                break;
            case PAGE_TYPE.BASE_INFORM:
                {
                    if (!AppManager.GetInstance().SetUserInfo(_listString))
                    {
                        ShowMessageBox("모든 정보를 입력해주세요.");
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
                    Debug.Log("Get"+_listString.Count);
                }
                break;
            case PAGE_TYPE.PAPS_RESULT:
                PAPSResultUISetting();
                break;
            case PAGE_TYPE.RECORD_CARDI:
                CreateMeterButton();
                break;
            case PAGE_TYPE.RECORD_CARDI_DATE:
                CreateDateButton();
                break;
            default:
                break;
        }
    }

    void PAPSUISetting()
    {
        List<string> info = AppManager.GetInstance().userInfo.GetInfo();
        GameObject obj = GameObject.Find("UserInform");
        string gender = info[5] == "0" ? "남" : "여";
        obj.GetComponent<Text>().text = info[0] + "초등학교 " + info[1] + "학년 " + info[2] + "반 " + info[4] + "(" + gender + ")";
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
            case PAGE_TYPE.FITNESS_UP_TIP_C:
                OnClickStartBtn((int)PAGE_TYPE.FITNESS_UP_TIP);
                break;
            case PAGE_TYPE.FITNESS_UP_TIP_M:
                OnClickStartBtn((int)PAGE_TYPE.FITNESS_UP_TIP);
                break;
            case PAGE_TYPE.FITNESS_UP_TIP_A:
                OnClickStartBtn((int)PAGE_TYPE.FITNESS_UP_TIP);
                break;
            case PAGE_TYPE.FITNESS_UP_TIP_B:
                OnClickStartBtn((int)PAGE_TYPE.FITNESS_UP_TIP);
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
                OnClickStartBtn((int)PAGE_TYPE.RECORD_CARDI);
                break;
            case PAGE_TYPE.RECORD_CARDI_BAR_GRAPH:
                OnClickStartBtn((int)PAGE_TYPE.RECORD_CARDI_DATE);
                break;
            case PAGE_TYPE.RECORD_CARDI_LINE_GRAPH:
                OnClickStartBtn((int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH);
                break;
            case PAGE_TYPE.RECORD_CARDI_NORMAL_DISTRIB:
                OnClickStartBtn((int)PAGE_TYPE.RECORD_CARDI_BAR_GRAPH);
                break;
            default:
                Debug.Log("Invalid PAGE_TYPE");
                break;
        }
    }

    private List<Button> _dateButtonList;
    void CreateDateButton()
    {
        List<StudentRecordData> recordData = DataManager.GetInstance().GetStudentRecord();
        if (_dateButtonList.Count == recordData.Count)
            return;

        for (int i = 0; i < recordData.Count; ++i)
        {
            Button button = Instantiate(_dateButton, _dateButton.transform);
            Text text = button.GetComponentInChildren<Text>();
            text.text = recordData[i].GetRecordDate();
            button.transform.SetParent(_dateContent.transform);
            button.transform.Translate(new Vector3(0, (-200.0f*i)-200.0f));
            _dateButtonList.Add(button);
        }
    }

    private List<Button> _meterButtonList;
    void CreateMeterButton()
    {
        List<StudentRecordData> recordData = DataManager.GetInstance().GetStudentRecord();
        if (_meterButtonList.Count == recordData.Count)
            return;

        for (int i = 0; i < recordData.Count; ++i)
        {
            Button button = Instantiate(_meterButton, _meterButton.transform);
            Text text = button.GetComponentInChildren<Text>();
            text.text = recordData[i].GetRecordDate();
            button.transform.SetParent(_meterContent.transform);
            button.transform.Translate(new Vector3(0, (-200.0f * i) - 200.0f));
            _meterButtonList.Add(button);
        }
    }
}
