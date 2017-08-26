package com.example.administrator.healthking_teacher.Data;

/**
 * Created by juicy on 2017-08-26.
 */

public class RecordDate { // year,month,date
    int year;
    int month;
    int date;

    public String getRecordDateToString(){
        return Integer.toString(this.year) +Integer.toString(this.month)+Integer.toString(this.date);
    }
}
