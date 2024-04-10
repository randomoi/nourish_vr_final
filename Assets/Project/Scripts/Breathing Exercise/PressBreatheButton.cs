using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class BreatheButtonPress : MonoBehaviour
{
    // includes ref to BreathingExerciseController
    public BreathingExerciseController breathingExercise;

    protected void OnEnable()
    {
        // adds event listener to selectEntered
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(TriggerExercise);
    }

    protected void OnDisable()
    {
        // removes event listener from selectEntered 
        GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(TriggerExercise);
    }

    // makes a call when button is clicked/pressed
    private void TriggerExercise(SelectEnterEventArgs args)
    {
        // calls StartBreathingExercise method 
        if (breathingExercise != null)
            breathingExercise.StartBreathingExercise();
    }
}

// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/manual/xr-simple-interactable.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
//https://forum.unity.com/threads/using-xrbaseinteractor-selectentered.1457695/
//https://forum.unity.com/threads/how-to-prevent-interactable-from-being-selected-while-in-socket.1317246/
//https://www.youtube.com/watch?v=lPPa9y_czlE
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDisable.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.RemoveListener.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html




