package com.example.administrator.healthking_teacher.Title;

import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.example.administrator.healthking_teacher.Data.DataManager;
import com.example.administrator.healthking_teacher.Manage.ManageActivity;
import com.example.administrator.healthking_teacher.Mission.MissionActivity;
import com.example.administrator.healthking_teacher.R;
import com.example.administrator.healthking_teacher.Record.RecordActivity;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;


public class TitleActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_title);

        Button recordButton = (Button) findViewById(R.id.Title_RecordButton);
        Button manageButton = (Button) findViewById(R.id.Title_ManageButton);
        Button missionButton = (Button) findViewById(R.id.Title_MissionButton);
        Button boardButton = (Button) findViewById(R.id.Title_BoardButton);
        Button dbButton = (Button) findViewById(R.id.Title_DatabaseButton);

        recordButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getApplicationContext(), RecordActivity.class);
                startActivity(intent);
            }
        });

        manageButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getApplicationContext(), ManageActivity.class);
                startActivity(intent);
            }
        });

        missionButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getApplicationContext(), MissionActivity.class);
                startActivity(intent);
            }
        });

        boardButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse( "http://cafe.naver.com/unityhub"  ));
                startActivity(intent);
            }
        });

        dbButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                new GetStudentListBackGroundTask().execute();
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

            AlertDialog dialog;
            AlertDialog.Builder builder = new AlertDialog.Builder(TitleActivity.this);
            dialog = builder.setMessage("데이터 연동이 완료되었습니다.")
                    .setNegativeButton("확인", null)
                    .create();
            dialog.show();

        }


    }
}