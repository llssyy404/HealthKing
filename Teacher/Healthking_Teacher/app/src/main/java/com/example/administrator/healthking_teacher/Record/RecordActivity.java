package com.example.administrator.healthking_teacher.Record;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.Data.TagData;
import com.example.administrator.healthking_teacher.R;
import com.example.administrator.healthking_teacher.RFIDDevice;

import org.w3c.dom.Text;

import java.util.Dictionary;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;

public class RecordActivity extends AppCompatActivity {

//    /*
//    아이들 데이터에 태그를 하나씩 매칭시켜준다. -> 매칭 UI 필요.
//
//
//     */
//    //private RFIDDevice test; // 이부분이 필요할지 생각해보자
//
//    private static List<TagData> TagDatalist;
//    StudentData studentList;
//    TextView textView;
//    Button readButton;
//    Button endButton;
//
//    Button rfidConnectButton;
//    Button settagButton;
//    Button finishReadButton;
//    TextView tempTextView;
//    private static HashMap<TagData,StudentData> maplist = new HashMap<TagData,StudentData>();
//
//    private static Iterator<TagData> Ir_TagKeys;
//    private int taglistsize = 0;

    //List<StudentData,TagData> listArr;

    //Dictionary<StudentData,TagData> listArr;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_record);

        Button matchButton = (Button) findViewById(R.id.Record_matchButton);
        Button measureButton = (Button) findViewById(R.id.Record_measureButton);

        matchButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getApplicationContext(), RecordMatchActivity.class);
                startActivity(intent);
            }
        });

        measureButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getApplicationContext(), RecordMeasureActivity.class);
                startActivity(intent);
            }
        });


        //Old Code
//        //test = RFIDDevice.getInstance(); // 가져왔는가?
//
//        rfidConnectButton = (Button)findViewById(R.id.rfidConnectButton);
//        textView = (TextView) findViewById(R.id.tagView);
//        readButton = (Button) findViewById(R.id.readButton);
//        endButton = (Button) findViewById(R.id.endButton);
//        tempTextView = (TextView)findViewById(R.id.tempTextView);
//        settagButton = (Button) findViewById(R.id.settagButton);
//        finishReadButton = (Button)findViewById(R.id.finishReadButton);
//
//        //TagDatalist.clear();
//
//
//        readButton.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                RFIDDevice.getInstance().beginReadTag();
//            }
//        });
//
//        finishReadButton.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                RFIDDevice.getInstance().finishReadTag();
//            }
//        });
//
//        settagButton.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View v){
//
//            }
//        });
//
//
//
//        rfidConnectButton.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                boolean isConnect = RFIDDevice.getInstance().set();
//
//                if(isConnect) {
//                    Log.d("Debug", "Success healthKing!!!!!!!!");
//                }
//                else
//                {
//                    Log.d("Debug", "false healthKing!!!!!!!!");
//                }
//                RFIDDevice.getInstance().SetRfPowerAttenuation(25);
//            }
//        });
//
//        endButton.setOnClickListener(new View.OnClickListener() { // test 잘되는군 멋지군.
//            @Override
//            public void onClick(View view) {
//                String[] tagList = RFIDDevice.getInstance().getTagList();
//
//                taglistsize = RFIDDevice.getInstance().getTagList().length;
//
//                //Log.d("Debug", String.valueOf(RFIDDevice.getInstance().getTagList().length));
//                Log.d("Debug", "hello!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1");
//                //if(TagDatalist != null){
//                //    TagDatalist.clear();
//                //}
//
//                //for(int i=0;i<taglistsize;++i){
//                //    TagDatalist.add(new TagData(tagList[i]));
//                //}
//
//                StringBuilder sb = new StringBuilder();
//
//                if(tagList != null)
//                {
//                    for(int i=0; i<tagList.length; ++i)
//                    {
//                        sb.append(tagList[i].toString());
//                        sb.append("\n");
//                    }
//                }
//                textView.setText(sb.toString());
//                //tempTextView.setText((Integer.toString(TagDatalist.size())));
//
//                RFIDDevice.getInstance().finishReadTag();
//
//            }
//        });
    }
}
