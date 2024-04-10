// !!!!!i'm not taking credit for any of this code!!!!
// START - This code was copied from https://github.com/Fist-Full-of-Shrimp/VR-Unity-Template-2023

using UnityEngine;
using System;

public class XRSettingsManager : MonoBehaviour
{
    public static event Action XRSettingsChange;
    public static XRSettingsManager Instance;

    private bool _continuousTurnActive = false;
    private bool _vignetteActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the XRSettingsManager persists across scene loads.
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one instance of the XRSettingsManager.
        }
    }

    /// <summary>
    /// Updates the continuous turn setting based on a dropdown value and invokes the XR settings change event.
    /// </summary>
    /// <param name="dropdownSetting">The dropdown setting index for continuous turn.</param>
    public void setContinuousTurn(int dropdownSetting)
    {
        _continuousTurnActive = dropdownSetting != 0;
        XRSettingsChange?.Invoke(); // Notify subscribers of the change.
    }

    /// <summary>
    /// Updates the vignette effect state based on the provided value and invokes the XR settings change event.
    /// </summary>
    /// <param name="vignetteValue">The new state of the vignette effect.</param>
    public void setVignette(bool vignetteValue)
    {
        _vignetteActive = vignetteValue;
        XRSettingsChange?.Invoke(); // Notify subscribers of the change.
    }

    /// <summary>
    /// Returns whether continuous turn is currently active.
    /// </summary>
    /// <returns>True if continuous turn is active, false otherwise.</returns>
    public bool isContinuousTurnActive()
    {
        return _continuousTurnActive;
    }

    /// <summary>
    /// Returns whether the vignette effect is currently active.
    /// </summary>
    /// <returns>True if the vignette effect is active, false otherwise.</returns>
    public bool isVignetteActive()
    {
        return _vignetteActive;
    }
}

// !!!!!i'm not taking credit for any of this code!!!!
// END - This code was copied from https://github.com/Fist-Full-of-Shrimp/VR-Unity-Template-2023

