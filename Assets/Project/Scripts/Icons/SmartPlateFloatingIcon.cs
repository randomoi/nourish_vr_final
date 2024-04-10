using UnityEngine;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

// this class attaches icon to plate by name, if plate moved icon follows
public class FloatingIconSmartPlate : MonoBehaviour
{
    private Camera mainCamera;
    private Canvas worldSpaceCanvas;
    private Vector3 offset = new Vector3(0f, 0.3f, 0f); //  y offset 
    private Transform targetPlate;

    void Start()
    {
        // assigns main camera and world canvas
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        worldSpaceCanvas = FindObjectOfType<Canvas>();
        // gets target plate by name
        GameObject targetObject = GameObject.Find("SmartPlate"); // plate name
        if (targetObject != null)
        {
            targetPlate = targetObject.transform;
        }
    }

    // handles visibility of icon
    public void SetIconVisibility(bool isVisible)
    {
        this.gameObject.SetActive(isVisible);
    }

    // handles updates of position (it will follow SmartPlate)
    void Update()
    {
        PositionIconAboveTarget();
        Vector3 positionOfTarget = transform.position - mainCamera.transform.position;
        positionOfTarget.y = 0; // parallel
        transform.rotation = Quaternion.LookRotation(positionOfTarget);
    }

    // handles icon position above SmartPlate
    private void PositionIconAboveTarget()
    {
        if (targetPlate == null) return;

        // positions icon above SmartPlate using offset
        Vector3 targetPosition = targetPlate.position + offset;
        transform.position = targetPosition;

        // icon is part of worldspacecanvas
        if (worldSpaceCanvas != null && !this.gameObject.transform.IsChildOf(worldSpaceCanvas.transform))
        {
            this.gameObject.transform.SetParent(worldSpaceCanvas.transform, true);
        }
    }
}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/Canvas.html
//https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
//https://docs.unity3d.com/ScriptReference/GameObject.Find.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.AddListener.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.RemoveListener.html
//https://www.youtube.com/watch?v=ysg9oaZEgwc
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectExitEventArgs.html
//https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
//https://forum.unity.com/threads/the-use-of-this-in-c.478441/
//https://docs.unity3d.com/ScriptReference/Vector3.html
//https://docs.unity3d.com/ScriptReference/Transform.html
//https://docs.unity3d.com/ScriptReference/Camera-main.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/ScriptReference/RectTransform.html
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
//https://docs.unity3d.com/ScriptReference/Transform.IsChildOf.html
//https://docs.unity3d.com/ScriptReference/Transform.SetParent.html