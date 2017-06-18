package com.example.administrator.healthking_teacher;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import org.w3c.dom.Text;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    public void SettingClick(View v){
        TextView text = (TextView)findViewById(R.id.ResultText);

        text.setText("설정 버튼을 눌렀습니다.");
    }

    public void AddStudent(View v){
        TextView text = (TextView)findViewById(R.id.ResultText);

        text.setText("학생등록 버튼을 눌렀습니다.");
    }

    public void WagleClick(View v){

        TextView text = (TextView)findViewById(R.id.ResultText);

        text.setText("와글와글 버튼을 눌렀습니다.");

    }
    public void MissionClick(View v){
        TextView text = (TextView)findViewById(R.id.ResultText);
        text.setText("미션 버튼을 눌렀습니다.");
    }
}
