using UnityEngine;
using TMPro;
using System.Collections;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class SmartPlateInfoDisplayManager : MonoBehaviour
{
    // to be assigned in Unity inspector
    [SerializeField] private GameObject smartPlatePanel; 
    [SerializeField] private TextMeshProUGUI titleText; 
    [SerializeField] private TextMeshProUGUI totalCaloriesText; 
    [SerializeField] private TextMeshProUGUI totalWeightText; 
    [SerializeField] private TextMeshProUGUI dietaryAlertText; 

    [SerializeField] private DynamicFoodMeterSlider foodMeter; // ref to DynamicFoodMeterSlider


    private Camera mainCamera;

    private void Awake()
    {
        smartPlatePanel.SetActive(false);
        foodMeter.SetFoodMeterSliderVisibility(false);

        mainCamera = Camera.main; 

        // sets title text
        if (titleText != null) titleText.text = "Smart Plate Info";
    }

    // handles update of dietary alerts and color text based on total calories on SmartPlate
    public void UpdateSmartPlateInfo(float totalCalories, float totalWeight)
    {
        totalCaloriesText.text = $"Total Calories: {totalCalories} kcal";
        totalWeightText.text = $"Total Weight: {totalWeight}g";

        if (totalCalories <= 500)
        {
            dietaryAlertText.text = "Optimal Portion";
            dietaryAlertText.color = Color.green;
        }
        else if (totalCalories <= 750)
        {
            dietaryAlertText.text = "Moderate Portion";
            dietaryAlertText.color = Color.yellow;
        }
        else if (totalCalories > 1000)
        {
            dietaryAlertText.text = "Excess Serving";
            dietaryAlertText.color = Color.red;
        }
        else // for values between 751 to 999
        {
            dietaryAlertText.text = "Elevated Portion";
            dietaryAlertText.color = new Color(1, 0.5f, 0); // darker orange
        }

        PanelPositionToFaceCamera();
        smartPlatePanel.SetActive(true);

        StartCoroutine(HideSmartPlatePanelAfterDelay(5f)); // hides panel after 5 seconds

        // updates food meter slider color 
        if (foodMeter != null)
        {
            foodMeter.UpdateFoodMeterSlider(totalCalories);

            foodMeter.SetFoodMeterSliderVisibility(true);
        }
    }

    // handles panels position to be facing camera
    private void PanelPositionToFaceCamera()
    {
        if (mainCamera == null) return;

        PanelPositionInFrontOfCamera();

        Vector3 positionToCamera = smartPlatePanel.transform.position - mainCamera.transform.position;
        positionToCamera.y = 0; // parallel to floor
        Quaternion rotation = Quaternion.LookRotation(positionToCamera, Vector3.up);
        smartPlatePanel.transform.rotation = rotation;
    }


    // handles panel position (hight, distance etc.)
    private void PanelPositionInFrontOfCamera()
    {
        float distanceFromCamera = 1.3f; // distance to camera
        float offsetLeft = 0.5f; // left offset

        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0; // parallel to floor
        cameraForward.Normalize(); // normalizes vector after adjusting y

        Vector3 cameraRight = mainCamera.transform.right; // gets right vector
        Vector3 leftward = -cameraRight * offsetLeft; // calculates leftward offset

        Vector3 updatedPosition = cameraPosition + cameraForward * distanceFromCamera + leftward;
        smartPlatePanel.transform.position = updatedPosition;
    }

    private IEnumerator HideSmartPlatePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        smartPlatePanel.SetActive(false);

    }

    // handles hiding of SmartPlate info panel
    public void HideSmartPlateInfo()
    {
        StopAllCoroutines();
        smartPlatePanel.SetActive(false);
        foodMeter.SetFoodMeterSliderVisibility(false);
    }
}

// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/Packages/com.unity.textmeshpro@1.0/api/TMPro.TextMeshProUGUI.html
//https://docs.unity3d.com/ScriptReference/Color.html
//https://docs.unity3d.com/ScriptReference/WaitForSeconds.html
//https://docs.unity3d.com/ScriptReference/SerializeField.html
//https://docs.unity3d.com/ScriptReference/Transform.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
//https://docs.unity3d.com/ScriptReference/Object.FindObjectsOfType.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.AddListener.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.RemoveListener.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectExitEventArgs.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDisable.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://discussions.unity.com/t/cant-get-attach-transform-of-interactorobject/251540
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs.html
//https://docs.unity3d.com/ScriptReference/Component.CompareTag.html
//https://docs.unity3d.com/ScriptReference/Transform.html
//https://forum.unity.com/threads/checking-raycast-if-it-is-obstructed-by-another-object-before-a-box-collider.1446814/
//https://docs.unity3d.com/ScriptReference/RaycastHit.html
//https://docs.unity3d.com/ScriptReference/Transform-position.html
//https://forum.unity.com/threads/how-to-make-object-match-position-with-camera.918788/
//https://forum.unity.com/threads/c-change-camera-target-object-problem.151368/
//https://forum.unity.com/threads/check-if-the-player-was-hit-by-a-raycasthit-solved.379780/
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
//https://docs.unity3d.com/ScriptReference/Vector3.html
//https://docs.unity3d.com/ScriptReference/Transform.IsChildOf.html
//https://docs.unity3d.com/ScriptReference/Transform.SetParent.html
//https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
//https://docs.unity3d.com/ScriptReference/Vector3.Normalize.html
//https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
//https://stackoverflow.com/questions/73136759/menu-panels-show-in-game-by-default-as-open

