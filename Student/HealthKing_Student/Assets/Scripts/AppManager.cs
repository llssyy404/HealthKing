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
    public UserInfo userInfo { get { return _userInfo; } set { _userInfo = userInfo; } }
    private PAPSInfo _papsInfo;
    public PAPSInfo papsInfo { get { return _papsInfo; } set { _papsInfo = papsInfo; } }
    private PAPSTableManager _papsTableManager;
    public PAPSTableManager papsTableManager { get { return _papsTableManager; } set { _papsTableManager = papsTableManager; } }

    private const int _maxCount = 6;
    private const int _maxCount1 = 3;
    private const int _maxCount2 = 2;
    private const int _maxCount3 = 3;
    private const int _maxCount4 = 1;
    private const int _maxCount5 = 2;

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        _userInfo = new UserInfo();
        _papsInfo = new PAPSInfo();
        _papsTableManager = new PAPSTableManager();
        _papsTableManager.ReadTableFile();
    }

    //
    public void SetUserInfo(List<string> listString)
    {
        if (listString.Count != _maxCount)
        {
            Debug.Log("정보를 입력해주세요");
            return;
        }

        _userInfo.InitUserInfo(listString);
    }

    public void SetCardiovascularEnduranceInfo(List<string> listString)
    {
        if(listString.Count != _maxCount1)
        {
            Debug.Log("listString.Count != _maxCount1" + listString.Count);
            return;
        }

        _papsInfo._cardiovascularEndurance.InitInfo(listString);
    }

    public void SetAgilityInfo(List<string> listString)
    {
        if (listString.Count != _maxCount2)
        {
            Debug.Log("listString.Count != _maxCount2" + listString.Count);
            return;
        }

        _papsInfo._agility.InitInfo(listString);
    }

    public void SetMuscularEnduranceInfo(List<string> listString)
    {
        if (listString.Count != _maxCount3)
        {
            Debug.Log("listString.Count != _maxCount3" + listString.Count);
            return;
        }

        _papsInfo._muscularEndurance.InitInfo(listString);
    }

    public void SetFlexibilityInfo(List<string> listString)
    {
        if (listString.Count != _maxCount4)
        {
            Debug.Log("listString.Count != _maxCount4" + listString.Count);
            return;
        }

        _papsInfo._flexibility.InitInfo(listString);
    }

    public void SetBMIInfo(List<string> listString)
    {
        if (listString.Count != _maxCount5)
        {
            Debug.Log("listString.Count != _maxCount5" + listString.Count);
            return;
        }

        _papsInfo._BMI.InitInfo(listString);
    }
    //

    // Update is called once per frame
    void Update () {
        if (Application.platform != RuntimePlatform.Android)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
