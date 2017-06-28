package com.example.administrator.healthking_teacher.Manage;


import android.support.annotation.IdRes;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Spinner;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.toolbox.Volley;
import com.example.administrator.healthking_teacher.Data.StudentData;
import com.example.administrator.healthking_teacher.Network.RegisterRequest;
import com.example.administrator.healthking_teacher.Network.ValidateRequest;
import com.example.administrator.healthking_teacher.R;

import org.json.JSONObject;

public class RegisterActivity extends AppCompatActivity {


    private EditText idText;
    private EditText passwordText;
    private EditText nameText;

    private RadioGroup genderGroup;

    private Button validateButton;
    private Button registerButton;

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

    private boolean validate =false;

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
        InitButtons();
    }

    private void InitTextViews() {

        idText = (EditText) findViewById(R.id.Register_idText);
        idText.setBackgroundColor(getResources().getColor(R.color.colorPrimary));
        passwordText = (EditText) findViewById(R.id.Register_passwordText);
        passwordText.setBackgroundColor(getResources().getColor(R.color.colorPrimary));
        nameText = (EditText) findViewById(R.id.Register_nameText);
        nameText.setBackgroundColor(getResources().getColor(R.color.colorPrimary));
    }

    private void InitRadioGroup() {

        genderGroup = (RadioGroup) findViewById(R.id.Register_genderGroup);
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
        validateButton = (Button) findViewById(R.id.Register_vallidateButton);
        validateButton.setBackgroundColor(getResources().getColor(R.color.colorWarning));
        validateButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                id = idText.getText().toString();

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

                ValidateRequest validateRequest = new ValidateRequest(id, GetValidateResponse());
                RequestQueue queue = Volley.newRequestQueue(RegisterActivity.this);
                queue.add(validateRequest);
            }
        });

        registerButton = (Button) findViewById(R.id.Register_registerButton);
        registerButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                id = idText.getText().toString();
                password = passwordText.getText().toString();
                name = nameText.getText().toString();
                gender = ((RadioButton) findViewById(genderGroup.getCheckedRadioButtonId())).getText().toString();
                school = schoolSpinner.getSelectedItem().toString();
                grade = gradeSpinner.getSelectedItem().toString();
                classroomNumber = classroomSpinner.getSelectedItem().toString();

                if (!validate) {
                    AlertDialog.Builder builder = new AlertDialog.Builder(RegisterActivity.this);
                    dialog = builder.setMessage("먼저 중복체크를 해주세요.")
                            .setNegativeButton("확인", null)
                            .create();
                    dialog.show();
                    return;
                }

                if (id.equals("") || password.equals("") || name.equals("")) {
                    AlertDialog.Builder builder = new AlertDialog.Builder(RegisterActivity.this);
                    dialog = builder.setMessage("빈 칸 없이 입력해주세요.")
                            .setNegativeButton("확인", null)
                            .create();
                    dialog.show();
                    return;
                }


                StudentData studentData = new StudentData(id, password, name, gender, school, grade, classroomNumber);
                RegisterRequest registerRequest = new RegisterRequest(studentData, GetRegisterResponse());
                RequestQueue queue = Volley.newRequestQueue(RegisterActivity.this);
                queue.add(registerRequest);

            }
        });

    }


    @Override
    protected void onStop() {
        super.onStop();
        if (dialog != null) {
            dialog.dismiss();
            dialog = null;
        }
    }
// network

    private Response.Listener<String> GetValidateResponse() {
        Response.Listener<String> responseListener = new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                try {
                    JSONObject jsonResponse = new JSONObject(response);
                    boolean success = jsonResponse.getBoolean("success");
                    if (success) {
                        AlertDialog.Builder builder = new AlertDialog.Builder(RegisterActivity.this);
                        dialog = builder.setMessage("사용할 수 있는 아이디입니다.")
                                .setPositiveButton("확인", null)
                                .create();
                        dialog.show();

                        idText.setEnabled(false);
                        idText.setBackgroundColor(getResources().getColor(R.color.colorGray));
                        validateButton.setBackgroundColor(getResources().getColor(R.color.colorGray));
                        validate = true;
                    } else {
                        AlertDialog.Builder builder = new AlertDialog.Builder(RegisterActivity.this);
                        dialog = builder.setMessage("사용할 수 없는 아이디입니다.")
                                .setNegativeButton("확인", null)
                                .create();
                        dialog.show();
                    }

                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        };

        return responseListener;
    }


    private Response.Listener<String> GetRegisterResponse() {
        Response.Listener<String> responseListener = new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                try {
                    JSONObject jsonResponse = new JSONObject(response);
                    boolean success = jsonResponse.getBoolean("success");
                    if (success) {
                        AlertDialog.Builder builder = new AlertDialog.Builder(RegisterActivity.this);
                        dialog = builder.setMessage("회원 등록에 성공했습니다.")
                                .setPositiveButton("확인", null)
                                .create();
                        dialog.show();
                        finish();
                    } else {
                        AlertDialog.Builder builder = new AlertDialog.Builder(RegisterActivity.this);
                        dialog = builder.setMessage("회원 등록에 실패했습니다.")
                                .setNegativeButton("확인", null)
                                .create();
                        dialog.show();
                    }

                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        };

        return responseListener;
    }
}
