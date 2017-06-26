using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if(_prevSelNum == 2)
        {
            AppManager.GetInstance().SetUserInfo(_listString);
        }
        _listString.Clear();
    }
}
