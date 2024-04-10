// !!!!!i'm not taking credit for any of this code!!!!
// START - This code was copied from https://github.com/Fist-Full-of-Shrimp/VR-Unity-Template-2023

using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the fading in and out of a canvas using a CanvasGroup component. It listens for fade events from the LevelManager to trigger fades.
/// </summary>
[RequireComponent(typeof(CanvasGroup))] // Ensures a CanvasGroup component is attached to the GameObject.
public class FadeCanvas : MonoBehaviour
{
    // Stores the reference to the currently running fade coroutine, allowing it to be stopped if a new fade begins.
    public Coroutine CurrentRoutine { private set; get; } = null;

    // Reference to the CanvasGroup component used to adjust the canvas's opacity.
    private CanvasGroup canvasGroup;

    // Duration for a quick fade, used for quick transitions.
    private float quickFadeDuration = 0.25f;

    private void Awake()
    {
        // Initialize the CanvasGroup component.
        canvasGroup = GetComponent<CanvasGroup>();

        // Subscribe to the FadeIn and FadeOut events from LevelManager to start fade transitions.
        LevelManager.FadeIn += StartFadeIn;
        LevelManager.FadeOut += StartFadeOut;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the FadeIn and FadeOut events when the GameObject is destroyed to avoid memory leaks.
        LevelManager.FadeIn -= StartFadeIn;
        LevelManager.FadeOut -= StartFadeOut;
    }

    /// <summary>
    /// Initiates a fade-in effect by setting the canvas's transparency to fully transparent and then fading to fully opaque.
    /// </summary>
    /// <param name="fadeDuration">The duration of the fade-in effect.</param>
    public void StartFadeIn(float fadeDuration)
    {
        // Stops any currently running fade coroutine before starting a new one.
        if (CurrentRoutine != null) StopCoroutine(CurrentRoutine);
        // Starts a new fade coroutine to change the alpha from 0 (transparent) to 1 (opaque).
        CurrentRoutine = StartCoroutine(Fade(0f, 1f, fadeDuration));
    }

    /// <summary>
    /// Initiates a fade-out effect by setting the canvas's transparency to fully opaque and then fading to fully transparent.
    /// </summary>
    /// <param name="fadeDuration">The duration of the fade-out effect.</param>
    public void StartFadeOut(float fadeDuration)
    {
        // Stops any currently running fade coroutine before starting a new one.
        if (CurrentRoutine != null) StopCoroutine(CurrentRoutine);
        // Starts a new fade coroutine to change the alpha from 1 (opaque) to 0 (transparent).
        CurrentRoutine = StartCoroutine(Fade(1f, 0f, fadeDuration));
    }

    /// <summary>
    /// Initiates a quick fade-in effect using a predefined quick fade duration.
    /// </summary>
    public void QuickFadeIn()
    {
        StartFadeIn(quickFadeDuration);
    }

    /// <summary>
    /// Initiates a quick fade-out effect using a predefined quick fade duration.
    /// </summary>
    public void QuickFadeOut()
    {
        StartFadeOut(quickFadeDuration);
    }

    /// <summary>
    /// Coroutine to smoothly transition the canvas's alpha value from startAlpha to endAlpha over the specified duration.
    /// </summary>
    /// <param name="startAlpha">The initial alpha value.</param>
    /// <param name="endAlpha">The final alpha value.</param>
    /// <param name="duration">The time in seconds over which the transition occurs.</param>
    /// <returns>IEnumerator for coroutine control.</returns>
    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0.0f;
        // Set the initial alpha value of the canvas.
        canvasGroup.alpha = startAlpha;

        // Gradually change the alpha value to the endAlpha over the duration.
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            canvasGroup.alpha = newAlpha;
            yield return null; // Wait for the next frame before continuing the loop.
        }

        // Ensure the final alpha value is set to endAlpha.
        canvasGroup.alpha = endAlpha;
        // Reset the current routine reference since the fade is complete.
        CurrentRoutine = null;
    }
}

// !!!!!i'm not taking credit for any of this code!!!!
// END - This code was copied from https://github.com/Fist-Full-of-Shrimp/VR-Unity-Template-2023

