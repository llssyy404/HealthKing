package com.example.administrator.healthking_teacher.Data;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by admin on 2017-06-29.
 */

public class DataManager {

    private List<StudentData> studentDataList;
    private List<StudentRecordData> sendStudentRecordDataList; // 서버에 저장할 레코드 데이터 리스트
    private List<StudentRecordData> studentRecordDataList; // 서버에서 가져온 모든 레코드 데이터 리스트

    static private DataManager _instance;

    static public DataManager getInstance() {
        if (_instance == null) {
            _instance = new DataManager();
            _instance.Init();
        }
        return _instance;
    }


    private void Init() {
        studentDataList = new ArrayList<>();
        sendStudentRecordDataList = new ArrayList<>();
        studentRecordDataList = new ArrayList<>();
    }

    public void SetStudentDatas(String data) {

        studentDataList.clear();
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
                studentDataList.add(studentData);
                ++count;
            }

        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    public List<StudentData> getStudentDataList() {
        return studentDataList;
    }



    // 서버에 전송할 학생 레코드 데이터
    public void setSendStudentRecodeDatas(List<StudentRecordData> datas) {
        sendStudentRecordDataList = datas;
    }

    public List<StudentRecordData> getSendStudentRecodeDatas() {
        return sendStudentRecordDataList;
    }




}
