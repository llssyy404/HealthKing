using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    private GameObject[] obj = null;
    private int selNum = 0;
    private int prevNum = 0;

    // Use this for initialization
    void Start () {
        obj = new GameObject[11];
        obj[0] = GameObject.Find("Canvas").transform.Find("LoginPref").gameObject;
        obj[1] = GameObject.Find("Canvas").transform.Find("MainPref").gameObject;
        obj[2] = GameObject.Find("Canvas").transform.Find("BaseInformPref").gameObject;
        obj[3] = GameObject.Find("Canvas").transform.Find("PAPSPref").gameObject;
        obj[4] = GameObject.Find("Canvas").transform.Find("PAPS1").gameObject;
        obj[5] = GameObject.Find("Canvas").transform.Find("PAPS2").gameObject;
        obj[6] = GameObject.Find("Canvas").transform.Find("PAPS3").gameObject;
        obj[7] = GameObject.Find("Canvas").transform.Find("PAPS4").gameObject;
        obj[8] = GameObject.Find("Canvas").transform.Find("PAPS5").gameObject;
        obj[9] = GameObject.Find("Canvas").transform.Find("PAPSResultPref").gameObject;
        obj[10] = GameObject.Find("Canvas").transform.Find("FitnessUpTipPref").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickStartBtn(int sel)
    {
        prevNum = selNum;
        selNum = sel;
        if (obj[prevNum] != null) obj[prevNum].SetActive(false);
        if (obj[selNum] != null) obj[selNum].SetActive(true);
    }
}
