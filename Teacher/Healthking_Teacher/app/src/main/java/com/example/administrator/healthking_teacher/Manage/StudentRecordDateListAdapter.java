package com.example.administrator.healthking_teacher.Manage;

import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.support.v7.app.AlertDialog;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.toolbox.Volley;
import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.Data.StudentRecordData;
import com.example.administrator.healthking_teacher.Network.UserAllDataDeleteRequest;
import com.example.administrator.healthking_teacher.R;

import org.json.JSONObject;

import java.text.SimpleDateFormat;
import java.util.List;

/**
 * Created by Forestwind on 2017-08-17.
 */

public class StudentRecordDateListAdapter extends BaseAdapter {
    private Context context;
    private StudentData studentData;
    private List<StudentRecordData> studentRecordDataList;


    public StudentRecordDateListAdapter(Context context, StudentData studentData, List<StudentRecordData> studentRecordDataList) {
        this.context = context;
        this.studentData =studentData;
        this.studentRecordDataList = studentRecordDataList;

    }

    @Override
    public int getCount() {
        return studentRecordDataList.size();
    }

    @Override
    public Object getItem(int position) {
        return studentRecordDataList.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(final int position, View convertView, ViewGroup parent) {

        View v = View.inflate(context, R.layout.student_record_date, null);

        TextView dateText = (TextView) v.findViewById(R.id.Student_Record_Date_DateText);
        dateText.setBackgroundColor(Color.rgb(255,255,255));

        final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy년 MM월 dd일 HH시");
        dateText.setText(dateFormat.format(studentRecordDataList.get(position).getRecordDate()));
        dateText.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(context, ManageDateInfoActivity.class);
                intent.putExtra("studentData", studentData);
                intent.putExtra("recordData", studentRecordDataList.get(position));
                context.startActivity(intent);
            }
        });


        v.setTag(studentRecordDataList.get(position).getId());

        Button deleteButton = (Button) v.findViewById(R.id.Student_Record_Date_DeleteButton);
        deleteButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //Todo : 기록 삭제 기능 추가
            }
        });
        return v;
    }


//    private Response.Listener<String> GetUserAllDataDeleteResponse(String userId) {
//        Response.Listener<String> responseListener = new Response.Listener<String>() {
//            @Override
//            public void onResponse(String response) {
//                try {
//                    JSONObject jsonResponse = new JSONObject(response);
//                    boolean success = jsonResponse.getBoolean("success");
//                    if (success) {
//
//                        AlertDialog dialog;
//                        AlertDialog.Builder builder = new AlertDialog.Builder(context);
//                        dialog = builder.setMessage("삭제가 완료되었습니다")
//                                .setPositiveButton("확인", null)
//                                .create();
//                        dialog.show();
//
//                    } else {
//                        AlertDialog dialog;
//                        AlertDialog.Builder builder = new AlertDialog.Builder(context);
//                        dialog = builder.setMessage("삭제가 실패하였습니다")
//                                .setNegativeButton("확인", null)
//                                .create();
//                        dialog.show();
//                    }
//
//                } catch (Exception e) {
//                    e.printStackTrace();
//                }
//            }
//        };
//
//        return responseListener;
//    }
}
