using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchAction : MonoBehaviour, IManipulationHandler
{


    public GameObject taskGameObject;

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        taskGameObject.active = !taskGameObject.active;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
         
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
         
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        
    }
}
