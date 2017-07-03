package com.example.administrator.healthking_teacher.Record;

import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.example.administrator.healthking_teacher.R;

public class RecordMeasureActivity extends AppCompatActivity {
    int second=0, minute=0, hour=0;
    TextView timeText;
    Handler handler;

    private Runnable runnable;
    private boolean isRunning = false;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_record_measure);

        timeText = (TextView) findViewById(R.id.elapsedTime);
        String Hour = ((hour>9)?"":"0")+hour;
        String Min = ((minute>9)?"":"0")+minute;
        String Sec = ((second>9)?"":"0")+second;
        timeText.setText("경과시간 : " + Hour + ":" + Min + ":" + Sec);

        String[] items = {"리스트1", "리스트2", "리스트3"};
        ListAdapter adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, items);
        ListView listView = (ListView) findViewById(R.id.Record_ListView);
        listView.setAdapter(adapter);

        handler = new Handler() {
            public void handleMessage(Message msg) {
                if(isRunning == false)
                    return;

                handler.sendEmptyMessageDelayed(0,1000); // 1초에 1씩 증가(1000 = 1 초)

                second++;
                String Hour = ((hour>9)?"":"0")+hour;
                String Min = ((minute>9)?"":"0")+minute;
                String Sec = ((second>9)?"":"0")+second;
                timeText.setText("경과시간 : " + Hour + ":" + Min + ":" + Sec);
                if(second == 59)
                {
                    second=-1;
                    minute++;

                    if(minute == 59)
                    {
                        minute=-1;
                        hour++;
                    }
                }
            }
        };
        handler.sendEmptyMessage(0);
    }

    public void Record_startClick(View v)
    {
        final Button btnStartPause = (Button) findViewById(R.id.Record_Startbutton);
        btnStartPause.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(isRunning == true)
                    return;

                isRunning = true;
                handler.removeCallbacks(runnable);
                second=0; minute=0; hour=0;
                handler.post(runnable); // Runnable 시작
            }
        });
    }

    public void Record_stopClick(View v)
    {
        Button btnStop = (Button) findViewById(R.id.Record_Stopbutton);
        btnStop.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(isRunning == false)
                    return;

                isRunning = false;
                handler.removeCallbacks(runnable);
            }
        });
    }

    public void Record_saveClick(View v)
    {

    }
}
