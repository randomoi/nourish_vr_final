using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class XRGrabInteractableSnapKnifeToHand : XRGrabInteractable
{
    public Transform snapBackPosition;
    public Transform snapBackRotation;

    private Vector3 originalAttachLocalPosition;
    private Quaternion originalAttachLocalRotation;

    protected override void Awake()
    {
        base.Awake();
        // stores original local position and rotation of attach point
        originalAttachLocalPosition = attachTransform.localPosition;
        originalAttachLocalRotation = attachTransform.localRotation;
    }

    // handles when knife is grabbed
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        // uses position and rotation of transforms set in Unity inspector to set attach transform
        if (snapBackPosition != null && snapBackRotation != null)
        {
            attachTransform.position = snapBackPosition.position;
            attachTransform.rotation = snapBackRotation.rotation;
        }
    }

    // handles when knife is released
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        // resets attach point to original position and rotation
        attachTransform.localPosition = originalAttachLocalPosition;
        attachTransform.localRotation = originalAttachLocalRotation;
    }
}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/manual/extending-xri.html
//https://forum.unity.com/threads/generating-and-grabbing-an-object-all-at-once.1244821/
//https://docs.unity3d.com/ScriptReference/Vector3.html
//https://docs.unity3d.com/ScriptReference/Quaternion.html
//https://docs.unity3d.com/ScriptReference/Rigidbody.html
//https://docs.unity3d.com/ScriptReference/AudioSource.html
//https://discussions.unity.com/t/c-inheriting-awake/70446
//https://stackoverflow.com/questions/53076669/how-to-correctly-inherit-unitys-callback-functions-like-awake-start-and-up
//https://forum.unity.com/threads/how-to-freeze-quaternion-y-axis-from-rotating-help.356905/
//https://docs.unity3d.com/ScriptReference/Transform-position.html
//https://docs.unity3d.com/ScriptReference/RigidbodyConstraints.FreezeRotationX.html
//https://docs.unity3d.com/ScriptReference/RigidbodyConstraints.html
//https://docs.unity3d.com/ScriptReference/RigidbodyConstraints.FreezeRotationZ.html
//https://forum.unity.com/threads/selectentereventargs-how-to-get-interacting-controller.1230318/
//https://docs.unity.cn/Packages/com.unity.xr.interaction.toolkit@3.0/api/UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.OnSelectExited.html
//https://docs.unity3d.com/ScriptReference/Vector3-zero.html
//https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
//https://forum.unity.com/threads/rigidbody-velocity-vector3-zero-not-working.1331097/
//https://stackoverflow.com/questions/58914962/rigidbody-not-stopping-instantly-when-setting-its-velocity-to-0


