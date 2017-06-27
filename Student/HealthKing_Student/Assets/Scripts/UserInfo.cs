using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo {
    private string _schoolName = null;
    private int _schoolGrade = 0;
    private int _classNum = 0;
    private int _number = 0;
    private string _name = null;
    private int _gender = 0;

    public bool InitUserInfo(List<string> listString)
    {
        //if (schoolName == null)
        //{
        //    Debug.Log("이름 안적었어");
        //    return false;
        //}

        //if (schoolGrade == 0)
        //{
        //    Debug.Log("학년 안적었어");
        //    return false;
        //}

        //if (classNum == 0)
        //{
        //    Debug.Log("반 안적었어");
        //    return false;
        //}

        //if (number == 0)
        //{
        //    Debug.Log("번호 안적었어");
        //    return false;
        //}

        //if (name == null)
        //{
        //    Debug.Log("이름 안적었어");
        //    return false;
        //}

        //if (gender == null)
        //{
        //    Debug.Log("성별 안적었어");
        //    return false;
        //}

        _schoolName = listString[0];
        _schoolGrade = System.Convert.ToInt32(listString[1].Trim());
        _classNum = System.Convert.ToInt32(listString[2].Trim());
        _number = System.Convert.ToInt32(listString[3].Trim());
        _name = listString[4];
        _gender = System.Convert.ToInt32(listString[5].Trim());

        return true;
    }

    public int GetGender() { return _gender; }
    public int GetSchoolGrade() { return _schoolGrade; } 
}
