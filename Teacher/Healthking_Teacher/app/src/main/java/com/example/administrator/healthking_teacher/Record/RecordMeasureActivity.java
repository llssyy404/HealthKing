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
import android.widget.Toast;

import com.example.administrator.healthking_teacher.Data.TagMatchData;
import com.example.administrator.healthking_teacher.R;
import com.example.administrator.healthking_teacher.RFIDDevice;

import java.util.List;

public class RecordMeasureActivity extends AppCompatActivity {
    int second=0, minute=0, hour=0;
    TextView timeText;
    Handler handler;

    private Runnable runnable;
    private boolean isRunning = false;

    private Button rfidMeasureConnect;
    private Button rfidMeasureDisconnect;

    private ListView timeListView;
    private TextView recordMeasureDebugText;
    private int debugCount = 0;
    private String[] studentitems;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_record_measure);

        timeText = (TextView) findViewById(R.id.elapsedTime);
        String Hour = ((hour>9)?"":"0")+hour;
        String Min = ((minute>9)?"":"0")+minute;
        String Sec = ((second>9)?"":"0")+second;
        timeText.setText("경과시간 : " + Hour + ":" + Min + ":" + Sec);

        ///////////////////////////////////////////////////////////////////////// jungmin
        rfidMeasureConnect = (Button)findViewById(R.id.rfidMeasureConnect);
        rfidMeasureDisconnect = (Button)findViewById(R.id.rfidMeasureDisconnect);
        timeListView = (ListView)findViewById(R.id.timeListView);
        recordMeasureDebugText = (TextView)findViewById(R.id.recordMeasureDebugText);

        studentitems = new String[TagMatchData.getInstance().GetTagDataList().size()];
        for(int i=0;i<TagMatchData.getInstance().GetTagDataList().size();++i){
            studentitems[i] = "";
        }
        ////////////////////////////////////////버튼 세팅.*
        rfidMeasureConnect.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){
                //myToastMessage("연결중입니다.");
                boolean isConnect = RFIDDevice.getInstance().set();
                if(isConnect) {
                    myToastMessage("연결에 실패했습니다.");
                }
                else
                {
                    myToastMessage("연결에 성공했습니다.");
                }
                RFIDDevice.getInstance().SetRfPowerAttenuation(25);
            }
        });

        rfidMeasureDisconnect.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){
                RFIDDevice.getInstance().DisconnectReader();
                myToastMessage("연결을 끊었습니다.");
            }
        });
        //////////////////////////////////////////////
        String[] items;
        ListAdapter adapter;
        ListView listView;
        //String[] items = {TagMatchData.getInstance().GetStudentRecordData().get(0).getId() + " 태그: " + TagMatchData.getInstance().GetTagDataList().get(0).GetTagId(), "리스트2", "리스트3"};
        if(0 < TagMatchData.getInstance().GetStudentRecordData().size()){
            items = new String[TagMatchData.getInstance().GetStudentRecordData().size()];
            for(int i=0;i<TagMatchData.getInstance().GetStudentRecordData().size();++i) {
                items[i] = TagMatchData.getInstance().GetStudentRecordData().get(i).getId();
            }
            adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, items);
            listView= (ListView) findViewById(R.id.Record_ListView);
            listView.setAdapter(adapter);
        }else{
            items = new String[1];
            items[0] = "학생 태그매치를 먼저 해주세요!^^";

            adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, items);
            listView = (ListView) findViewById(R.id.Record_ListView);
            listView.setAdapter(adapter);
        }
        //////////////////////////////////////////////////////////////////////// jungmin

        handler = new Handler() {
            public void handleMessage(Message msg) {
                if(isRunning == false)
                    return;

                /*second++;
                String Hour = ((hour>9)?"":"0")+hour;
                String Min = ((minute>9)?"":"0")+minute;
                String Sec = ((second>9)?"":"0")+second;
                timeText.setText("경과시간 : " + Hour + ":" + Min + ":" + Sec);*/

                handler.sendEmptyMessageDelayed(0,10); // 1초에 1씩 증가(1000 = 1 초)
                second++;
                String Hour = ((hour>9)?"":"0")+hour;
                String Min = ((minute>9)?"":"0")+minute;
                String Sec = ((second>9)?"":"0")+second;
                timeText.setText("경과시간 : " + Hour + ":" + Min + ":" + Sec);
                ////////////////////////// jungmin

                int nowTagListSize = RFIDDevice.getInstance().GetTagListSize();
                int TagListSize = TagMatchData.getInstance().GetTagDataList().size();

                if(0 < nowTagListSize){

                    for(int i=0;i<nowTagListSize;++i){
                        for(int j=0;j<TagListSize;++j){
                            //debugCount += 1;

                            if(TagMatchData.getInstance().GetTagDataList().get(j).GetTagId().equals(RFIDDevice.getInstance().getTagList()[i].toString())){
                                debugCount += 1;
                                studentitems[j] = "경과시간 : " + Hour + ":" + Min + ":" + Sec;
                                ListAdapter adapter = new ArrayAdapter<String>(RecordMeasureActivity.this, android.R.layout.simple_list_item_1, studentitems);

                                //ListView listView = (ListView) findViewById(R.id.tagMatchListView);
                                timeListView.setAdapter(adapter);
                            }
                        }
                    }
                    RFIDDevice.getInstance().ClearTagList();
                }

                recordMeasureDebugText.setText(Integer.toString(debugCount));
                //////////////////////////
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

                //////////////////////////////jungmin   태그기와 연결을 한뒤 측정시작 // 태그기연결에 시간이 걸리니 시작과 다른 버튼을 넣는게 좋을듯.

                RFIDDevice.getInstance().beginReadTag();
                //////////////////////////////
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
                ///////////////////////////////////////////////////////jungmin 리드를종료하고 태그기와 연결을 끊기.
                RFIDDevice.getInstance().finishReadTag();
                myToastMessage("기록측정을 종료합니다.");
                //////////////////////////////////////////////////////
            }
        });
    }

    public void Record_saveClick(View v)
    {

    }
    public void myToastMessage(String str){
        Toast toast = Toast.makeText(RecordMeasureActivity.this, str, Toast.LENGTH_SHORT );
        toast.show();
    }
}
