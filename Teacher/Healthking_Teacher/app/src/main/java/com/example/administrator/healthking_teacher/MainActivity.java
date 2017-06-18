package com.example.administrator.healthking_teacher;

import android.os.Debug;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    TextView textview;
    Button readButton;
    Button endButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

       boolean isConnect = RFIDDevice.getInstance().set();

        if(isConnect) {
            Log.d("Debug", "Success");
        }
        else
        {
            Log.d("Debug", "false");
        }



        textview = (TextView) findViewById(R.id.tagView);
        readButton = (Button) findViewById(R.id.readButton);
        endButton = (Button) findViewById(R.id.endButton);


        readButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                RFIDDevice.getInstance().beginReadTag();
            }
        });

        endButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                String[] tagList = RFIDDevice.getInstance().getTagList();

                Log.d("Debug", String.valueOf(RFIDDevice.getInstance().getTagList().length));

                StringBuilder sb = new StringBuilder();

                if(tagList != null)
                {
                    for(int i=0; i<tagList.length; ++i)
                    {
                        sb.append(tagList[i].toString());
                        sb.append("\n");
                    }
                }
                textview.setText(sb.toString());
                RFIDDevice.getInstance().finishReadTag();
            }
    });






    }
}
