package com.example.administrator.healthking_teacher.Network;

import com.android.volley.Response;
import com.android.volley.toolbox.StringRequest;
import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.Data.StudentRecordData;

import java.util.HashMap;
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
        parameters.put("userID", recordData.getId());
        parameters.put("recordDate", recordData.getRecordDate().toString());
        parameters.put("recordMeter", recordData.getRecordMeter() + "");
        parameters.put("trackCount", recordData.getTrackCount() + "");
        parameters.put("trackTimeDate", recordData.getTrackTimeDate() + "");
        parameters.put("allTrackTimeDate", recordData.getAllTrackTimeDate().toString());

    }


    @Override
    public Map<String, String> getParams() {
        return parameters;
    }


}