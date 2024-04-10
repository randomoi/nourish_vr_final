using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class VideoController : MonoBehaviour
{
    public VideoPlayer instructionsVideoPlayer;
    public Button playVideoButton;
    public Image buttonIcon; 

    public SceneAudioPlayer sceneAudioPlayer;


    void Start()
    {
        // assigns instructionsVideoPlayer
        if (instructionsVideoPlayer == null)
        {
            instructionsVideoPlayer = GetComponent<VideoPlayer>();
        }

        // assigns playVideoButton
        if (playVideoButton == null)
        {
            playVideoButton = GetComponentInChildren<Button>();
        }

        // gets icon from playVideoButton
        if (playVideoButton != null && buttonIcon == null)
        {
            buttonIcon = playVideoButton.GetComponentInChildren<Image>();
            if (buttonIcon == null)
            {
                Debug.LogError("Icon not found on the play button.");
                return;
            }
        }

        playVideoButton.onClick.AddListener(PlayVideo);

        // subscribes to player events
        instructionsVideoPlayer.started += OnVideoStarted;
        // detects when video ends
        instructionsVideoPlayer.loopPointReached += OnVideoStopped; 
    }

    // to avoid memory leaks unsubscribes from player events
    void OnDestroy()
    {
        if (instructionsVideoPlayer != null)
        {
            instructionsVideoPlayer.started -= OnVideoStarted;
            instructionsVideoPlayer.loopPointReached -= OnVideoStopped;
        }
    }

    // checks if scene audio plays, stops it if it does and plays video
    public void PlayVideo()
    {
        if (instructionsVideoPlayer == null)
        {
            Debug.LogError("instructionsVideoPlayer not found.");
            return;
        }

        // stops scene audio if it's playing
        if (sceneAudioPlayer != null)
        {
            sceneAudioPlayer.StopSceneSound();
        }

        if (instructionsVideoPlayer.isPlaying)
        {
            instructionsVideoPlayer.Pause();
            
            buttonIcon.enabled = true; // updates button 
        }
        else
        {
            instructionsVideoPlayer.Play();
        }
    }

    // handles icon display on start
    private void OnVideoStarted(VideoPlayer source)
    {
        buttonIcon.enabled = false;
    }

    // handles icon display on end
    private void OnVideoStopped(VideoPlayer source)
    {
        buttonIcon.enabled = true;
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
//https://docs.unity3d.com/ScriptReference/Video.VideoPlayer.html
//https://docs.unity3d.com/Manual/class-VideoPlayer.html
//https://docs.unity3d.com/2018.2/Documentation/ScriptReference/UI.Image.html
//https://forum.unity.com/threads/reference-videoplayer-component-script-question.485897/
//https://docs.unity3d.com/ScriptReference/Component.GetComponentInChildren.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/ScriptReference/Debug.LogError.html
//https://docs.unity3d.com/2018.3/Documentation/ScriptReference/UI.Button-onClick.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDestroy.html
//https://discussions.unity.com/t/how-to-enable-disable-ui-buttons/140438
//https://docs.unity3d.com/2018.2/Documentation/ScriptReference/UI.Button.html
//https://docs.unity3d.com/Manual/NullReferenceException.html
//https://discussions.unity.com/t/how-to-check-if-something-is-null/16399