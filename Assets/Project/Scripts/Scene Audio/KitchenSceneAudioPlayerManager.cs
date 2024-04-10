using UnityEngine;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class KitchenSceneAudioPlayerManager : MonoBehaviour
{
    public static KitchenSceneAudioPlayerManager Instance { get; private set; }

    private AudioSource audioSource;

    // this was necessary to make the sound stop when breathing exercise started
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // handles starting sound
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

    // handles stopping ambiance sound in the kitchen
    public void StopKitchenSceneSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // handles restarting of audio (ambiance sound)
    public void RestartKitchenSceneSound(float startSoundAtSecondsPosition = 8f)
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.time = startSoundAtSecondsPosition; // sets to play at designated time position to avoid starting at instructions for scene
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No AudioSource/Audio was found.", this);
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
