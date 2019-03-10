using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnClick : MonoBehaviour {


    public GameObject navigationObject;
    public GameObject taskObject;
    public GameObject ChatObject;
    public GameObject NetworkObject;
    public GameObject BianyaqiObject;
    public GameObject Panl3d;


    public GameObject fengji;
    public GameObject peidian;

    void Start()
    {      
    }

    public void BtnTask()
    {
        navigationObject.SetActive(false);
        taskObject.SetActive(true);
        ChatObject.SetActive(false);
        BianyaqiObject.SetActive(false);
        peidian.SetActive(false);
        fengji.SetActive(false);
        NetworkObject.SetActive(false);
        StartCoroutine(NetWork.instance.GetTask());
        Panl3d.SetActive(false);
        StaticData.NowState = 1;
    }

    public void CloseTask()
    {
        navigationObject.SetActive(true); 
        taskObject.SetActive(false);
        ChatObject.SetActive(false);
        BianyaqiObject.SetActive(false);
        peidian.SetActive(false);
        fengji.SetActive(false);
        NetworkObject.SetActive(false);
        Panl3d.SetActive(false);
        StaticData.NowState = 0;
    }


    public void BtnCamera()
    {
        navigationObject.SetActive(false);
        taskObject.SetActive(false);
        ChatObject.SetActive(true);
        BianyaqiObject.SetActive(false);
        peidian.SetActive(false);
        fengji.SetActive(false);
        NetworkObject.SetActive(true);
        Panl3d.SetActive(false);
        StaticData.NowState = 2;
    }

    public void Btn3D()
    {
        navigationObject.SetActive(false);
        taskObject.SetActive(false);
        ChatObject.SetActive(false);
        BianyaqiObject.SetActive(false);
        peidian.SetActive(false);
        fengji.SetActive(true);
        NetworkObject.SetActive(false);
        Panl3d.SetActive(true);
        StaticData.NowState = 3;
    }

    public void BtnGoHome()
    {
        navigationObject.SetActive(true);
        taskObject.SetActive(false);
        ChatObject.SetActive(false);
        BianyaqiObject.SetActive(false);
        peidian.SetActive(false);
        fengji.SetActive(false);
        NetworkObject.SetActive(false);
        Panl3d.SetActive(false);
        StaticData.NowState = 0;
    }

    public void BtnComputerVision()
    {
        navigationObject.SetActive(false);
        taskObject.SetActive(false);
        ChatObject.SetActive(false);
        BianyaqiObject.SetActive(false);
        peidian.SetActive(false);
        fengji.SetActive(false);
        NetworkObject.SetActive(false);
        Panl3d.SetActive(false);
        StaticData.NowState = 4;
    }


}
