package com.example.administrator.healthking_teacher.Manage;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;

import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.R;

public class ManageDateInfoActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage_date_info);

        TextView textView = (TextView) findViewById(R.id.ManageDateInfo_stuText);
        Intent intent = getIntent();
        StudentData sD = (StudentData)intent.getExtras().get("stuText");
        textView.setText(sD.getGrade()+ sD.getClassroomNumber()+sD.getName()+ sD.getGender());

        TextView dateTextView = (TextView) findViewById(R.id.ManageDateInfo_dateText);
        dateTextView.setText("2017년 07월 04일");


    }
}
