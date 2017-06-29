using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;
    public static UIManager GetInstance()
    {
        return _instance;
    }

    private GameObject[] _obj = null;
    private int _selNum = 0;
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
        _obj = new GameObject[11];
        _obj[0] = GameObject.Find("Canvas").transform.Find("Login").gameObject;
        _obj[1] = GameObject.Find("Canvas").transform.Find("Main").gameObject;
        _obj[2] = GameObject.Find("Canvas").transform.Find("BaseInform").gameObject;
        _obj[3] = GameObject.Find("Canvas").transform.Find("PAPS").gameObject;
        _obj[4] = GameObject.Find("Canvas").transform.Find("CardiovascularEndurance").gameObject;
        _obj[5] = GameObject.Find("Canvas").transform.Find("Agility").gameObject;
        _obj[6] = GameObject.Find("Canvas").transform.Find("MuscularEndurance").gameObject;
        _obj[7] = GameObject.Find("Canvas").transform.Find("Flexibility").gameObject;
        _obj[8] = GameObject.Find("Canvas").transform.Find("BMI").gameObject;
        _obj[9] = GameObject.Find("Canvas").transform.Find("PAPSResult").gameObject;
        _obj[10] = GameObject.Find("Canvas").transform.Find("FitnessUpTip").gameObject;

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
        switch(sel)
        {
            case 0:
                Application.OpenURL("https://youtu.be/tlTLmCZ6GsQ");
                break;
            case 1:
                Application.OpenURL("https://youtu.be/mt_QKF-axdc");
                break;
            case 2:
                Application.OpenURL("https://youtu.be/jTG2Gqivtu0");
                break;
            case 3:
                Application.OpenURL("https://youtu.be/qZAD2a0AhjI");
                break;
            case 4:
                Application.OpenURL("https://youtu.be/5l9Jt_SEdD0");//
                break;
            case 5:
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

        switch(_prevSelNum)
        {
            case 2:
                AppManager.GetInstance().SetUserInfo(_listString);
                break;
            case 4:
                AppManager.GetInstance().SetCardiovascularEnduranceInfo(_listString);
                break;
            case 5:
                AppManager.GetInstance().SetAgilityInfo(_listString);
                break;
            case 6:
                AppManager.GetInstance().SetMuscularEnduranceInfo(_listString);
                break;
            case 7:
                AppManager.GetInstance().SetFlexibilityInfo(_listString);
                break;
            case 8:
                AppManager.GetInstance().SetBMIInfo(_listString);
                break;
            default:
                break;
        }
        _listString.Clear();

        if(_selNum == 9)
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
