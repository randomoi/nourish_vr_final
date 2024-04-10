using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.XR;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class PopupButtonsHapticFeedback : MonoBehaviour
{
    // to be assigned in Unity inspector
    public Button yesButton; 
    public Button noButton; 

    void Start()
    {
        // adds listeners to buttons
        if (yesButton != null)
        {
            yesButton.onClick.AddListener(() => ButtonClicked(yesButton));
        }

        if (noButton != null)
        {
            noButton.onClick.AddListener(() => ButtonClicked(noButton));
        }
    }

    void ButtonClicked(Button button)
    {
        Debug.Log(button.name + " was clicked.");
        TriggerHapticFeedback();
    }

    // handles haptics feedback on left controller
    void TriggerHapticFeedback()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, inputDevices);
        if (inputDevices.Count > 0)
        {
            InputDevice device = inputDevices[0];
            HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    // creates short vibration
                    device.SendHapticImpulse(0, 1.0f, 0.1f);
                }
            }
        }
    }

    // handles clean up
    void OnDestroy()
    {
        if (yesButton != null)
        {
            yesButton.onClick.RemoveListener(() => ButtonClicked(yesButton));
        }
        if (noButton != null)
        {
            noButton.onClick.RemoveListener(() => ButtonClicked(noButton));
        }
    }
}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/2018.3/Documentation/ScriptReference/UI.Button-onClick.html
//https://docs.unity3d.com/ScriptReference/UIElements.Button-clicked.html
//https://docs.unity3d.com/ScriptReference/XR.InputDevices.GetDevicesWithCharacteristics.html
//https://docs.unity3d.com/Manual/xr_input.html
//https://forum.unity.com/threads/haptics-not-working-with-oxr-xr-interaction-toolkit.1101877/
//https://forum.unity.com/threads/haptic-feedback-for-ui.1420253/
//https://www.youtube.com/watch?v=HxqnDww2Fjo
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDestroy.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.AddListener.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.RemoveListener.html