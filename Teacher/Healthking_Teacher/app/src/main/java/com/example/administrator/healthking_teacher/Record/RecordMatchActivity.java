package com.example.administrator.healthking_teacher.Record;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.administrator.healthking_teacher.Data.DataManager;
import com.example.administrator.healthking_teacher.Data.StudentRecordData;
import com.example.administrator.healthking_teacher.Data.TagData;
import com.example.administrator.healthking_teacher.R;
import com.example.administrator.healthking_teacher.RFIDDevice;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class RecordMatchActivity extends AppCompatActivity {

    private List<StudentRecordData> studentRecordDataList;
    private List<TagData> tagDatas;

    private List<StudentRecordData> teststudentRecordDataList;

    //////////////////////////////////
    private Button letsgoButton;
    private TextView studentRecordIdTextView;

    private Button rfidConnectButton;
    private Button rfidReadStartButton;
    private Button rfidReadEndButton;
    private Button rfidDisconnectButton;
    /////////////////////////////////

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_record_match);

        ///////////////////////////////// 레코드 리스트
        studentRecordDataList = new ArrayList<>();
        studentRecordDataList.clear();
        ///////////////////////////////// 태그 리스트
        tagDatas = new ArrayList<>();
        tagDatas.clear();

        ///////////////////////////////// UI 연결
        letsgoButton = (Button)findViewById(R.id.letsgoButton);
        studentRecordIdTextView = (TextView)findViewById(R.id.studentRecordidTextView);
        rfidConnectButton = (Button)findViewById(R.id.rfidConnectButton);
        rfidReadEndButton = (Button)findViewById(R.id.rfidReadEndButton);
        rfidReadStartButton = (Button)findViewById(R.id.rfidReadStartButton);
        rfidDisconnectButton = (Button)findViewById(R.id.rfidDisconnectButton);

        for(int i=0;i<10;++i){
            //long now = System.currentTimeMillis();
            Date tempdate = new Date();
            List<Date> templistdate = new ArrayList<>();
            templistdate.clear();
            templistdate.add(tempdate);
            templistdate.add(tempdate);
            templistdate.add(tempdate);
            StudentRecordData temp = new StudentRecordData(Integer.toString(i+ 1 * 33 ),tempdate,100,3,templistdate,tempdate);
            studentRecordDataList.add(temp);

            //TagData tempTagdata = new TagData(Integer.toString(i*1111));
            //tagDatas.add(tempTagdata);
        }

        //studentRecordDataList.add(DataManager.getInstance().getStudentRecordDataList().get(0));
        //studentRecordDataList.get(0);

        letsgoButton.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view) {

                studentRecordIdTextView.setText(getStringData(studentRecordDataList.get(0)));
                //studentRecordIdTextView.setText(Long.toString(studentRecordDataList.get(0).getRecordDate().getMinutes()) + "aaa");
            }
        });

        rfidConnectButton.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){
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
        rfidDisconnectButton.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){
                RFIDDevice.getInstance().DisconnectReader();
                myToastMessage("연결을 끊었습니다.");
            }
        });

        rfidReadEndButton.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){
                //tagDatas.add()
                for(int i=0;i<RFIDDevice.getInstance().GetTagListSize();++i){
                    TagData temp = new TagData(RFIDDevice.getInstance().getTagList()[i].toString());
                    tagDatas.add(temp);
                }

                RFIDDevice.getInstance().finishReadTag();

                String str = "";

                for(int i=0;i<tagDatas.size();++i){

                    str += Integer.toString(i) + "번째 태그: " + tagDatas.get(i).GetTagId();
                    str += "\n";
                }

                studentRecordIdTextView.setText(str);

                myToastMessage("태그 읽기를 종료합니다.");
            }
        });
        rfidReadStartButton.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view){
                RFIDDevice.getInstance().beginReadTag();
                myToastMessage("태그 읽기를 시작합니다.");
            }
        });
    }

    public String getStringData(StudentRecordData data){
        String temp = data.getId() + data.getRecordDate().toString() + data.getRecordMeter() + data.getTrackCount() + data.getTrackTimeDate() + data.getAllTrackTimeDate();
        return  temp;
    }

    public void myToastMessage(String str){
        Toast toast = Toast.makeText(RecordMatchActivity.this, str, Toast.LENGTH_SHORT );
        toast.show();
    }

    public void Test(){

    }
}
