package com.example.administrator.healthking_teacher.Manage;


import android.content.Intent;
import android.os.Debug;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;


import com.example.administrator.healthking_teacher.Data.DataManager;
import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.Data.StudentRecordData;
import com.example.administrator.healthking_teacher.R;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ManageStudentInfoActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage_student_info);

        final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy년 MM월 dd일 HH시");
        final StudentData currentStudentData = (StudentData) getIntent().getExtras().get("studentData");
        final List<StudentRecordData> AllUserRecordDataList = DataManager.getInstance().getStudentRecodeDatas();
        final List<StudentRecordData> selectUserRecordDataList = new ArrayList<>();


        TextView textView = (TextView) findViewById(R.id.ManageStudentInfo_stuText);
        textView.setText(currentStudentData.getGrade() + " " + currentStudentData.getClassroomNumber() + " " + currentStudentData.getName() + " "  + currentStudentData.getGender());

        // 아이디체크로 record 데이터 검출
        for (int i = 0; i < AllUserRecordDataList.size(); ++i) {
            if (AllUserRecordDataList.get(i).getId().equals(currentStudentData.getId())) {
                selectUserRecordDataList.add(AllUserRecordDataList.get(i));
            }
        }

        final String[] items = new String[selectUserRecordDataList.size()];

        for (int i = 0; i < selectUserRecordDataList.size(); ++i) {
            items[i] = dateFormat.format(selectUserRecordDataList.get(i).getRecordDate());
        }

        ListAdapter adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, items);
        ListView listView = (ListView) findViewById(R.id.ManageStudentInfo_StuDateList);
        listView.setAdapter(adapter);

        listView.setOnItemClickListener(
                new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                        String item = String.valueOf(parent.getItemAtPosition(position));
                        Intent intent = new Intent(ManageStudentInfoActivity.this, ManageDateInfoActivity.class);
                        intent.putExtra("studentData", currentStudentData);
                        Log.e("CCCC",selectUserRecordDataList.get(position).toString());
                        intent.putExtra("recordData", selectUserRecordDataList.get(position));
                        startActivity(intent);
                    }
                }
        );
    }
}


