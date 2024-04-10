using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class BreathingExerciseController : MonoBehaviour
{
    // to be assigned in Unity inspector  
    public Image targetIcon; // starting icon indicator where to look
    public AudioClip audioInstructions; // instructions to breathe
    private AudioSource audioSource; // AudioSource component 

    // to be assigned in Unity inspector  
    public ParticleSystem inhalationParticleSystem; // inhalation particle system
    public ParticleSystem exhalationParticleSystem; // exhalation particle system

    private float inhalationDuration = 3f; // sets duration for inhale 
    private float exhalationDuration = 3f; // sets duration for exhale 

    private Transform transformCamera;

    private float inhalationDistance = 3f; // sets inhalation distance in front of camera 
    private float exhalationDistance = 0.1f; // sets exhalation distance in front of camera 

    // offsets for position of icon
    private float iconDistance = 1f; // sets icon distance
    private float iconVerticalOffset = -0.1f; // sets verticle offset (to bring it lower relative to camera)

    // manipulates lighting for when breathing excersise is on/off
    public Light[] lightsToTurnOff; // assign in Unity inspector which lights to turn off

    // manipulates skyboxes for when breathing excersise is on/off
    public Material nightSkybox; // night sky skybox
    private Material daySkybox; // trees skybox 

    // checks if exercise started to prevent starting again (if user clicked twice on the button before exercise ends)
    private bool isBreathingExerciseInProgress = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transformCamera = Camera.main.transform;

        inhalationParticleSystem.Stop();
        exhalationParticleSystem.Stop();

        targetIcon.gameObject.SetActive(false);

        // original Skybox 
        daySkybox = RenderSettings.skybox;
    }

    // handles starting exercise
    public void StartBreathingExercise()
    {
        // checks if exercise is in progress
        if (isBreathingExerciseInProgress)
        {
            return; // exits if is already started
        }

        isBreathingExerciseInProgress = true; // sets flag to true 

        // stops scene audio 
        KitchenSceneAudioPlayerManager.Instance.StopKitchenSceneSound();

        // if audioSourse and instructions available, plays audio instructions sound
        if (audioSource != null && audioInstructions != null)
        {
            audioSource.PlayOneShot(audioInstructions);
        }

        // displays target icon
        PositionTargetIcon(targetIcon.gameObject, iconDistance, iconVerticalOffset);
        targetIcon.gameObject.SetActive(true);

        // turn off lighthing
        ChangeLightingIntensity(0); // 0 = turn off
        RenderSettings.skybox = nightSkybox; // updates skybox to night sky
        DynamicGI.UpdateEnvironment(); // updates global illumination 

        StartCoroutine(StartBreathingExerciseAfterDelay());
    }

    // handles exercise delay
    IEnumerator StartBreathingExerciseAfterDelay()
    {
        // shows start icon with with designated position
        PositionTargetIcon(targetIcon.gameObject, iconDistance, iconVerticalOffset);

        yield return new WaitForSeconds(5); // delay

        // hide icon and begins breathing exercise
        targetIcon.gameObject.SetActive(false);

        StartCoroutine(BreathingExerciseCycle());
    }

    // handles breathing cycle
    IEnumerator BreathingExerciseCycle()
    {
        float totalElapsedTime = 0f; // elapsed time
        float maximumDuration = 30f; // duration of breathing exercise
        float inhaleExhaleDelay = 2.0f; // delay between inhale and exhale

        while (totalElapsedTime < maximumDuration)
        {
            // inhalation cycle
            ParticleSystemsPosition(inhalationParticleSystem, inhalationDistance, true);
            inhalationParticleSystem.Play();
            yield return new WaitForSeconds(inhalationDuration);
            inhalationParticleSystem.Stop();

            // delay to create illusion of sequensial breathing (allows inhale particles to disappear)
            yield return new WaitForSeconds(inhaleExhaleDelay);

            totalElapsedTime += inhalationDuration + inhaleExhaleDelay;
            if (totalElapsedTime >= maximumDuration) break;

            // exhalation cycle
            ParticleSystemsPosition(exhalationParticleSystem, exhalationDistance, false);
            exhalationParticleSystem.Play();
            yield return new WaitForSeconds(exhalationDuration);
            exhalationParticleSystem.Stop();

            yield return new WaitForSeconds(inhaleExhaleDelay);

            totalElapsedTime += exhalationDuration + inhaleExhaleDelay;

            if (totalElapsedTime + inhalationDuration > maximumDuration) break;
        }

        isBreathingExerciseInProgress = false; // sets flag to false

        // after finishing while loop restarts ambiance sound (at seconds 8 to start at music not speech)
        KitchenSceneAudioPlayerManager.Instance.RestartKitchenSceneSound();

        // after breathing exercise
        ChangeLightingIntensity(1); // turns lights on 
        RenderSettings.skybox = daySkybox; // updates to original skybox material 
        DynamicGI.UpdateEnvironment(); // update GI to change back to original skybox
    }

    // adjusts lighting intensity
    private void ChangeLightingIntensity(float intensity)
    {
        foreach (Light light in lightsToTurnOff)
        {
            light.intensity = intensity;
        }
    }

    // handles position of target icon
    private void PositionTargetIcon(GameObject icon, float distance, float offsetVertical)
    {
        if (transformCamera == null) return;

        Vector3 iconPositionInFrontOfCamera = transformCamera.position
                                           + transformCamera.forward * distance
                                           + transformCamera.up * offsetVertical;
        icon.transform.position = iconPositionInFrontOfCamera;

        // makes sure that icon faces camera
        icon.transform.LookAt(icon.transform.position + transformCamera.rotation * Vector3.forward, transformCamera.rotation * Vector3.up);
    }

    // handles position of particles
    private void ParticleSystemsPosition(ParticleSystem particleSystem, float distance, bool isInhaling)
    {
        if (transformCamera == null) return;

        Vector3 positionCamera = transformCamera.position;

        // height offsets
        float inhaleHeightOffset = -0.5f;
        float exhaleHeightOffset = -0.1f;
        float offsetHeight = isInhaling ? inhaleHeightOffset : exhaleHeightOffset;

        // horizontal offets
        float inhaleHorizontalOffset = 0.1f;
        float exhaleHorizontalOffset = 0.0f;
        float horizontalOffset = isInhaling ? inhaleHorizontalOffset : exhaleHorizontalOffset;

        // camera position
        positionCamera.y += offsetHeight;
        positionCamera += transformCamera.right * horizontalOffset;

        Vector3 updatedPosition = positionCamera + transformCamera.forward * distance;
        particleSystem.transform.position = updatedPosition;

        Vector3 rotationOriginal = particleSystem.transform.eulerAngles;
        float yRotation = transformCamera.eulerAngles.y + (isInhaling ? 180 : 0);
        particleSystem.transform.rotation = Quaternion.Euler(rotationOriginal.x, yRotation, rotationOriginal.z);
    }
}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/AudioSource.html
//https://docs.unity3d.com/ScriptReference/Camera-main.html
//https://docs.unity3d.com/ScriptReference/AudioSource.Stop.html
//https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
//https://docs.unity3d.com/ScriptReference/RenderSettings-skybox.html
//https://discussions.unity.com/t/what-is-an-instance-of-a-gameobject/72949
//https://discussions.unity.com/t/acessing-a-script-instance-from-another-object/40623
//https://docs.unity3d.com/ScriptReference/AudioSource.PlayOneShot.html
//https://docs.unity3d.com/ScriptReference/Light-intensity.html
//https://docs.unity3d.com/ScriptReference/DynamicGI.UpdateEnvironment.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
//https://docs.unity3d.com/ScriptReference/WaitForSeconds.html
//https://docs.unity3d.com/550/Documentation/ScriptReference/GameObject.SetActive.html
//https://forum.unity.com/threads/turning-on-off-multiple-lights-at-once.165717/
//https://discussions.unity.com/t/lights-on-and-off-loop/137642
//https://forum.unity.com/threads/delayed-set-active.342487/
//https://stackoverflow.com/questions/52999487/start-coroutine-on-an-inactive-de-activated-gameobject
//https://discussions.unity.com/t/coroutine-not-working-even-when-game-object-is-active/232143
//https://gamedev.stackexchange.com/questions/156667/everything-after-the-yield-return-new-wait-for-seconds-is-not-runnung-unity
//https://docs.unity3d.com/ScriptReference/Transform-position.html
//https://forum.unity.com/threads/how-to-make-object-match-position-with-camera.918788/
//https://forum.unity.com/threads/c-change-camera-target-object-problem.151368/
//https://discussions.unity.com/t/my-camera-stays-in-the-same-position-regardless-of-any-variables-i-change/65034
//https://forum.unity.com/threads/camera-get-position-vector3-solved.59051/
//https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
//https://discussions.unity.com/t/question-regarding-to-transform-lookat/251234
//https://stackoverflow.com/questions/59039958/how-can-a-get-the-camera-to-move-smoothly-to-a-new-vector3-position-on-the-click
//https://discussions.unity.com/t/how-do-i-make-the-camera-follow-a-vector3-3d/223403
//https://discourse.threejs.org/t/camera-transform-from-unity-to-three/18903
//https://docs.unity3d.com/ScriptReference/Camera.WorldToScreenPoint.html
//https://docs.unity3d.com/ScriptReference/Camera.ViewportToWorldPoint.html
//https://stackoverflow.com/questions/75984333/bring-gameobject-to-the-position-of-camera
//https://docs.unity3d.com/ScriptReference/Quaternion-eulerAngles.html
//https://discussions.unity.com/t/how-does-one-use-quaternion-euler-x-y-z-to-rotate-on-y-axis/106681
//https://forum.unity.com/threads/quaternions-and-particle-collisions.1084151/
//https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
//https://docs.unity3d.com/ScriptReference/Light.html
//https://forum.unity.com/threads/convert-position-from-one-cameras-perspective-to-another.1387248/
//https://docs.unity3d.com/ScriptReference/ParticleSystem.html
//https://docs.unity3d.com/ScriptReference/ParticleSystem.Play.html
//https://forum.unity.com/threads/how-to-start-particle-system-with-script.938336/
//https://discussions.unity.com/t/how-play-and-stop-a-particlesystem-through-scripting/83652
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://www.youtube.com/watch?v=ysg9oaZEgwc
//https://academy.zenva.com/product/the-complete-virtual-reality-game-development-course/

