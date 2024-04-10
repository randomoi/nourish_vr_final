using UnityEngine;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class FoodMeterSliderPositionController : MonoBehaviour
{
    public Transform targetObject; // object slider should be attached to
    public Vector3 offset = new Vector3(0f, 0.3f, 0f); // offset based on target
    public Camera mainCamera; // ref to main camera

    private RectTransform rectTransform; // ref to RectTransform of slider

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // gets component

        if (mainCamera == null)
        {
            mainCamera = Camera.main; // finds camera 
        }
    }

    // handles slider position updates
    void Update()
    {
        if (targetObject != null)
        {
            PositionFoodMeterSlider();
        }
    }

    // handles position of slider
    private void PositionFoodMeterSlider()
    {
        if (rectTransform != null)
        {
            // converts world position of target and offset 
            Vector3 worldPosition = targetObject.position + offset;
            rectTransform.position = worldPosition;

            // positions slider to face camera
            Vector3 directionToFace = worldPosition - mainCamera.transform.position;
            directionToFace.y = 0; 
            rectTransform.rotation = Quaternion.LookRotation(directionToFace);
        }
    }
}

// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
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
