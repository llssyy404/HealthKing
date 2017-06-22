using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour {
    PAPSTableManager papsTableManager;
    // Use this for initialization
    void Start () {
        papsTableManager = new PAPSTableManager();
        papsTableManager.ReadTableFile();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
