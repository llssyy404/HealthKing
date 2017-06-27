package com.example.administrator.healthking_teacher.Network;

import com.android.volley.Response;
import com.android.volley.toolbox.StringRequest;
import com.example.administrator.healthking_teacher.Data.StudentData;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by admin on 2017-06-28.
 */

public class RegisterRequest extends StringRequest {

    final static private String URL = "http://came1230.cafe24.com/UserRegister.php";
    private Map<String, String> parameters;

    public RegisterRequest(StudentData studentData, Response.Listener<String> listener) {
        super(Method.POST, URL, listener, null);

        parameters = new HashMap<>();
        parameters.put("userID", studentData.getId());
        parameters.put("userPassword", studentData.getPassword());
        parameters.put("userName", studentData.getName());
        parameters.put("userGender", studentData.getGender());
        parameters.put("userSchool", studentData.getSchool());
        parameters.put("userGrade", studentData.getGrade());
        parameters.put("userClassroom", studentData.getClassroomNumber());
    }



    @Override
    public Map<String, String> getParams() {
        return parameters;
    }


}
