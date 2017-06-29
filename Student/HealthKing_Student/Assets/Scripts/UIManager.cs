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
    MAX_PAGE_TYPE
}

enum VIDEO_URL
{
    CARDI_ENDU,
    AGILITY,
    MUSC_ENDU,
    FLEXIBILITY,
    BMI,
    WARMING_UP,
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

        _listString = new List<string>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEndEdit(string str)
    {
        _listString.Add(str);
        Debug.Log(str);
    }

    public void OnClickBtnPlayVideo(int sel)
    {
        switch((VIDEO_URL)sel)
        {
            case VIDEO_URL.CARDI_ENDU:
                Application.OpenURL("https://youtu.be/tlTLmCZ6GsQ");
                break;
            case VIDEO_URL.FLEXIBILITY:
                Application.OpenURL("https://youtu.be/mt_QKF-axdc");
                break;
            case VIDEO_URL.MUSC_ENDU:
                Application.OpenURL("https://youtu.be/jTG2Gqivtu0");
                break;
            case VIDEO_URL.AGILITY:
                Application.OpenURL("https://youtu.be/qZAD2a0AhjI");
                break;
            case VIDEO_URL.BMI:
                Application.OpenURL("https://youtu.be/5l9Jt_SEdD0");//
                break;
            case VIDEO_URL.WARMING_UP:
                Application.OpenURL("https://youtu.be/Iybe05oOMGw");
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

        _prevSelNum = _selNum;
        _selNum = sel;
        _obj[_prevSelNum].SetActive(false);
        _obj[_selNum].SetActive(true);

        switch((PAGE_TYPE)_prevSelNum)
        {
            case PAGE_TYPE.BASE_INFORM:
                AppManager.GetInstance().SetUserInfo(_listString);
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
        _listString.Clear();

        if(_selNum == (int)PAGE_TYPE.PAPS_RESULT)
        {
            GameObject cardiGrade = GameObject.Find("CardiGrade");
            GameObject agilityGrade = GameObject.Find("AgilityGrade");
            GameObject musGrade = GameObject.Find("MusGrade");
            GameObject flexibilityGrade = GameObject.Find("FlexibilityGrade");
            GameObject bmiGrade = GameObject.Find("BMIGrade");

            cardiGrade.GetComponent<Text>().text = Grade.GetCardiGrade().ToString();
            agilityGrade.GetComponent<Text>().text = Grade.GetAgilityGrade().ToString();
            musGrade.GetComponent<Text>().text = Grade.GetMusGrade().ToString();
            flexibilityGrade.GetComponent<Text>().text = Grade.GetFlexibilityGrade().ToString();
            bmiGrade.GetComponent<Text>().text = Grade.GetBMIGrade().ToString();
        }
    }
}
