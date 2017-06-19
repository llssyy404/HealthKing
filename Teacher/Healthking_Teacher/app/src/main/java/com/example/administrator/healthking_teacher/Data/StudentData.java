package com.example.administrator.healthking_teacher.Data;

/**
 * Created by admin on 2017-06-20.
 */

public class StudentData {

    private int grade; //학년
    private int classNumber; //반
    private String name;  //이름

    public StudentData(int grade, int classNumber, String name) {
        this.grade = grade;
        this.classNumber = classNumber;
        this.name = name;
    }

    public int getGrade() {
        return grade;
    }

    public void setGrade(int grade) {
        this.grade = grade;
    }

    public int getClassNumber() {
        return classNumber;
    }

    public void setClassNumber(int classNumber) {
        this.classNumber = classNumber;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
