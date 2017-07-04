package com.example.administrator.healthking_teacher.Manage;

import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;

import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.R;


import java.util.List;

/**
 * Created by admin on 2017-06-30.
 */

public class StudentListAdapter extends BaseAdapter {
    private Context context;
    private List<StudentData> studentDataList;


    public StudentListAdapter(Context context, List<StudentData> studentDataList) {
        this.context = context;
        this.studentDataList = studentDataList;

    }

    @Override
    public int getCount() {
        return studentDataList.size();
    }

    @Override
    public Object getItem(int position) {
        return studentDataList.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(final int position, View convertView, ViewGroup parent) {

        View v = View.inflate(context, R.layout.student, null);

        TextView nameText = (TextView) v.findViewById(R.id.Student_nameText);
        nameText.setBackgroundColor(Color.rgb(255,255,255));
        TextView genderText = (TextView) v.findViewById(R.id.Student_genderText);
        genderText.setBackgroundColor(Color.rgb(255,255,255));
        TextView idText = (TextView) v.findViewById(R.id.Student_idText);
        idText.setBackgroundColor(Color.rgb(255,255,255));

        nameText.setText("이름 : " + studentDataList.get(position).getName());
        genderText.setText("(" + studentDataList.get(position).getGender()+ ")" );
        idText.setText("ID : " + studentDataList.get(position).getId());

        v.setTag(studentDataList.get(position).getId());

        Button button = (Button) v.findViewById(R.id.StudentInfoButton);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(context, ManageStudentInfoActivity.class);
                intent.putExtra("studentData", studentDataList.get(position));
                context.startActivity(intent);
            }
        });
        return v;
    }
}
