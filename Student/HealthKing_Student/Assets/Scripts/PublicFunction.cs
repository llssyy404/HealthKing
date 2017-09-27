using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicFunction {

	static public string ConvertTimeToString(int miliSecondTime)
    {
        int nowSec = miliSecondTime / 1000;
        int hour = nowSec / 3600;
        int min = (nowSec % 3600) / 60;
        int sec = nowSec % 60;
        int milSec = miliSecondTime % 1000;

        string str = "";
        if (hour > 0)
            str += hour.ToString() + "시";
        if (min > 0)
            str += min.ToString() + "분";
        if (sec > 0)
            str += sec.ToString() + "초";
        if (milSec > 0)
            str += milSec.ToString();

        return str;
    }
}
