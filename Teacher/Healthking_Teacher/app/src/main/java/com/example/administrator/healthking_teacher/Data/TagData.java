package com.example.administrator.healthking_teacher.Data;

/**
 * Created by admin on 2017-06-20.
 */

public class TagData {

    private String id ; // 16진수 값

    public TagData(){
        this.id = "init";
    }

    public  TagData(String id){
        this.id = id;
    }

    public void SetTagId(String id){
        this.id = id;
    }
    public String GetTagId(){return this.id;}
}
