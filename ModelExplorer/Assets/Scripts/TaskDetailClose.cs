using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDetailClose : MonoBehaviour {


    public GameObject taskDetail;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Close()
    {
        Destroy(taskDetail);
    }
}
