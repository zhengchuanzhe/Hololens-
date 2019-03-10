using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItemManager : MonoBehaviour {


    public string taskDetail;
    

    public GameObject taskDetailObject;

    // Use this for initialization
    void Start() {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnClick()
    {
        GameObject parent= GameObject.Find("/Canvas/Task");
        GameObject go = Instantiate(taskDetailObject, parent.transform);
        //go.transform.parent = parent.transform;
        //go.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1);
        //go.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        //go.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        //go.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f,16f,0f);
        Text tt = go.transform.GetChild(1).GetComponent<Text>();
        tt.text = taskDetail;
        TaskItemManager item = go.GetComponent<TaskItemManager>();

        Debug.Log("详情：" + taskDetail);
    }
}
