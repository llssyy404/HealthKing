package com.example.administrator.healthking_teacher.Manage;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.R;

import java.util.List;

/**
 * Created by admin on 2017-06-30.
 */

public class StudentListAdapter extends BaseAdapter
{
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
    public View getView(int position, View convertView, ViewGroup parent) {

        View v = View.inflate(context, R.layout.student,null);

        TextView nameText = (TextView) v.findViewById(R.id.Student_nameText);
        nameText.setBackgroundColor(v.getResources().getColor(R.color.colorPrimary));
        TextView genderText = (TextView) v.findViewById(R.id.Student_genderText);
        genderText.setBackgroundColor(v.getResources().getColor(R.color.colorPrimary));
        TextView idText = (TextView) v.findViewById(R.id.Student_idText);
        idText.setBackgroundColor(v.getResources().getColor(R.color.colorPrimary));

        nameText.setText(studentDataList.get(position).getName());
        genderText.setText(studentDataList.get(position).getGender());
        idText.setText(studentDataList.get(position).getId());

        v.setTag(studentDataList.get(position).getId());

        return  v;
    }
}
