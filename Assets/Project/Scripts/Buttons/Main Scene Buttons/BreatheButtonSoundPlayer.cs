using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class BreatheButtonSoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private XRBaseInteractable interactableComponent;

    void Start()
    {
        // gets AudioSource 
        audioSource = GetComponent<AudioSource>();

        // gets XRBaseInteractable 
        interactableComponent = GetComponent<XRBaseInteractable>();

        // subscribes to selectEntered
        interactableComponent.selectEntered.AddListener(PlaySound);
    }

    private void PlaySound(SelectEnterEventArgs args)
    {
        // plays audio
        audioSource.Play();
    }

    // to avoid memory leaks unsubscribes 
    private void OnDestroy()
    {
        if (interactableComponent != null)
        {
            interactableComponent.selectEntered.RemoveListener(PlaySound);
        }
    }
}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
//https://forum.unity.com/threads/audiosource-play-problem.1399312/
//https://docs.unity3d.com/ScriptReference/AudioSource.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.AddListener.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.RemoveListener.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDestroy.html
//https://stackoverflow.com/questions/58340949/audio-clip-not-playing-after-collision
//https://discussions.unity.com/t/playing-audio-on-collision/161497
//https://www.youtube.com/watch?v=lPPa9y_czlE