package com.example.administrator.healthking_teacher.Data;

import java.util.Date;
import java.util.List;

/**
 * Created by admin on 2017-06-30.
 */

public class StudentRecordData {

    private String id;      //유저정보
    private Date recordDate; // 기록한 날짜
    private int recordMeter; // 기록한 총 미터수
    private int trackCount; // 기록 총 바퀴수
    private List<Date> trackTimeData; // 바퀴마다 돈 시간
    private Date AllTrackTimeData; //총 바퀴시간

    public StudentRecordData(String id, Date recordDate, int recordMeter, int trackCount, List<Date> trackTimeData, Date allTrackTimeData) {
        this.id = id;
        this.recordDate = recordDate;
        this.recordMeter = recordMeter;
        this.trackCount = trackCount;
        this.trackTimeData = trackTimeData;
        AllTrackTimeData = allTrackTimeData;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public Date getRecordDate() {
        return recordDate;
    }

    public void setRecordDate(Date recordDate) {
        this.recordDate = recordDate;
    }

    public int getRecordMeter() {
        return recordMeter;
    }

    public void setRecordMeter(int recordMeter) {
        this.recordMeter = recordMeter;
    }

    public int getTrackCount() {
        return trackCount;
    }

    public void setTrackCount(int trackCount) {
        this.trackCount = trackCount;
    }

    public List<Date> getTrackTimeData() {
        return trackTimeData;
    }

    public void setTrackTimeData(List<Date> trackTimeData) {
        this.trackTimeData = trackTimeData;
    }

    public Date getAllTrackTimeData() {
        return AllTrackTimeData;
    }

    public void setAllTrackTimeData(Date allTrackTimeData) {
        AllTrackTimeData = allTrackTimeData;
    }
}
