using UnityEngine;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class SceneAudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    // handles playing sound when scene starts
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No AudioSource/Audio was found.", this);
        }
    }

    // handles stopping of sound (needed to stop overlap of video and breathing exercise instructions)
    public void StopSceneSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/AudioSource.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
//https://docs.unity3d.com/ScriptReference/AudioSource.Stop.html
//https://docs.unity3d.com/ScriptReference/Debug.LogError.html
//https://forum.unity.com/threads/audio-source-returning-null.1437709/
//https://discussions.unity.com/t/audio-source-is-null-when-changing-scene/218356
