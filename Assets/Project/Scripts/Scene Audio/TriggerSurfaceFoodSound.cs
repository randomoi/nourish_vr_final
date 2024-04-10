using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class TriggerSurfaceFoodSound : MonoBehaviour
{
    // to be assigned in Unity inpspector (sound played on contact with surface)
    public AudioClip contactSound; 
    private AudioSource audioSource;
    private bool hasBeenGrabbed = false; // tracks if food item was picked up
    private XRGrabInteractable interactableGrabObject; // ref to XRGrabInteractable 

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        interactableGrabObject = GetComponent<XRGrabInteractable>();
        if (interactableGrabObject != null)
        {
            interactableGrabObject.selectEntered.AddListener(OnItemGrab);
            interactableGrabObject.selectExited.AddListener(OnItemPutDown);
        }
    }

    // handles clean up
    private void OnDestroy()
    {
        if (interactableGrabObject != null)
        {
            interactableGrabObject.selectEntered.RemoveListener(OnItemGrab);
            interactableGrabObject.selectExited.RemoveListener(OnItemPutDown);
        }
    }

    // sets flag to true
    private void OnItemGrab(SelectEnterEventArgs arg)
    {
        hasBeenGrabbed = true;
    }

    // need this otherwise no sound
    private void OnItemPutDown(SelectExitEventArgs arg)
    {
         //hasBeenGrabbed = false;
    }

    // handles playing sound only if item is not on the SmartPlate (collision)
    void OnCollisionEnter(Collision other)
    {
        if (hasBeenGrabbed && !other.gameObject.CompareTag("Plate"))
        {
            audioSource.PlayOneShot(contactSound);
            // resets grab state 
            hasBeenGrabbed = false;
        }
    }
}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/AudioSource.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html
//https://docs.unity3d.com/ScriptReference/Object.Destroy.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
//https://docs.unity3d.com/ScriptReference/Debug.LogError.html
//https://forum.unity.com/threads/audio-source-returning-null.1437709/
//https://discussions.unity.com/t/audio-source-is-null-when-changing-scene/218356
//https://docs.unity3d.com/ScriptReference/AudioClip.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.html
//https://docs.unity3d.com/ScriptReference/GameObject.AddComponent.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.0/api/UnityEngine.XR.Interaction.Toolkit.SelectExitEvent.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.RemoveListener.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectExitEventArgs.html
//https://docs.unity3d.com/ScriptReference/GameObject.CompareTag.html
//https://forum.unity.com/threads/what-does-other-mean.182025/
