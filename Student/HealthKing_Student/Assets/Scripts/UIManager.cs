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
    MAX_PAGE_TYPE
}

enum VIDEO_URL
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
    MAX_VIDEO_URL
}

public class UIManager : MonoBehaviour {

    private GameObject[] _obj = null;
    private int _selNum = 0;
    private int _prevSelNum = 0;
    private List<string> _listString;
    private string[] _strPAPSGrade;
    private string[] _strBMIGrade;
    private string _input = null;
    const int _MAX_GRADE_COUNT = 6;

    // Use this for initialization
    void Start () {
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

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackPage();
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

        int input = value + 4;  // 4학년부터라...
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
        switch((VIDEO_URL)sel)
        {
            case VIDEO_URL.CARDI_ENDU_1:    // 오래달리기 자세
                Application.OpenURL("https://youtu.be/dA5lJ4p1hL8");
                break;
            case VIDEO_URL.CARDI_ENDU_2:    // 오래달리기 호흡법
                Application.OpenURL("https://youtu.be/tlTLmCZ6GsQ");
                break;
            case VIDEO_URL.FLEXIBILITY:     // 유연성
                Application.OpenURL("https://youtu.be/mt_QKF-axdc");
                break;
            case VIDEO_URL.MUSC_ENDU_1:     // 팔굽혀펴기
                Application.OpenURL("https://youtu.be/jTG2Gqivtu0");
                break;
            case VIDEO_URL.MUSC_ENDU_2:     // 철봉
                Application.OpenURL("https://youtu.be/47SxMjjCr20");
                break;
            case VIDEO_URL.MUSC_ENDU_3:     // 런지
                Application.OpenURL("https://youtu.be/liO2ZbTrudI");
                break;
            case VIDEO_URL.MUSC_ENDU_4:     // 플랭크
                Application.OpenURL("https://youtu.be/5l9Jt_SEdD0");
                break;
            case VIDEO_URL.AGILITY_1:       // 제자리 높이뛰기
                Application.OpenURL("https://youtu.be/MOfFtp9Xbn4");
                break;
            case VIDEO_URL.AGILITY_2:       // 제자리 멀리뛰기
                Application.OpenURL("https://youtu.be/qZAD2a0AhjI");
                break;
            case VIDEO_URL.BMI_1:           // 플랭크
                Application.OpenURL("https://youtu.be/5l9Jt_SEdD0");
                break;
            case VIDEO_URL.BMI_2:           // 버핏
                Application.OpenURL("https://youtu.be/7rowIMNUW9s");
                break;
            case VIDEO_URL.WARMING_UP:      // 준비운동
                Application.OpenURL("https://youtu.be/Iybe05oOMGw");
                break;
            case VIDEO_URL.NOTICE_BOARD:      // 게시판
                Application.OpenURL("https://cafe.naver.com/redu5tc5");
                break;
            default:
                Debug.Log("Invalid VIDEO_URL TYPE");
                break;
        }
    }

    public void OnClickStartBtn(int sel)
    {
        if (_selNum == sel)
            return;

        if (_obj[_prevSelNum] == null || _obj[_selNum] == null)
            return;

        if (PreSettingPage(sel) == false)
            return;

        _prevSelNum = _selNum;
        _selNum = sel;
        _obj[_prevSelNum].SetActive(false);
        _obj[_selNum].SetActive(true);
        _listString.Clear();

        AfterSettingPage();
    }
    //

    bool PreSettingPage(int sel)
    {
        if (sel == (int)PAGE_TYPE.PAPS && AppManager.GetInstance().userInfo.IsInitInfo() == false)
        {
            ShowMessageBox("기본정보 입력 후 사용가능합니다.");
            return false;
        }

        switch ((PAGE_TYPE)_selNum)
        {
            case PAGE_TYPE.BASE_INFORM:
                {
                    if (false == AppManager.GetInstance().SetUserInfo(_listString))
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
            case PAGE_TYPE.PAPS_RESULT:
                PAPSResultUISetting();
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
            default:
                Debug.Log("Invalid PAGE_TYPE");
                break;
        }
    }
}
