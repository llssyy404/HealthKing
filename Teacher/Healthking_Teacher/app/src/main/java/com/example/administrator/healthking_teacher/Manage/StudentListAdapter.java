package com.example.administrator.healthking_teacher.Manage;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;

import com.android.volley.Response;
import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.R;

import org.json.JSONObject;

import java.util.List;

/**
 * Created by admin on 2017-06-30.
 */

public class StudentListAdapter extends BaseAdapter
{
    private Context context;
    private List<StudentData> studentDataList;
    private Activity parentActivity;

    public StudentListAdapter(Context context, List<StudentData> studentDataList, Activity parentActivity) {
        this.context = context;
        this.studentDataList = studentDataList;
        this.parentActivity = parentActivity;
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

        Button button = (Button) v.findViewById(R.id.StudentInfoButton);
        button.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
//                    Response.Listener<String> responseListener = new Response.Listener<String>() {
//                        @Override
//                        public void onResponse(String response) {
//                            try {
//                                JSONObject jsonResponse = new JSONObject(response);
//                                boolean success = jsonResponse.getBoolean("success");
//                                if(success)
//                                {
                                    //studentDataList.remove(i);
                                    Intent intent = new Intent(parentActivity, ManageStudentInfoActivity.class);
                    //intent.putExtra("stuText", item);
                    intent.putExtra("stuText", studentDataList.get(position));

                                    parentActivity.startActivity(intent);
//                                }
//                            }
//                            catch (Exception e)
//                            {
//                                e.printStackTrace();
//                            }
//                        }

//                };
            }
        });
        return  v;
    }
}
