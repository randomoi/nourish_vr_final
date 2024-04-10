// !!!!!i'm not taking credit for any of this code!!!!
// START - This code was copied from https://github.com/Fist-Full-of-Shrimp/VR-Unity-Template-2023

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages interactions within the Main Menu, such as starting the game, by handling UI elements and scene transitions.
/// </summary>
public class MainMenu : MonoBehaviour
{
    // SerializeField allows the startButtonSceneName to be set from the Unity Editor, providing flexibility in specifying the game scene name.
    [SerializeField]
    private string startButtonSceneName = "GameScene"; // The name of the scene to load when the game starts.

    // Reference to the start button to attach click events for starting the game.
    [SerializeField]
    private Button startButton;

    // Reference to the main menu UI to enable/disable it when the game starts or stops.
    [SerializeField]
    private GameObject mainMenu;

    private void Awake()
    {
        // Add a listener to the start button so it reacts to clicks/taps. This method is preferred to ensure the button is properly initialized before adding the listener.
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame); // Links the StartGame method to the button's onClick event.
        }
    }

    private void OnDestroy()
    {
        // It's crucial to remove listeners when the object is destroyed to prevent memory leaks and ensure that old listeners do not persist, which could lead to errors or unexpected behavior.
        if (startButton != null)
        {
            startButton.onClick.RemoveListener(StartGame); // Removes the linked StartGame method from the button's onClick event.
        }
    }

    /// <summary>
    /// Called when the start button is clicked. It deactivates the main menu UI and requests the LevelManager to load the specified game scene.
    /// </summary>
    private void StartGame()
    {
        // Hides the main menu UI to transition to the game scene. This is done to ensure a smooth transition and improve user experience.
        if (mainMenu != null)
        {
            mainMenu.SetActive(false); // Deactivates the main menu GameObject.
        }

        // Ensures that there is an instance of LevelManager available to handle the scene transition. This check is important to prevent errors if the LevelManager is not properly set up or is missing.
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadSceneAsync(startButtonSceneName); // Calls the LevelManager to load the game scene asynchronously.
        }
        else
        {
            // Provides a warning in the console if the LevelManager instance is not found, aiding in debugging and setup verification.
            Debug.LogWarning("LevelManager instance not found. Cannot load scene.");
        }
    }
}

// !!!!!i'm not taking credit for any of this code!!!!
// END - This code was copied from https://github.com/Fist-Full-of-Shrimp/VR-Unity-Template-2023

