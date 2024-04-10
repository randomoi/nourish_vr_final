using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class TriggerSurfaceItemSound : MonoBehaviour
{
    // to be assigned in Unity inpspector (sound played on contact with surface)
    public AudioClip putDownSound;
    private AudioSource audioSource;
    private XRGrabInteractable interactableGrabObject; // ref to XRGrabInteractable 
    private bool hasBeenGrabbed = false; // tracks if item was picked up
    private bool soundReadyToPlay = false; // makes sure audio plays only after being grabbed

    void Awake()
    {
        SetupAudioSource();
        SetupGrabInteractable();
    }

    // handles audio source setup
    private void SetupAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    // handles setup
    private void SetupGrabInteractable()
    {
        interactableGrabObject = GetComponent<XRGrabInteractable>();
        if (interactableGrabObject != null)
        {
            interactableGrabObject.selectEntered.AddListener(OnGrabbedItem);
            interactableGrabObject.selectExited.AddListener(OnItemRelease);
        }
    }

    // handles clean up
    private void OnDestroy()
    {
        if (interactableGrabObject != null)
        {
            interactableGrabObject.selectEntered.RemoveListener(OnGrabbedItem);
            interactableGrabObject.selectExited.RemoveListener(OnItemRelease);
        }
    }

    // handles flags
    private void OnGrabbedItem(SelectEnterEventArgs args)
    {
        hasBeenGrabbed = true;
        soundReadyToPlay = false; // resets flag to make sure sound is not played after item was grabbed
    }

    // handles when item is released
    private void OnItemRelease(SelectExitEventArgs args)
    {
        hasBeenGrabbed = false;
        Invoke(nameof(EnableSoundPlayback), 0.05f); // this is a delay to avoid scene load issues
    }

    private void EnableSoundPlayback()
    {
        if (!hasBeenGrabbed) // checks to make sure items hasn't been grabbed during set delay
        {
            soundReadyToPlay = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // checks if item was previously grabbed; checks if is ready to play sound; checks is has collided with surface
        if (soundReadyToPlay && !hasBeenGrabbed && audioSource && putDownSound)
        {
            audioSource.PlayOneShot(putDownSound);
            soundReadyToPlay = false; // resets to avoid playing sound until next cycle
        }
    }
}

// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/AudioSource.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
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
//https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
//https://docs.unity3d.com/ScriptReference/AudioSource.PlayOneShot.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Invoke.html
//https://forum.unity.com/threads/sound-not-playing-on-box-collision.1142656/
//https://discussions.unity.com/t/play-sound-on-collision-doesnt-work/159523

