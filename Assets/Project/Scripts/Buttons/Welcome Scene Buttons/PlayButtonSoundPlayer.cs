using UnityEngine;
using UnityEngine.UI;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class PlayButtonSoundPlayer : MonoBehaviour
{
    public AudioClip soundClip; // to be assigned in Unity inspector

    private void Start()
    {
        // gets button 
        Button button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Doesnt have button component.", this);
            return;
        }
        // adds PlayAudio to onClick event
        button.onClick.AddListener(PlayAudio);
    }

    private void PlayAudio()
    {
        // plays  sounClip at position
        if (soundClip != null)
        {
            AudioSource.PlayClipAtPoint(soundClip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogError("No audio is assigned in Unity inspector.", this);
        }
    }
}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/AudioSource-clip.html
//https://docs.unity3d.com/2017.3/Documentation/ScriptReference/UI.Button.html
//https://discussions.unity.com/t/how-do-i-access-a-button-component/134374
//https://docs.unity3d.com/2018.4/Documentation/ScriptReference/UI.Button-onClick.html
//https://docs.unity3d.com/ScriptReference/AudioSource.PlayClipAtPoint.html
//https://docs.unity3d.com/2020.1/Documentation/ScriptReference/Transform-position.html
//https://docs.unity3d.com/ScriptReference/Debug.LogError.html
