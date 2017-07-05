package com.example.administrator.healthking_teacher.Mission;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.administrator.healthking_teacher.R;

import java.util.ArrayList;
import java.util.List;

public class MissionActivity extends AppCompatActivity {

    int missionCount = 0;
    List<TextView> listTextView;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_mission);

        Button registerButton = (Button) findViewById(R.id.Mission_registButton);
        listTextView = new ArrayList<>();
        listTextView.add((TextView) findViewById(R.id.Mission_text1));
        listTextView.add((TextView) findViewById(R.id.Mission_text2));
        listTextView.add((TextView) findViewById(R.id.Mission_text3));
        listTextView.add((TextView) findViewById(R.id.Mission_text4));
        listTextView.add((TextView) findViewById(R.id.Mission_text5));
        listTextView.add((TextView) findViewById(R.id.Mission_text6));
        listTextView.add((TextView) findViewById(R.id.Mission_text7));
        listTextView.add((TextView) findViewById(R.id.Mission_text8));
        listTextView.add((TextView) findViewById(R.id.Mission_text9));
        listTextView.add((TextView) findViewById(R.id.Mission_text10));

        registerButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                EditText inputText = (EditText) findViewById(R.id.Mission_inputText);
                if(inputText.getText().toString().isEmpty())
                    return;

                if(missionCount >= 10)
                {
                    inputText.setText("");
                    String notice = "미션개수 MAX";
                    Toast.makeText(getApplicationContext(), notice, Toast.LENGTH_SHORT).show();
                    return;
                }

                listTextView.get(missionCount).setText(inputText.getText().toString());
                missionCount++;
                inputText.setText("");
            }
        });
    }
}
