using UnityEngine;
using TMPro; 
using UnityEngine.XR.Interaction.Toolkit;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class DisplayFoodInfoMessage : MonoBehaviour
{

    [SerializeField] private GameObject foodInfoPanel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI caloriesText;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private TextMeshProUGUI nutritionalInfoText;

    private FoodInformation selectedFoodInformation;
    private Transform cameraTransform;


    void Start()
    {
        foodInfoPanel.SetActive(false); // panel visibility is set to false
        cameraTransform = Camera.main.transform;
    }

    void OnEnable()
    {
        var interactableObjects = FindObjectsOfType<XRGrabInteractable>();
        foreach (var interactable in interactableObjects)
        {
            interactable.selectEntered.AddListener(ManageSelectEntered);
            interactable.selectExited.AddListener(HandleSelectExited);
        }
    }

    void OnDisable()
    {
        var interactableObjects = FindObjectsOfType<XRGrabInteractable>();
        foreach (var interactable in interactableObjects)
        {
            interactable.selectEntered.RemoveListener(ManageSelectEntered);
            interactable.selectExited.RemoveListener(HandleSelectExited);
        }
    }

    private void ManageSelectEntered(SelectEnterEventArgs arg)
    {
        FoodInformation foodInfo = arg.interactableObject.transform.GetComponent<FoodInformation>();
        if (foodInfo != null)
        {
            DisplayNutritionalInfo(foodInfo);
        }
    }

    private void HandleSelectExited(SelectExitEventArgs arg)
    {
        // hides panel after food item is dropped
        HideFoodInfoPanel();
    }

    // handles hiding of food info panel
    public void HideFoodInfoPanel()
    {
        foodInfoPanel.SetActive(false);
    }


    public void SetSelectedFoodInformation(FoodInformation foodInfo)
    {
        selectedFoodInformation = foodInfo;
    }

    public void DisplayNutritionalInfo(FoodInformation foodInfo)
    {
        if (foodInfo != null)
        {
            // sets panel title
            titleText.text = "Nutritional Value";

            // update fields with food information
            nameText.text = "Name: " + foodInfo.name;
            caloriesText.text = "Calories: " + foodInfo.caloriesPer100g + " kcal";
            weightText.text = "Weight: " + foodInfo.weight + "g";
            nutritionalInfoText.text = "Fan Fact: " + foodInfo.nutritionalInfo;

            PanelPositionInFrontOfCamera(foodInfoPanel); 
            foodInfoPanel.SetActive(true);
        }
    }

    private void PanelPositionInFrontOfCamera(GameObject panel)
    {
        if (cameraTransform == null) return;

        float modalDistanceToCamera = 2.0f; // distance in front of camera 
        float offsetLeft = 0.5f; // left ofsett

        Vector3 cameraPosition = cameraTransform.position;
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // parallel to the floor
        cameraForward.Normalize(); // normalizes vector after changing y

        Vector3 cameraRight = cameraTransform.right; // gets right vector for camera
        Vector3 leftward = -cameraRight * offsetLeft; // calculates left offset

        Vector3 updatedPosition = cameraPosition + cameraForward * modalDistanceToCamera + leftward;
        panel.transform.position = updatedPosition;

        RaycastHit hit;
        // checks if there is an obstruction between camera and panel 
        if (Physics.Raycast(cameraPosition, updatedPosition - cameraPosition, out hit, modalDistanceToCamera))
        {
            if (IsObstruction(hit.transform))
            {
                // if yes uses static position 
                PanelInStaticPosition(panel);
                return; 
            }
        }

        // sets for panel to face camera
        panel.transform.LookAt(cameraPosition);
        panel.transform.rotation = Quaternion.LookRotation(panel.transform.position - cameraPosition);
    }

    // check if kitchen is obstruction
    private bool IsObstruction(Transform hitTransform)
    {
        return hitTransform.CompareTag("Kitchen");
    }

    private void PanelInStaticPosition(GameObject panel)
    {
        float fixedDistance = 0.8f; // sets distance 

        // calculates static position 
        Vector3 staticPosition = cameraTransform.position + cameraTransform.forward * fixedDistance;

        // update panel position
        panel.transform.position = staticPosition;

        // sets panel rotation to be parallel to kitchen
        panel.transform.rotation = Quaternion.Euler(0, -180, 0);
        panel.transform.LookAt(panel.transform.position + panel.transform.rotation * Vector3.back, Vector3.up);
    }

}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
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

