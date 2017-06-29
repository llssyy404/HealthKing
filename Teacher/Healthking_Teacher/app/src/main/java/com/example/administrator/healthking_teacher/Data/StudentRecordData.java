package com.example.administrator.healthking_teacher.Data;

import java.util.Date;

/**
 * Created by admin on 2017-06-30.
 */

public class StudentRecordData {

    private String id;
    private Date date;
    private int runMeter;
    private Date timeRecord;

    public StudentRecordData(String id, Date date, int runMeter, Date timeRecord) {
        this.id = id;
        this.date = date;
        this.runMeter = runMeter;
        this.timeRecord = timeRecord;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public int getRunMeter() {
        return runMeter;
    }

    public void setRunMeter(int runMeter) {
        this.runMeter = runMeter;
    }

    public Date getTimeRecord() {
        return timeRecord;
    }

    public void setTimeRecord(Date timeRecord) {
        this.timeRecord = timeRecord;
    }
}
