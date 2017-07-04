package com.example.administrator.healthking_teacher.Network;

import android.util.Log;

import com.android.volley.Response;
import com.android.volley.toolbox.StringRequest;
import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.Data.StudentRecordData;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by Administrator on 2017-07-02.
 */

public class RecordDataRequest extends StringRequest {

    final static private String URL = "http://came1230.cafe24.com/RecordDataAdd.php";
    private Map<String, String> parameters;

    public RecordDataRequest(StudentRecordData recordData, Response.Listener<String> listener) {
        super(Method.POST, URL, listener, null);

        parameters = new HashMap<>();
        DateFormat sdFormat = new SimpleDateFormat("yyyy-MM-dd-HH-mm-ss");
        DateFormat sdTimeFormat = new SimpleDateFormat("HH-mm-ss");

        parameters.put("userID", recordData.getId());
        parameters.put("recordDate", sdFormat.format(recordData.getRecordDate()));
        parameters.put("recordMeter", recordData.getRecordMeter() + "");
        parameters.put("trackCount", recordData.getTrackCount() + "");

        List<Date> trackTimeList =recordData.getTrackTimeDate();
        StringBuilder sb = new StringBuilder();

        for(int i=0; i<trackTimeList.size(); ++i)
        {
            sb.append(sdTimeFormat.format(trackTimeList.get(i)));
            sb.append(",");
        }
        parameters.put("trackTimeDate", sb.toString().trim());

        parameters.put("allTrackTimeDate", sdTimeFormat.format(recordData.getAllTrackTimeDate()));

    }


    @Override
    public Map<String, String> getParams() {
        return parameters;
    }


}