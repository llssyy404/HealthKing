package com.example.administrator.healthking_teacher.Record;

import android.os.Handler;
import android.os.Message;
import android.os.SystemClock;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.administrator.healthking_teacher.Data.DataManager;
import com.example.administrator.healthking_teacher.Data.StudentRecordData;
import com.example.administrator.healthking_teacher.Data.TagMatchData;
import com.example.administrator.healthking_teacher.R;
import com.example.administrator.healthking_teacher.RFIDDevice;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
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
    private boolean[] fillter;
    private int[] fillter_seconds;
    private Date[] oldDate;
    private Date[] totalDate;
    private int[] countTrack;

    private int FinishTrackCount = 0;
    private int TrackMeter = 0;

    private long[] totalETime;

    private List<StudentRecordData> studentRecordData;
    private Date[][] trackTimeList;

    //private StudentRecordData[][] studentRecordList;
    //private List<List<StudentRecordData>> studentRecordList;

    private Date test;
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
        fillter = new boolean[TagMatchData.getInstance().GetTagDataList().size()];
        fillter_seconds = new int[TagMatchData.getInstance().GetTagDataList().size()];
        oldDate = new Date[TagMatchData.getInstance().GetTagDataList().size()];
        totalDate= new Date[TagMatchData.getInstance().GetTagDataList().size()];
        countTrack = new int[TagMatchData.getInstance().GetTagDataList().size()];
        totalETime = new long[TagMatchData.getInstance().GetTagDataList().size()];
        studentRecordData = new ArrayList<>();
        //studentRecordData = new StudentRecordData[TagMatchData.getInstance().GetTagDataList().size()];
        //studentRecordList = new StudentRecordData[TagMatchData.getInstance().GetTagDataList().size()][];
        //studentRecordList = new ArrayList<>();
        for(int i=0;i<TagMatchData.getInstance().GetTagDataList().size();++i){
            studentitems[i] = "";
            fillter[i] = false;
            fillter_seconds[i] = 0;
            countTrack[i] = 0;
            totalETime[i] = 0;

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
                RFIDDevice.getInstance().SetRfPowerAttenuation(5);
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
                //SystemClock.currentThreadTimeMillis();

                recordMeasureDebugText.setText(Long.toString(System.currentTimeMillis()));
                handler.sendEmptyMessageDelayed(0,1); // 1초에 1씩 증가(1000 = 1 초)
                second++;
                String Hour = ((hour>9)?"":"0")+hour;
                String Min = ((minute>9)?"":"0")+minute;
                String Sec = ((second>9)?"":"0")+second;
                timeText.setText("경과시간 : " + Hour + ":" + Min + ":" + Sec);
                ////////////////////////// jungmin

                for(int i=0;i<fillter.length;++i){
                    if(fillter[i]){
                        if(500 < fillter_seconds[i]){
                            fillter[i] = false;
                            fillter_seconds[i] = 0;
                        }else
                            fillter_seconds[i] += 1;
                    }
                }

                int nowTagListSize = RFIDDevice.getInstance().GetTagListSize();
                int TagListSize = TagMatchData.getInstance().GetTagDataList().size();

                if(0 < nowTagListSize){

                    for(int i=0;i<nowTagListSize;++i){
                        for(int j=0;j<TagListSize;++j){
                            //debugCount += 1;

                            if(!fillter[j] && TagMatchData.getInstance().GetTagDataList().get(j).GetTagId().equals(RFIDDevice.getInstance().getTagList()[i].toString())){
                                if(FinishTrackCount <= countTrack[j])
                                    break;


                                debugCount += 1;

                                Date testDate = new Date();

                                long eleaseTime = testDate.getTime() - oldDate[j].getTime();

                                //String.format("%02d:%02d:%02d", testvalue/1000 / 60, (testvalue/1000)%60,(testvalue%1000)/10);

                                //studentitems[j] = Long.toString(testvalue) + "  경과시간 : " + Hour + ":" + Min + ":" + Sec;
                                //studentitems[j] = String.format("%02d:%02d:%02d", testvalue/1000 / 60, (testvalue/1000)%60,(testvalue%1000)/10);
                                //studentitems[j] = test.toString();
                                //int min = Integer.parseInt(Hour);
                                //int sec = Integer.parseInt(Min);
                                //int milli = Integer.parseInt(Sec);
                                int min = (int)eleaseTime/1000 / 60;
                                int sec = (int)(eleaseTime/1000)%60;
                                int milli = (int)(eleaseTime%1000)/10;
                                oldDate[j].setHours(0);
                                oldDate[j].setMinutes(min);
                                oldDate[j].setSeconds(sec);

                                trackTimeList[j][countTrack[j]] = oldDate[j];

                                studentitems[j] = String.format("%02d:%02d:%02d", eleaseTime/1000 / 60, (eleaseTime/1000)%60,(eleaseTime%1000)/10);
                                totalETime[j] += eleaseTime;
                                //TagMatchData.getInstance().GetStudentRecordData().get(j).getTrackTimeDate().get(0).

                                oldDate[j] = (Date)testDate.clone();
                                //Date da = new Date();
                                //TagMatchData.getInstance().GetStudentRecordData().get(j).se


                                ListAdapter adapter = new ArrayAdapter<String>(RecordMeasureActivity.this, android.R.layout.simple_list_item_1, studentitems);

                                //ListView listView = (ListView) findViewById(R.id.tagMatchListView);
                                timeListView.setAdapter(adapter);

                                countTrack[j] += 1;
                                fillter[j] = true;
                            }
                        }
                    }
                    RFIDDevice.getInstance().ClearTagList();
                }

                //recordMeasureDebugText.setText(Integer.toString(RFIDDevice.getInstance().getRfPowerAttenuation()));

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
                //test = new Date();

                EditText editTextTrackCount = (EditText)findViewById(R.id.Record_CountInput);
                FinishTrackCount = Integer.parseInt(editTextTrackCount.getText().toString());
                EditText editTextTrackMeter = (EditText)findViewById(R.id.Record_MeterInput);
                TrackMeter = Integer.parseInt(editTextTrackMeter.getText().toString());

                if(FinishTrackCount == 0){
                    Date d = new Date();
                    myToastMessage("트랙을 1이상으로 설정해주세요." + d.toString());
                    return;
                }
                trackTimeList = new Date[TagMatchData.getInstance().GetTagDataList().size()][FinishTrackCount];
                for(int i=0;i<TagMatchData.getInstance().GetTagDataList().size();++i){
                    oldDate[i] = new Date();
                    totalDate[i] = new Date();

                    Date tempdate = new Date();
                    List<Date> templistdate = new ArrayList<>();
                    templistdate.clear();
                    templistdate.add(tempdate);
                    templistdate.add(tempdate);
                    templistdate.add(tempdate);

                    StudentRecordData temp = new StudentRecordData("",tempdate,TrackMeter,FinishTrackCount,templistdate,tempdate);
                    studentRecordData.add(temp);

                    for(int j=0;j<FinishTrackCount;++j) {
                        trackTimeList[i][j] = tempdate;
                    }
                }

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

                for(int i =0;i<TagMatchData.getInstance().GetTagDataList().size();++i){
                    Date temp = new Date();

                    long eleaseTime = temp.getTime() - totalDate[i].getTime();
                    /*int min = (int)eleaseTime/1000 / 60;
                    int sec = (int)(eleaseTime/1000)%60;
                    int milli = (int)(eleaseTime%1000)/10;*/
                    if(totalETime[i] == 0){
                        totalETime[i] = 1;
                    }
                    int min = (int)totalETime[i]/1000 / 60;
                    int sec = (int)(totalETime[i]/1000)%60;
                    int milli = (int)(totalETime[i]%1000)/10;

                    totalDate[i].setHours(0);
                    totalDate[i].setMinutes(min);
                    totalDate[i].setSeconds(sec);

                    List<Date> tempArray = new ArrayList<>();
                    tempArray.clear();
                    for(int j=0;j<FinishTrackCount;++j){
                        tempArray.add(trackTimeList[i][j]);
                    }

                    studentitems[i] = String.format("%02d:%02d:%02d", min, sec,milli) + Integer.toString(countTrack[i]);

                    ListAdapter adapter = new ArrayAdapter<String>(RecordMeasureActivity.this, android.R.layout.simple_list_item_1, studentitems);

                    studentRecordData.get(i).setId(TagMatchData.getInstance().GetStudentRecordData().get(i).getId());
                    studentRecordData.get(i).setAllTrackTimeDate(totalDate[i]); ///토탈트랙기록
                    studentRecordData.get(i).setTrackTimeDate(tempArray);    ///트랙당 저장

                    //ListView listView = (ListView) findViewById(R.id.tagMatchListView);
                    timeListView.setAdapter(adapter);
                }

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
        DataManager.getInstance().setSendStudentRecodeDatas(studentRecordData);
        myToastMessage("기록을 저장하였습니다.");
    }
    public void myToastMessage(String str){
        Toast toast = Toast.makeText(RecordMeasureActivity.this, str, Toast.LENGTH_SHORT );
        toast.show();
    }
}
