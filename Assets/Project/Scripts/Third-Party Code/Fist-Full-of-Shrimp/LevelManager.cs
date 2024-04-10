// !!!!!i'm not taking credit for any of this code!!!!
// START - This code was copied from https://github.com/Fist-Full-of-Shrimp/VR-Unity-Template-2023


using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    // Define events for fade in and fade out transitions.
    public static event Action<float> FadeIn;
    public static event Action<float> FadeOut;

    // Singleton instance of LevelManager to ensure only one instance exists.
    public static LevelManager Instance;

    // Duration of the fade transitions.
    public float fadeDuration = 1.0f;

    // Flag to check if a scene is currently loading to prevent multiple loads.
    private bool _isLoading = false;

    private void Awake()
    {
        // Ensure that only one instance of LevelManager exists.
        EnsureSingletonInstance();
        // Subscribe to the sceneLoaded event to handle actions after a scene is loaded.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event when the object is destroyed.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Public method to start loading a scene asynchronously.
    public void LoadSceneAsync(string sceneName)
    {
        // Check if another scene is not already loading.
        if (!_isLoading)
        {
            // Start the coroutine to load the scene.
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        }
    }

    // Coroutine to load the scene asynchronously.
    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        _isLoading = true;

        // Invoke the fade in event at the start of the scene loading.
        FadeIn?.Invoke(fadeDuration);
        // Wait for the fade duration before proceeding.
        yield return new WaitForSeconds(fadeDuration);

        // Start loading the scene asynchronously.
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the scene has fully loaded.
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // Mark loading as complete.
        _isLoading = false;
    }

    // Callback method called after a scene is loaded.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Invoke the fade out event to transition to the new scene.
        FadeOut?.Invoke(fadeDuration);
    }

    // Ensures that this object is a singleton and manages its instance.
    private void EnsureSingletonInstance()
    {
        // If an instance doesn't exist, set it to this object.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent the object from being destroyed on scene load.
        }
        else if (Instance != this)
        {
            // If another instance exists, destroy this one to enforce the singleton pattern.
            Destroy(gameObject);
        }
    }
}


// !!!!!i'm not taking credit for any of this code!!!!
// END - This code was copied from https://github.com/Fist-Full-of-Shrimp/VR-Unity-Template-2023

