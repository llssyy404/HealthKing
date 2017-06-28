package com.example.administrator.healthking_teacher.Data;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by admin on 2017-06-29.
 */

public class StudentDataManager {

    private List<StudentData> dataList;


    static private StudentDataManager _instance;

    static public StudentDataManager getInstance() {
        if (_instance == null) {
            _instance = new StudentDataManager();
            _instance.Init();
        }
        return _instance;
    }


    private void Init() {
        dataList = new ArrayList<>();
    }

    public void SetDatas(String data) {

        dataList.clear();
        try {
            JSONObject jsonObject = new JSONObject(data);
            JSONArray jsonArray = jsonObject.getJSONArray("response");
            int count = 0;
            String userID, userPassword, userName, userGender, userSchool, userGrade, userClassroom;
            while (count < jsonArray.length()) {
                JSONObject object = jsonArray.getJSONObject(count);
                userID = object.getString("userID");
                userPassword = object.getString("userPassword");
                userName = object.getString("userName");
                userGender = object.getString("userGender");
                userSchool = object.getString("userSchool");
                userGrade = object.getString("userGrade");
                userClassroom = object.getString("userClassroom");
                StudentData studentData = new StudentData(userID, userPassword, userName, userGender, userSchool, userGrade, userClassroom);
                dataList.add(studentData);
                ++count;
            }

        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    public List<StudentData> getDataList() {
        return dataList;
    }
}
