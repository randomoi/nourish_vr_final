// !!!!!i'm not taking credit for any of this code!!!!
// START - This code was copied from https://github.com/Fist-Full-of-Shrimp/VR-Unity-Template-2023


using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

/// <summary>
/// Listens to changes in XR settings and updates XR environment components accordingly.
/// </summary>
public class XRSettingsListener : MonoBehaviour
{
    [SerializeField]
    private TunnelingVignetteController tunnelingVignetteController; // Controls the vignette effect for tunneling vision.

    [SerializeField]
    private ActionBasedControllerManager controllerManager; // Manages controller inputs and settings.

    private void Awake()
    {
        // Subscribe to the XR settings change event.
        XRSettingsManager.XRSettingsChange += UpdateXRSettings;
    }

    private void Start()
    {
        // Apply the initial XR settings on start.
        UpdateXRSettings();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the XR settings change event to avoid memory leaks.
        XRSettingsManager.XRSettingsChange -= UpdateXRSettings;
    }

    /// <summary>
    /// Updates the XR environment components based on the current XR settings.
    /// </summary>
    private void UpdateXRSettings()
    {
        if (XRSettingsManager.Instance != null)
        {
            // Update the tunneling vignette controller based on the XR settings.
            if (tunnelingVignetteController != null)
            {
                tunnelingVignetteController.gameObject.SetActive(XRSettingsManager.Instance.isVignetteActive());
            }

            // Update the controller manager's smooth turn setting based on the XR settings.
            if (controllerManager != null)
            {
                controllerManager.smoothTurnEnabled = XRSettingsManager.Instance.isContinuousTurnActive();
            }
        }
        else
        {
            Debug.LogWarning("No XRSettingsManager was found. XR Rig will use default settings.");
        }
    }
}


// !!!!!i'm not taking credit for any of this code!!!!
// END - This code was copied from https://github.com/Fist-Full-of-Shrimp/VR-Unity-Template-2023

