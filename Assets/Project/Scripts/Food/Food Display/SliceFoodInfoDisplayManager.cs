using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class SliceFoodInfoDisplayManager : MonoBehaviour
{
    // to be assigned in Unity inspector
    [SerializeField] private GameObject sliceInfoPanel; 
    [SerializeField] private TextMeshProUGUI titleText; 
    [SerializeField] private TextMeshProUGUI nameText; 
    [SerializeField] private TextMeshProUGUI caloriesText; 
    [SerializeField] private TextMeshProUGUI weightText; 

    private Camera mainCamera;

    private void Awake()
    {
        sliceInfoPanel.SetActive(false);
        mainCamera = Camera.main;
        if (titleText != null) titleText.text = "Slice Nutritional Value";
    }

    // gets all XRGrabInteractables objects and subscribes to their events
    void OnEnable()
    {
        var grabInteractableObjects = FindObjectsOfType<XRGrabInteractable>();
        foreach (var interactable in grabInteractableObjects)
        {
            interactable.selectEntered.AddListener(ManageSelectEntered);
            interactable.selectExited.AddListener(HandleSelectExited);
        }
    }

    // to avoid memory leaks unbscribes from events
    void OnDisable()
    {
        var grabInteractableObjects = FindObjectsOfType<XRGrabInteractable>();
        foreach (var interactable in grabInteractableObjects)
        {
            interactable.selectEntered.RemoveListener(ManageSelectEntered);
            interactable.selectExited.RemoveListener(HandleSelectExited);
        }
    }

    // handles SelectEntered
    private void ManageSelectEntered(SelectEnterEventArgs arg)
    {
        var foodInfo = arg.interactableObject.transform.GetComponent<FoodInformation>();
        if (foodInfo != null)
        {
            DisplaySliceInfo(foodInfo);
        }
    }

    // handles SelectExited
    private void HandleSelectExited(SelectExitEventArgs arg)
    {
        HideSliceFoodInfo();
    }

    // handles display of slice info
    public void DisplaySliceInfo(FoodInformation foodInfo)
    {
        nameText.text = "Name: " + foodInfo.name;
        caloriesText.text = "Calories: " + foodInfo.caloriesPer100g + " kcal";
        weightText.text = "Weight: " + foodInfo.weight + "g";

        PanelPositionAndFaceCamera();
        sliceInfoPanel.SetActive(true);
    }

    // handles panel position to face camera
    private void PanelPositionAndFaceCamera()
    {
        if (mainCamera == null) return;

        PanelPositionInFrontOfCamera();

        Vector3 positionCamera = sliceInfoPanel.transform.position - mainCamera.transform.position;
        positionCamera.y = 0; // to be parallel to the floor
        Quaternion panelRotation = Quaternion.LookRotation(positionCamera, Vector3.up);
        sliceInfoPanel.transform.rotation = panelRotation;
    }

    // handles panel to be in front of camera
    private void PanelPositionInFrontOfCamera()
    {
        float distance = 1.3f; // distance
        float offsetLeft = 0.5f; // left offset

        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0; // to be parallel to the floor
        cameraForward.Normalize(); // normalizes vector after changing y 

        Vector3 cameraRight = mainCamera.transform.right; // obtains right vector
        Vector3 leftward = -cameraRight * offsetLeft; // calculates offset

        Vector3 updatedPosition = cameraPosition + cameraForward * distance + leftward;
        sliceInfoPanel.transform.position = updatedPosition;
    }

    // handles panel hiding
    public void HideSliceFoodInfo()
    {
        sliceInfoPanel.SetActive(false);
    }
}

// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.AddListener.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.RemoveListener.html
//https://docs.unity3d.com/Manual/com.unity.textmeshpro.html
//https://docs.unity3d.com/ScriptReference/SerializeField.html
//https://docs.unity3d.com/ScriptReference/Camera-main.html
//https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
//https://docs.unity3d.com/ScriptReference/Object.FindObjectsOfType.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDisable.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs.html
//https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
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
//https://forum.unity.com/threads/convert-position-from-one-cameras-perspective-to-another.1387248/
//https://docs.unity3d.com/ScriptReference/Quaternion.html
//https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
//https://forum.unity.com/threads/how-to-make-object-match-position-with-camera.918788/