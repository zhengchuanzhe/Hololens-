using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class TapManage : MonoBehaviour {

    [Tooltip("taskGageObject")]
    public GameObject taskGameObject;
    //任务下拉栏
    [Tooltip("taskScrollbar")]
    public GameObject taskScrollbarObject;


    public GameObject  bianyaqiObject;
    //下拉速度
    private float  taskSpeed = 0.1f;
    //下拉栏对象
    private Scrollbar scrollbarobject;

    private GestureRecognizer gestureRecognizer;
    // Use this for initialization
    void Start () {
        //  创建GestureRecognizer实例
        gestureRecognizer = new GestureRecognizer();
        //  注册指定的手势类型,本例指定单击及双击手势类型
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap
            | GestureSettings.DoubleTap);
        //  订阅手势事件
        gestureRecognizer.NavigationUpdated += GestureRecognizer_NavigationUpdated;
        gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;
        //  开始手势识别
        gestureRecognizer.StartCapturingGestures();
        scrollbarobject = taskScrollbarObject.GetComponentInChildren<Scrollbar>();


    }
    

    private void GestureRecognizer_NavigationUpdated(NavigationUpdatedEventArgs obj)
    {
        scrollbarobject.value = obj.normalizedOffset.y* taskSpeed;
    }

    private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (tapCount == 1)
        {
            onTipClick();
        }
        else
        {
            onDoubleClick();
        }
        
    }


    private void onTipClick() {
       
    }


    private void onDoubleClick(){
        bianyaqiObject.SetActive(false);
        taskGameObject.active = !taskGameObject.active;
    }
}
