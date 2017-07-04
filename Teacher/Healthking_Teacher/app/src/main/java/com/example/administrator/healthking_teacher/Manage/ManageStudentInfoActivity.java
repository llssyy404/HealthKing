package com.example.administrator.healthking_teacher.Manage;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.R;

import java.io.Serializable;

public class ManageStudentInfoActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage_student_info);

        TextView textView = (TextView) findViewById(R.id.ManageStudentInfo_stuText);
        Intent intent = getIntent();
        final StudentData studentData = (StudentData)intent.getExtras().get("stuText");
        textView.setText(studentData.getGrade()+ studentData.getClassroomNumber()+studentData.getName()+ studentData.getGender());

        String[] items = {"리스트1", "리스트2", "리스트3"};
        ListAdapter adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, items);
        ListView listView = (ListView) findViewById(R.id.ManageStudentInfo_StuDateList);
        listView.setAdapter(adapter);

        listView.setOnItemClickListener(
                new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                        String item = String.valueOf(parent.getItemAtPosition(position));
                        //Toast.makeText(ManageStudentInfoActivity.this, item, Toast.LENGTH_SHORT).show();
                        Intent intent = new Intent(getApplicationContext(), ManageDateInfoActivity.class);
                        intent.putExtra("stuText", studentData);
                        startActivity(intent);
                    }
                }
        );
    }
}


