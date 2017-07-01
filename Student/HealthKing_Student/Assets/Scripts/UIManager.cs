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

    private static UIManager _instance;
    public static UIManager GetInstance()
    {
        return _instance;
    }

    private GameObject[] _obj = null;
    private int _selNum = 0;
    public int selPageNum { get { return _selNum; } }
    private int _prevSelNum = 0;
    private List<string> _listString;
    private string[] _strPAPSGrade;
    private string[] _strBMIGrade;
    private string _input = null;
    //
    //private string[] _inputString;

    void Awake()
    {
        _instance = this;
    }

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
        _strPAPSGrade = new string[6];
        _strPAPSGrade[0] = "1등급";
        _strPAPSGrade[1] = "2등급";
        _strPAPSGrade[2] = "3등급";
        _strPAPSGrade[3] = "4등급";
        _strPAPSGrade[4] = "5등급";
        _strPAPSGrade[5] = "NONE";

        _strBMIGrade = new string[6];
        _strBMIGrade[0] = "마름";
        _strBMIGrade[1] = "정상";
        _strBMIGrade[2] = "과체중";
        _strBMIGrade[3] = "경도비만";
        _strBMIGrade[4] = "고도비만";
        _strBMIGrade[5] = "NONE";
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnEndEdit(string str)
    {
        //_listString.Add(str);
        _input = str;
        Debug.Log(str);
    }

    public void EditNum(int num)
    {
        _listString[num] = _input;
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
                Application.OpenURL("http://cafe.naver.com/unityhub");
                break;
            default:
                break;
        }
    }

    public void OnClickStartBtn(int sel)
    {
        if (_selNum == sel)
            return;

        if (_obj[_prevSelNum] == null || _obj[_selNum] == null)
            return;

        switch ((PAGE_TYPE)_selNum)
        {
            case PAGE_TYPE.BASE_INFORM:
                {
                    if (false == AppManager.GetInstance().SetUserInfo(_listString))
                        return;
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

        _prevSelNum = _selNum;
        _selNum = sel;
        _obj[_prevSelNum].SetActive(false);
        _obj[_selNum].SetActive(true);

        _listString.Clear();

        switch ((PAGE_TYPE)_selNum)
        {
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
                break;
            default:
                break;
        }
    }
}
