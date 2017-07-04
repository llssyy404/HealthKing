package com.example.administrator.healthking_teacher.Data;

import java.util.Calendar;
import java.util.Comparator;

/**
 * Created by admin on 2017-07-05.
 */

public class StudentRecordDataComparator implements Comparator<StudentRecordData> {

    @Override
    public int compare(StudentRecordData o1, StudentRecordData o2) {

        Calendar firstCal = Calendar.getInstance();
        Calendar secondCal = Calendar.getInstance();
        firstCal.setTime(o1.getRecordDate());
        secondCal.setTime(o2.getRecordDate());

        if(firstCal.getTimeInMillis() > secondCal.getTimeInMillis())
            return 1;
        else if(firstCal.getTimeInMillis() < secondCal.getTimeInMillis())
            return -1;
        else
            return 0;
    }
}
