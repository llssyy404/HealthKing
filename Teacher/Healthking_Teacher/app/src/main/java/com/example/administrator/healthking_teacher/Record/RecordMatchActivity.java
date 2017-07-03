package com.example.administrator.healthking_teacher.Record;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.example.administrator.healthking_teacher.Data.DataManager;
import com.example.administrator.healthking_teacher.Data.StudentRecordData;
import com.example.administrator.healthking_teacher.Data.TagData;
import com.example.administrator.healthking_teacher.R;

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

    /////////////////////////////////

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_record_match);

        studentRecordDataList = new ArrayList<>();
        studentRecordDataList.clear();

        letsgoButton = (Button)findViewById(R.id.letsgoButton);
        studentRecordIdTextView = (TextView)findViewById(R.id.studentRecordidTextView);

        for(int i=0;i<10;++i){
            Date tempdate = new Date();
            List<Date> templistdate = new ArrayList<>();
            templistdate.clear();
            templistdate.add(tempdate);
            templistdate.add(tempdate);
            templistdate.add(tempdate);
            StudentRecordData temp = new StudentRecordData(Integer.toString(i+ 1 * 33 ),tempdate,100,3,templistdate,tempdate);
            studentRecordDataList.add(temp);
        }

        //studentRecordDataList.add(DataManager.getInstance().getStudentRecordDataList().get(0));
        //studentRecordDataList.get(0);

        letsgoButton.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View view) {
                studentRecordIdTextView.setText(studentRecordDataList.get(0).getId());
            }
        });

    }

    public void Test(){


    }
}
