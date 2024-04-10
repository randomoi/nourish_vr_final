using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class DisplayFoodInfoOnGrab : MonoBehaviour
{
    [SerializeField] private SliceFoodInfoDisplayManager foodInfoDisplayManager;
    private bool isFirstInteractionWithItem = true; // checks if it's a first interaction with item

    private void Awake()
    {
        if (!foodInfoDisplayManager)
        {
            foodInfoDisplayManager = FindObjectOfType<SliceFoodInfoDisplayManager>();
        }
    }

    // subscribes to selectEntered
    protected void OnEnable()
    {
        GetComponent<XRBaseInteractable>().selectEntered.AddListener(OnGrabbed);
    }

    // to avoid memory leaks unsubscribes 
    protected void OnDisable()
    {
        GetComponent<XRBaseInteractable>().selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // if first interaction with item
        if (isFirstInteractionWithItem)
        {
            FoodInformationHolder foodInfoHolder = GetComponent<FoodInformationHolder>();
            if (foodInfoHolder != null && foodInfoHolder.foodInfo != null)
            {
                // shows food info using SliceFoodInfoDisplayManager
                foodInfoDisplayManager.DisplaySliceInfo(foodInfoHolder.foodInfo);
                isFirstInteractionWithItem = false; // marks as first interaction 
            }
            else
            {
                Debug.LogWarning($"FoodInformationHolder for {gameObject.name} was not found.");
            }
        }
    }
}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/GameObject.html
//https://unity.com/how-to/separate-game-data-logic-scriptable-objects
//https://docs.unity3d.com/ScriptReference/ScriptableObject.html
//https://docs.unity3d.com/ScriptReference/GameObject.Find.html
//https://forum.unity.com/threads/the-use-of-this-in-c.478441/
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.AddListener.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.RemoveListener.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
//https://forum.unity.com/threads/using-xrbaseinteractor-selectentered.1457695/
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/ScriptReference/Debug.LogWarning.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDisable.html
//https://forum.unity.com/threads/selectentereventargs-how-to-get-interacting-controller.1230318/
//https://discussions.unity.com/t/question-about-unsubscribing-eventhandlers-ondisable-and-onenable/163938
//https://forum.unity.com/threads/calling-findobjectsoftype-in-awake-not-working.105387/
//https://docs.unity3d.com/ScriptReference/SerializeField.html