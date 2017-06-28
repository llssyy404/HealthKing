package com.example.administrator.healthking_teacher.Network;

import com.android.volley.Response;
import com.android.volley.toolbox.StringRequest;
import com.example.administrator.healthking_teacher.Data.StudentData;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by admin on 2017-06-28.
 */

public class ValidateRequest extends StringRequest {

    final static private String URL = "http://came1230.cafe24.com/UserValidate.php";
    private Map<String, String> parameters;

    public ValidateRequest(String userID, Response.Listener<String> listener) {
        super(Method.POST, URL, listener, null);

        parameters = new HashMap<>();
        parameters.put("userID", userID);

    }



    @Override
    public Map<String, String> getParams() {
        return parameters;
    }


}
