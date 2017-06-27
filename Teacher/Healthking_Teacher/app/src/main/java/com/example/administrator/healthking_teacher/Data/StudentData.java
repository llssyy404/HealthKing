package com.example.administrator.healthking_teacher.Data;

/**
 * Created by admin on 2017-06-20.
 */

public class StudentData {

    private String id; //아이디
    private String password; // 패스워드
    private String name;  //이름
    private String gender; //성별
    private String school; // 초,중,고등학교
    private String grade; //학년
    private String classroomNumber; //반

    public StudentData(String id, String password, String name, String gender, String school, String grade, String classroomNumber) {
        this.id = id;
        this.password = password;
        this.name = name;
        this.gender = gender;
        this.school = school;
        this.grade = grade;
        this.classroomNumber = classroomNumber;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    public String getSchool() {
        return school;
    }

    public void setSchool(String school) {
        this.school = school;
    }

    public String getGrade() {
        return grade;
    }

    public void setGrade(String grade) {
        this.grade = grade;
    }

    public String getClassroomNumber() {
        return classroomNumber;
    }

    public void setClassroomNumber(String classroomNumber) {
        this.classroomNumber = classroomNumber;
    }
}
