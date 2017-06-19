package com.example.administrator.healthking_teacher.Title;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.example.administrator.healthking_teacher.Board.BoardActivity;
import com.example.administrator.healthking_teacher.Manage.ManageActivity;
import com.example.administrator.healthking_teacher.Mission.MissionActivity;
import com.example.administrator.healthking_teacher.R;
import com.example.administrator.healthking_teacher.Record.RecordActivity;


public class TitleActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_title);

        Button recordButton = (Button) findViewById(R.id.Title_RecordButton);
        Button manageButton = (Button) findViewById(R.id.Title_ManageButton);
        Button missionButton = (Button) findViewById(R.id.Title_MissionButton);
        Button boardButton = (Button) findViewById(R.id.Title_BoardButton);

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
                Intent intent = new Intent(getApplicationContext(), BoardActivity.class);
                startActivity(intent);
            }
        });

    }
}
