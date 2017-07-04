package com.example.administrator.healthking_teacher.Manage;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.ArrayAdapter;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.Data.StudentRecordData;
import com.example.administrator.healthking_teacher.R;

import java.text.DateFormat;
import java.text.SimpleDateFormat;

public class ManageDateInfoActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage_date_info);

        final TextView textView = (TextView) findViewById(R.id.ManageDateInfo_stuText);
        final TextView recordDateTextView = (TextView) findViewById(R.id.ManageDateInfo_dateText);
        final TextView distTextView = (TextView) findViewById(R.id.ManageDateInfo_distanceText);
        final TextView timeTextView = (TextView) findViewById(R.id.ManageDateInfo_timeText);
        final ListView recordTimeListView = (ListView) findViewById(R.id.ManagerDateInfo_TimeListView);

        final StudentData currentStudentData = (StudentData) getIntent().getExtras().get("studentData");
        final StudentRecordData currentStudentRecordText = (StudentRecordData) getIntent().getExtras().get("recordData");
        final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy년 MM월 dd일 HH시");
        final SimpleDateFormat timeFormat = new SimpleDateFormat("HH시 mm분 ss초");

        textView.setText(currentStudentData.getGrade() + "  " + currentStudentData.getClassroomNumber() + "  " + currentStudentData.getName() + "  " + currentStudentData.getGender());
        recordDateTextView.setText(dateFormat.format(currentStudentRecordText.getRecordDate()));
        distTextView.setText(currentStudentRecordText.getRecordMeter() + "미터");
        timeTextView.setText(timeFormat.format(currentStudentRecordText.getAllTrackTimeDate()));

        String[] recordTimeTexts = new String[currentStudentRecordText.getTrackTimeDate().size()];
        for (int i = 0; i < currentStudentRecordText.getTrackTimeDate().size(); ++i) {
            recordTimeTexts[i] = i + 1 + "바퀴 :  " + timeFormat.format(currentStudentRecordText.getTrackTimeDate().get(i));
        }

        ListAdapter adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, recordTimeTexts);
        recordTimeListView.setAdapter(adapter);
    }
}
