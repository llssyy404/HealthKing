package com.example.administrator.healthking_teacher.Record;

import android.content.Intent;
import android.support.annotation.IdRes;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.administrator.healthking_teacher.R;


/**
 * Created by juicy on 2017-08-23.
 */

public class RecordContentsActivity extends AppCompatActivity {

    private Button CardiovascularEnduranceButton;
    private Button AgilityButton;
    private Button MuscularEndurance;


    @Override
    protected void onCreate(Bundle savedInstanceState){
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_measure_contents);

        CardiovascularEnduranceButton = (Button)findViewById(R.id.button_CardiovascularEndurance);
        AgilityButton = (Button)findViewById(R.id.button_Agility);
        MuscularEndurance = (Button)findViewById(R.id.button_MuscularEndurance);


        //버튼처리.
        CardiovascularEnduranceButton.setOnClickListener(new View.OnClickListener(){
            public void onClick(View view){
                myToastMessage("CardiovascularEnduranceButton");
                Intent intent = new Intent(getApplicationContext(), RecordMeasureActivity.class);
                startActivity(intent);
            }
        });
        AgilityButton.setOnClickListener(new View.OnClickListener(){
            public void onClick(View view){
                Intent intent = new Intent(getApplicationContext(), RecordMeasureAgillityActivity.class);
                startActivity(intent);
                myToastMessage("AgilityButton");
            }
        });
        MuscularEndurance.setOnClickListener(new View.OnClickListener(){
            public void onClick(View view){
                Intent intent = new Intent(getApplicationContext(), RecordMeasureMuscularendurance.class);
                startActivity(intent);
                myToastMessage("MuscularEndurance");
            }
        });
    }



    public void myToastMessage(String str){
        Toast toast = Toast.makeText(RecordContentsActivity.this, str, Toast.LENGTH_SHORT );
        toast.show();
    }

}
