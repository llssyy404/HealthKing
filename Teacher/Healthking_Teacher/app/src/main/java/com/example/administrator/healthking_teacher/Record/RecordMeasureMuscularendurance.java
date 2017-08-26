package com.example.administrator.healthking_teacher.Record;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.ArrayAdapter;
import android.widget.ListAdapter;
import android.widget.ListView;

import com.example.administrator.healthking_teacher.Data.TagMatchData;
import com.example.administrator.healthking_teacher.R;

/**
 * Created by juicy on 2017-08-23.
 */

public class RecordMeasureMuscularendurance extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_measure_muscularendurance);


        String[] items;
        ListAdapter adapter;
        ListView listView;
        ListView listView2;
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
            listView = (ListView) findViewById(R.id.ListView);
            listView.setAdapter(adapter);
            listView2 = (ListView) findViewById(R.id.ListView2);
            listView2.setAdapter(adapter);
        }
    }
}
