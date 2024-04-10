using UnityEngine;
using System.Collections;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class DisplayGoInteractMessage : MonoBehaviour
{
    // to be assigned in Unity inspector
    public GameObject goInteractPanel; 
    public float messageDuration = 4f; 
    private Transform cameraTransform;

    void Start()
    {
        goInteractPanel.SetActive(false); // sets panel to invisible 
        cameraTransform = Camera.main.transform;
    }

    // handle GoInteract message
    public void ShowGoInteractMessage()
    {
        PanelPositionInFrontOfCamera(goInteractPanel);
        StartCoroutine(DisplayMessageCoroutine());
    }

    // handles delay
    private IEnumerator DisplayMessageCoroutine()
    {
        goInteractPanel.SetActive(true); // sets to true and view the message
        yield return new WaitForSeconds(messageDuration); // message duration
        goInteractPanel.SetActive(false); // hides after set duration
    }

    // handles panel position
    private void PanelPositionInFrontOfCamera(GameObject panel)
    {
        if (cameraTransform == null) return;

        float modalDistanceToCamera = 2.0f; // distance to camera
        float offsetRight = 0.495f; // offset to right
        float offsetHeight = -0.04f; // offset height

        Vector3 cameraPosition = cameraTransform.position;
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // parallel to floor
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        Vector3 moveRightward = cameraRight * offsetRight; // move to right
        Vector3 moveUpward = Vector3.up * offsetHeight; // move up

        Vector3 updatedPosition = cameraPosition + cameraForward * modalDistanceToCamera + moveRightward + moveUpward;
        panel.transform.position = updatedPosition;

        panel.transform.LookAt(cameraPosition);
        panel.transform.rotation = Quaternion.LookRotation(panel.transform.position - cameraPosition, Vector3.up);
    }

}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
//https://docs.unity3d.com/ScriptReference/SerializeField.html
//https://docs.unity3d.com/ScriptReference/Transform.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
//https://docs.unity3d.com/ScriptReference/Object.FindObjectsOfType.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.AddListener.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.RemoveListener.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectExitEventArgs.html
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

