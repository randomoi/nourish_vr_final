using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class GrabObject : MonoBehaviour
{
    public GameObject FirstQuestionModal; // question one modal
    public string foodName;
    private FoodManager foodManager;
    private DisplayFoodInfoMessage displayFoodInfoMessage;
    private Transform cameraTransform;
    private bool hasDisplayed = false; // tracks if panel was shown

    private void Start()
    {
        FirstQuestionModal.SetActive(false);
        foodManager = FindObjectOfType<FoodManager>();
        displayFoodInfoMessage = FindObjectOfType<DisplayFoodInfoMessage>();
        cameraTransform = Camera.main.transform;
    }

    public void OnGrabbed(SelectEnterEventArgs args)
    {
        // shows nutritional information when food is grabbed
        DisplayNutritionalInformation();

        // if first interaction, show question sequence after a delay
        if (!hasDisplayed)
        {
            StartCoroutine(DisplayQuestionAfterDelay());
            hasDisplayed = true; // makes sure that questions only shown once 
        }
    }

    private void DisplayNutritionalInformation()
    {
        FoodInformation foodInfo = foodManager.GetFoodInfo(foodName);
        if (foodInfo != null)
        {
            displayFoodInfoMessage.SetSelectedFoodInformation(foodInfo);
            displayFoodInfoMessage.DisplayNutritionalInfo(foodInfo); // shows nutritional information
        }
        else
        {
            Debug.LogWarning($"Food Information for {foodName} was not found.");
        }
    }

    IEnumerator DisplayQuestionAfterDelay()
    {
        yield return new WaitForSeconds(0); // tbd delay 
        PanelPositionInFrontOfCamera(FirstQuestionModal);
        FirstQuestionModal.SetActive(true); // shows first question
    }

    
    // handles position for question one
    private void PanelPositionInFrontOfCamera(GameObject panel)
    {
        if (cameraTransform == null) return;

        float panelDistance = 2.0f; // distance to camera
        float offsetRight = 0.5f; // right offset
        float offsetHeight = 0.15f; // height offset 

        Vector3 cameraPosition = cameraTransform.position;
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // sets parallel positioning to the floor
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        Vector3 panelRight = cameraRight * offsetRight; // moves panel to the right 
        Vector3 panelUpward = Vector3.up * offsetHeight; // moves panel slightly up

        Vector3 updatedPosition = cameraPosition + cameraForward * panelDistance + panelRight + panelUpward;
        panel.transform.position = updatedPosition;

        // sets panel to be viewed in user viewing direction
        panel.transform.LookAt(cameraPosition);
        panel.transform.rotation = Quaternion.LookRotation(panel.transform.position - cameraPosition, Vector3.up);
    }

}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
//https://docs.unity3d.com/ScriptReference/Transform.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
//https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
//https://docs.unity3d.com/ScriptReference/Camera.html
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
//https://docs.unity3d.com/ScriptReference/Vector3.html
//https://docs.unity3d.com/ScriptReference/Transform.IsChildOf.html
//https://docs.unity3d.com/ScriptReference/Transform.SetParent.html
//https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
//https://docs.unity3d.com/ScriptReference/Vector3.Normalize.html
//https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs.html
