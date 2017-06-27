package com.example.administrator.healthking_teacher.Manage;

import android.content.Context;
import android.support.annotation.IdRes;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Spinner;

import com.example.administrator.healthking_teacher.R;

public class RegisterActivity extends AppCompatActivity {


    private ArrayAdapter schoolAdapter;
    private Spinner schoolSpinner;

    private ArrayAdapter gradeAdapter;
    private Spinner gradeSpinner;

    private ArrayAdapter classroomAdapter;
    private Spinner classroomSpinner;

    private AlertDialog dialog;

    private String id; //아이디
    private String password; // 패스워드
    private String name;  //이름
    private String gender; //성별
    private String school; // 초,중,고등학교
    private String grade; //학년
    private String classroomNumber; //반
    private boolean validate;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        Init();
    }

    private void Init() {
        InitTextViews();
        InitRadioGroup();
        InitSpinners();

    }

    private void InitTextViews() {

        final EditText idText = (EditText) findViewById(R.id.Register_idText);
        final EditText passwordText = (EditText) findViewById(R.id.Register_passwordText);
        final EditText nameText = (EditText) findViewById(R.id.Register_nameText);
    }

    private void InitRadioGroup() {
        final RadioGroup genderGroup = (RadioGroup) findViewById(R.id.Register_genderGroup);
        int genderGroupID = genderGroup.getCheckedRadioButtonId();
        gender = ((RadioButton) findViewById(genderGroupID)).getText().toString();

        genderGroup.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup group, @IdRes int checkedId) {
                RadioButton genderButton = (RadioButton) findViewById(checkedId);
                gender = genderButton.getText().toString();
            }
        });

    }

    private void InitSpinners() {
        schoolSpinner = (Spinner) findViewById(R.id.Register_schoolSpinner);
        schoolAdapter = ArrayAdapter.createFromResource(this, R.array.schoolType, android.R.layout.simple_spinner_dropdown_item);
        schoolSpinner.setAdapter(schoolAdapter);

        gradeSpinner = (Spinner) findViewById(R.id.Register_gradeSpinner);
        gradeAdapter = ArrayAdapter.createFromResource(this, R.array.elementarySchoolGrade, android.R.layout.simple_spinner_dropdown_item);
        gradeSpinner.setAdapter(gradeAdapter);

        classroomSpinner = (Spinner) findViewById(R.id.Register_classroomNumberSpinner);
        classroomAdapter = ArrayAdapter.createFromResource(this, R.array.classroomNumber, android.R.layout.simple_spinner_dropdown_item);
        classroomSpinner.setAdapter(classroomAdapter);


        schoolSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if (schoolSpinner.getSelectedItem().equals("초등학교")) {
                    gradeAdapter = ArrayAdapter.createFromResource(view.getContext(), R.array.elementarySchoolGrade, android.R.layout.simple_spinner_dropdown_item);
                    gradeSpinner.setAdapter(gradeAdapter);
                } else if (schoolSpinner.getSelectedItem().equals("중학교")) {
                    gradeAdapter = ArrayAdapter.createFromResource(view.getContext(), R.array.middleSchoolGrade, android.R.layout.simple_spinner_dropdown_item);
                    gradeSpinner.setAdapter(gradeAdapter);
                }
                if (schoolSpinner.getSelectedItem().equals("고등학교")) {
                    gradeAdapter = ArrayAdapter.createFromResource(view.getContext(), R.array.highSchoolGrade, android.R.layout.simple_spinner_dropdown_item);
                    gradeSpinner.setAdapter(gradeAdapter);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }

    private void InitButtons() {
        final Button validateButton = (Button) findViewById(R.id.Register_vallidateButton);
        validateButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (validate) {
                    return;
                }
                if (id.equals("")) {
                    AlertDialog.Builder builder = new AlertDialog.Builder(RegisterActivity.this);
                    dialog = builder.setMessage("아이디는 빈 칸일 수 없습니다.")
                            .setPositiveButton("확인", null)
                            .create();
                    dialog.show();
                    return;
                }

                //중복체크 구간
            }
        });
    }

}
