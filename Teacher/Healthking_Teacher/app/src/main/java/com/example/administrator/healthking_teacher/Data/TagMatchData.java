package com.example.administrator.healthking_teacher.Data;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by juicy on 2017-07-04.
 */

public class TagMatchData {
    private List<StudentRecordData> studentRecordDataList;
    private List<TagData> tagDatalist;

    static private TagMatchData _instance;

    static public TagMatchData getInstance() {
        if (_instance == null) {
            _instance = new TagMatchData();
            _instance.Init();
        }
        return _instance;
    }
    private void Init() {
        tagDatalist = new ArrayList<>();
        studentRecordDataList = new ArrayList<>();
    }

    public void SetList(StudentRecordData studentData, TagData tagdata){
        studentRecordDataList.add(studentData);
        tagDatalist.add(tagdata);
    }
    public List<TagData> GetTagDataList(){
        return tagDatalist;
    }
    public List<StudentRecordData> GetStudentRecordData(){
        return studentRecordDataList;
    }
}
