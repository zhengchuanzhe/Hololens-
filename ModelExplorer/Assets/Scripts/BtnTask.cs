using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnTask : MonoBehaviour
{

    public GameObject navigationObject;
    public GameObject taskObject;

    // Use this for initialization
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);

    }


    public void OnClick()
    {
        //navigationObject.active = false;
        //taskObject.active = false;
        StartCoroutine(NetWork.instance.GetTask());
    }
}
