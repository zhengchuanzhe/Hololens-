using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn3D : MonoBehaviour {

    public GameObject fengji;
    public GameObject nibian;
    public GameObject peidian;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnFengji()
    {
        fengji.SetActive(true);
        nibian.SetActive(false);
        peidian.SetActive(false);
    }

    public void OnNibian()
    {

        fengji.SetActive(false);
        nibian.SetActive(true);
        peidian.SetActive(false);
    }

    public void OnPeidian()
    {

        fengji.SetActive(false);
        nibian.SetActive(false);
        peidian.SetActive(true);
    }
}
