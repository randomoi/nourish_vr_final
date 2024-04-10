using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class XRGrabInteractableSnapBackObjects : XRGrabInteractable
{
    private Vector3 originalObjectPosition;
    private Quaternion originalObjectRotation;
    public bool isGrabbed = false;
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        originalObjectPosition = transform.position;
        // saves original object rotation as vertical
        originalObjectRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // prevents Rigidbody rotation to keep vertical position
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    // handles when object is grabbed
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        isGrabbed = true;

        // if object is grabbed, removes rotation constraints for normal handling
        rigidBody.constraints = RigidbodyConstraints.None;
    }

    // handles when object is put down
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isGrabbed = false;

        // resets velocity to prevent object from jumpping
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        // re-adds constraints to keep vertical position after object was dropped/placed down
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // snapback to original vertical rotation
        transform.rotation = originalObjectRotation;

        // plays sound when object is put down
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
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



