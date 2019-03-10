using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.WSA.Input;
using UnityEngine.XR.WSA.WebCam;


public class GeustureTap : MonoBehaviour {


    /// <summary>
    /// HoloLens class to capture user gestures
    /// </summary>
    private GestureRecognizer recognizer;

    /// <summary>
    /// The analysis result text
    /// </summary>
    private TextMesh labelText;


    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    private GameObject newLabel;

    public GameObject ga1;
    public GameObject ga2;
    public GameObject ga3;
    public GameObject ga4;

    public Light l1;

    /// <summary>
    /// Called right after Awake
    /// </summary>
    void Start()
    {
        ga2.active = false;
        // Initialises user gestures capture 
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += TapHandler;
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }

    /// <summary>
    /// Respond to Tap Input.
    /// </summary>
    private void TapHandler(TappedEventArgs obj)
    {
        if (FocusedObject!=null)
        {
            // Create a sphere as new cursor
            if (newLabel!=null)
            {
                Destroy(newLabel);
            }
            newLabel = new GameObject();
           
            // Attach the label to the Main Camera
            newLabel.transform.parent = gameObject.transform;

            // Resize and position the new cursor
            newLabel.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            //newLabel.transform.localPosition = new Vector3(0, 0, 0);
            newLabel.transform.position = FocusedObject.transform.position;
           
             // Creating the text of the Label
            labelText = newLabel.AddComponent<TextMesh>();
            labelText.anchor = TextAnchor.MiddleCenter;
            labelText.alignment = TextAlignment.Center;
            labelText.tabSize = 4;
            labelText.fontSize = 50;
            if (FocusedObject.tag==null || FocusedObject.tag.Length<=0|| FocusedObject.tag=="Untagged")
            {
                labelText.text = "";
            }
            else
            {
                if (FocusedObject.tag == "a")
                {
                    if (l1.enabled)
                    {
                        l1.enabled = false;
                        ga3.active = false;
                        ga4.active = true;
                    }
                    else
                    {
                        l1.enabled = true;
                        ga3.active = true;
                        ga4.active = false;
                    }
                    return;
                }
                if (FocusedObject.tag == "配电箱")
                {

                    ga1.active = false;
                    ga2.active = true;
                    ga4.active = false;
                    return;
                }
                labelText.text = FocusedObject.tag; 
            }
        }
        
    }





}
