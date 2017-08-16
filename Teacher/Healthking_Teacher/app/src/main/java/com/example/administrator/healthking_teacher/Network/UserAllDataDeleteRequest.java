package com.example.administrator.healthking_teacher.Network;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.toolbox.StringRequest;
import com.example.administrator.healthking_teacher.Data.StudentData;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by Forestwind on 2017-08-17.
 */

public class UserAllDataDeleteRequest extends StringRequest {
    final static private String URL = "http://came1230.cafe24.com/UserAllDataDelete.php";
    private Map<String, String> parameters;

    public UserAllDataDeleteRequest(String userId, Response.Listener<String> listener) {
        super(Method.POST, URL, listener, null);
        parameters = new HashMap<>();
        parameters.put("userID", userId);

    }



    @Override
    public Map<String, String> getParams() {
        return parameters;
    }

}
