package com.example.administrator.healthking_teacher.Data;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
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



    // 학생 정보 getter, setter

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



    // 서버에서 가져온 모든 학생 기록 데이터

    public void SetStudentRecordDatas(String data) {
        studentRecordDataList.clear();

        try {
            JSONObject jsonObject = new JSONObject(data);
            JSONArray jsonArray = jsonObject.getJSONArray("response");
            int count = 0;
            String userID;
            Date recordDate;
            int recordMeter ,trackCount;
            List<Date> trackTimeDate ;
            Date allTrackTimeDate;
            DateFormat sdFormat = new SimpleDateFormat("yyyy-MM-dd-HH-mm-ss");
            DateFormat sdTimeFormat = new SimpleDateFormat("HH-mm-ss");

            while (count < jsonArray.length()) {
                JSONObject object = jsonArray.getJSONObject(count);
                userID = object.getString("userID");
                recordDate = sdFormat.parse(object.getString("recordDate"));
                recordMeter = object.getInt("recordMeter");
                trackCount = object.getInt("trackCount");

                String[] trackTimeDateString = object.getString("trackTimeDate").split(",");

                trackTimeDate= new ArrayList<>();
                for(int i=0; i<trackTimeDateString.length; ++i)
                {
                    trackTimeDate.add(sdTimeFormat.parse(trackTimeDateString[i]));
                }

                allTrackTimeDate = sdTimeFormat.parse(object.getString("allTrackTimeDate"));

                StudentRecordData studentRecordData = new StudentRecordData(userID,recordDate,recordMeter,trackCount,trackTimeDate,allTrackTimeDate);
                studentRecordDataList.add(studentRecordData);
                ++count;
            }

            Collections.sort(studentRecordDataList, new StudentRecordDataComparator());

        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    public List<StudentRecordData> getStudentRecodeDatas() {
        return studentRecordDataList;
    }

}
