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
    private PAPSTableManager _papsTableManager;

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        _userInfo = new UserInfo();
        _papsTableManager = new PAPSTableManager();
        _papsTableManager.ReadTableFile();
    }

    //
    public void SetUserInfo(List<string> listString)
    {
        if (listString.Count != 6)
        {
            Debug.Log("정보를 입력해주세요");
            return;
        }

        string schoolName = listString[0];
        int schoolGrade = System.Convert.ToInt32(listString[1].Trim());
        int classNum = System.Convert.ToInt32(listString[2].Trim());
        int number = System.Convert.ToInt32(listString[3].Trim());
        string name = listString[4];
        string gender = listString[5];

        _userInfo.InitUserInfo(schoolName, schoolGrade, classNum, number, name, gender);
    }
    //

    // Update is called once per frame
    void Update () {
		
	}
}
