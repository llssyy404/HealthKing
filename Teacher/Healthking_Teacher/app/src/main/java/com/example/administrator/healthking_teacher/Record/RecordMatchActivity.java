package com.example.administrator.healthking_teacher.Record;

import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.administrator.healthking_teacher.Data.DataManager;
import com.example.administrator.healthking_teacher.Data.StudentRecordData;
import com.example.administrator.healthking_teacher.Data.TagData;
import com.example.administrator.healthking_teacher.Data.TagMatchData;
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

    private TextView studentRecordIdTextView;

    private Button rfidConnectButton;
    private Button rfidReadStartButton;
    private Button rfidReadEndButton;
    private Button rfidDisconnectButton;

    private ListView tagListView;
    private ListView tagMatchListView;
    /////////////////////////////////

    private boolean matching = false;
    private int studentInex = 0;
    private int tagIndex = 0;

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

        studentRecordIdTextView = (TextView)findViewById(R.id.studentRecordidTextView);
        rfidConnectButton = (Button)findViewById(R.id.rfidConnectButton);
        rfidReadEndButton = (Button)findViewById(R.id.rfidReadEndButton);
        rfidReadStartButton = (Button)findViewById(R.id.rfidReadStartButton);
        rfidDisconnectButton = (Button)findViewById(R.id.rfidDisconnectButton);
        tagMatchListView = (ListView)findViewById(R.id.tagMatchListView);
        tagListView = (ListView)findViewById(R.id.tagListView);

        /*for(int i=0;i<10;++i){
            //long now = System.currentTimeMillis();
            Date tempdate = new Date();
            List<Date> templistdate = new ArrayList<>();
            templistdate.clear();
            templistdate.add(tempdate);
            templistdate.add(tempdate);
            templistdate.add(tempdate);
            StudentRecordData temp = new StudentRecordData("홍길동 " + Integer.toString((i+1)),tempdate,100,3,templistdate,tempdate);
            studentRecordDataList.add(temp);

            //TagData tempTagdata = new TagData(Integer.toString(i*1111));
            //tagDatas.add(tempTagdata);
        }*/


        //studentRecordDataList.add(DataManager.getInstance().getStudentRecordDataList().get(0));
        //studentRecordDataList.get(0);



        rfidConnectButton.setOnClickListener(new View.OnClickListener(){
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
                if(0 < DataManager.getInstance().getStudentDataList().size()) {
                    for(int i=0;i<DataManager.getInstance().getStudentDataList().size();++i) {
                        //String id, // Date recordDate, //int recordMeter, //int trackCount, //List<Date> trackTimeDate, //Date allTrackTimeDate
                        //StudentRecordData temp = new StudentRecordData(DataManager.getInstance().getStudentRecodeDatas().get(i).getId(),DataManager.getInstance().getStudentRecodeDatas().get(i).getRecordDate(),DataManager.getInstance().getStudentRecodeDatas().get(i).getRecordMeter(),DataManager.getInstance().getStudentRecodeDatas().get(i).getTrackCount(),DataManager.getInstance().getStudentRecodeDatas().get(i).getTrackTimeDate(),DataManager.getInstance().getStudentRecodeDatas().get(i).getAllTrackTimeDate());
                        Date tempdate = new Date();
                        List<Date> templistdate = new ArrayList<>();
                        templistdate.clear();
                        templistdate.add(tempdate);
                        templistdate.add(tempdate);
                        templistdate.add(tempdate);
                        StudentRecordData temp = new StudentRecordData(DataManager.getInstance().getStudentDataList().get(i).getId(),tempdate,100,3,templistdate,tempdate);
                        studentRecordDataList.add(temp);
                    }

                }

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

                //studentRecordIdTextView.setText(str);

                //String[] items = {getStringData(studentRecordDataList.get(0)), getStringData(studentRecordDataList.get(1)), getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2))};
                //ListAdapter adapter = new ArrayAdapter<String>(RecordMatchActivity.this, android.R.layout.simple_list_item_1, items);
                String[] studentitems = new String[studentRecordDataList.size()];
                ListAdapter adapter = new ArrayAdapter<String>(RecordMatchActivity.this, android.R.layout.simple_list_item_1, studentitems);

                for(int i=0;i<studentRecordDataList.size();++i){
                    studentitems[i] = getStringData(studentRecordDataList.get(i));
                }

                //ListView listView = (ListView) findViewById(R.id.tagMatchListView);
                tagMatchListView.setAdapter(adapter);
                tagMatchListView.setOnItemClickListener(new ListViewItemClickListener());



                //String[] tagitems = {getStringData(studentRecordDataList.get(0)), getStringData(studentRecordDataList.get(1)), getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2)),getStringData(studentRecordDataList.get(2))};
                //ListAdapter tagadapter = new ArrayAdapter<String>(RecordMatchActivity.this, android.R.layout.simple_list_item_1, tagitems);

                String[] tagitems = new String[tagDatas.size()];
                ListAdapter tagadapter = new ArrayAdapter<String>(RecordMatchActivity.this, android.R.layout.simple_list_item_1, tagitems);

                for(int i=0;i<tagDatas.size();++i){
                    tagitems[i] = tagDatas.get(i).GetTagId();
                }

                tagListView.setAdapter(tagadapter);
                tagListView.setOnItemClickListener(new tagListViewItemClickListener());

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

    //////// 학생리스트에 대한 클릭처리
    private class ListViewItemClickListener implements AdapterView.OnItemClickListener
    {
        @Override
        public void onItemClick(AdapterView<?> parent, View view, int position, long id)
        {
            studentInex = position;
            matching = true;
            myToastMessage("학생: " + studentInex + " 이 선택되었습니다.");
        }
    }
    ///////// tagList에 대한 클릭 처리
    private class tagListViewItemClickListener implements AdapterView.OnItemClickListener
    {
        @Override
        public void onItemClick(AdapterView<?> parent, View view, int position, long id)
        {
            if(matching){
                tagIndex = position;
                matching = false;
                TagMatchData.getInstance().SetList(studentRecordDataList.get(studentInex),tagDatas.get(tagIndex));
                myToastMessage("학생: " + studentInex + "태그: " + tagIndex + "\n매칭되었습니다.");
            }else{
                myToastMessage("학생을 먼저 골라주세요.");
            }

        }
    }

    public String getStringData(StudentRecordData data){
        String temp = "아이디: " + data.getId();
        return  temp;
    }

    public void myToastMessage(String str){
        Toast toast = Toast.makeText(RecordMatchActivity.this, str, Toast.LENGTH_SHORT );
        toast.show();
    }

    public void Test(){

    }
}
