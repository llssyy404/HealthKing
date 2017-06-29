package com.example.administrator.healthking_teacher.Manage;

import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Spinner;

import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.Data.DataManager;
import com.example.administrator.healthking_teacher.R;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

public class ManageInfoActivity extends AppCompatActivity {

    private ArrayAdapter schoolAdapter;
    private Spinner schoolSpinner;

    private ArrayAdapter gradeAdapter;
    private Spinner gradeSpinner;

    private ArrayAdapter classroomAdapter;
    private Spinner classroomSpinner;

    private ListView studentListView;
    private List<StudentData> searchStudentDataList;

    private Button searchButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage_info);
        Init();
        new GetStudentListBackGroundTask().execute();
    }

    private void Init() {
        schoolSpinner = (Spinner) findViewById(R.id.ManageInfo_schoolSpinner);
        schoolAdapter = ArrayAdapter.createFromResource(ManageInfoActivity.this, R.array.schoolType, android.R.layout.simple_spinner_dropdown_item);
        schoolSpinner.setAdapter(schoolAdapter);

        gradeSpinner = (Spinner) findViewById(R.id.ManageInfo_gradeSpinner);
        gradeAdapter = ArrayAdapter.createFromResource(ManageInfoActivity.this, R.array.elementarySchoolGrade, android.R.layout.simple_spinner_dropdown_item);
        gradeSpinner.setAdapter(gradeAdapter);

        classroomSpinner = (Spinner) findViewById(R.id.ManageInfo_classroomNumberSpinner);
        classroomAdapter = ArrayAdapter.createFromResource(ManageInfoActivity.this, R.array.classroomNumber, android.R.layout.simple_spinner_dropdown_item);
        classroomSpinner.setAdapter(classroomAdapter);


        schoolSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if (schoolSpinner.getSelectedItem().equals("초등학교")) {
                    gradeAdapter = ArrayAdapter.createFromResource(ManageInfoActivity.this, R.array.elementarySchoolGrade, android.R.layout.simple_spinner_dropdown_item);
                    gradeSpinner.setAdapter(gradeAdapter);
                } else if (schoolSpinner.getSelectedItem().equals("중학교")) {
                    gradeAdapter = ArrayAdapter.createFromResource(ManageInfoActivity.this, R.array.middleSchoolGrade, android.R.layout.simple_spinner_dropdown_item);
                    gradeSpinner.setAdapter(gradeAdapter);
                }
                if (schoolSpinner.getSelectedItem().equals("고등학교")) {
                    gradeAdapter = ArrayAdapter.createFromResource(ManageInfoActivity.this, R.array.highSchoolGrade, android.R.layout.simple_spinner_dropdown_item);
                    gradeSpinner.setAdapter(gradeAdapter);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });


        studentListView = (ListView) findViewById(R.id.ManageInfo_studentListView);
        searchStudentDataList = new ArrayList<StudentData>();

        searchButton = (Button) findViewById(R.id.ManageInfo_searchButton);
        searchButton.setEnabled(false);
        searchButton.setBackgroundColor(getResources().getColor(R.color.colorGray));
        searchButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                searchStudentDataList.clear();
                List<StudentData> AllStudentDataList = DataManager.getInstance().getStudentDataList();

                for (int i = 0; i < AllStudentDataList.size(); ++i) {
                    StudentData data = AllStudentDataList.get(i);
                    if (data.getSchool().equals(schoolSpinner.getSelectedItem()) && data.getGrade().equals(gradeSpinner.getSelectedItem()) && data.getClassroomNumber().equals(classroomSpinner.getSelectedItem())) {
                        searchStudentDataList.add(data);
                    }
                }

                StudentListAdapter studentListAdapter = new StudentListAdapter(getApplicationContext(), searchStudentDataList);
                studentListView.setAdapter(studentListAdapter);
            }
        });

    }


    class GetStudentListBackGroundTask extends AsyncTask<Void, Void, String> {
        String target;

        @Override
        protected void onPreExecute() {
            target = "http://came1230.cafe24.com/GetAllUserList.php";
        }

        @Override
        protected String doInBackground(Void... voids) {
            try {

                URL url = new URL(target);
                HttpURLConnection httpURLConnection = (HttpURLConnection) url.openConnection();
                InputStream inputStream = httpURLConnection.getInputStream();
                BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
                String temp;
                StringBuilder stringBuilder = new StringBuilder();

                while ((temp = bufferedReader.readLine()) != null) {
                    stringBuilder.append(temp + "\n");
                }
                bufferedReader.close();
                inputStream.close();
                httpURLConnection.disconnect();
                return stringBuilder.toString().trim();
            } catch (Exception e) {
                e.printStackTrace();
            }
            return null;
        }

        @Override
        public void onProgressUpdate(Void... values) {
            super.onProgressUpdate(values);
        }

        @Override
        public void onPostExecute(String result) {

            DataManager.getInstance().SetStudentDatas(result);
            searchButton.setEnabled(true);
            searchButton.setBackgroundColor(getResources().getColor(R.color.colorPrimary));

        }


    }
}
