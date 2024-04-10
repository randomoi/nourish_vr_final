using UnityEngine;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class SmartPlate : MonoBehaviour
{
    private float totalCalories = 0f;
    private float totalWeight = 0f;

    // to be assigned in Unity inspector
    public SmartPlateInfoDisplayManager displayManager; 
    public AudioClip onPlatePlacementSound; 
    private AudioSource audioSource; 
    public FloatingIconSmartPlate floatingIconSmartPlate; 
    public DynamicFoodMeterSlider foodMeter; 

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // !!!important!!! i have whole food values and sliced values which are stored in holder

    // handles on tigger event when food is on the SmartPlate
    private void OnTriggerEnter(Collider other)
    {
        FoodInformationHolder foodInfoHolder = other.GetComponent<FoodInformationHolder>();
        PlateFoodItem foodItem = other.GetComponent<PlateFoodItem>();

        if (foodInfoHolder != null)
        {
            AddFoodToPlate(foodInfoHolder.foodInfo);
        }
        else if (foodItem != null)
        {
            AddFoodToPlate(foodItem.foodInfo);
        }
    }

    // handles subtraction when food item is removed
    private void OnTriggerExit(Collider other)
    {
        FoodInformationHolder foodInfoHolder = other.GetComponent<FoodInformationHolder>();
        PlateFoodItem foodItem = other.GetComponent<PlateFoodItem>();

        if (foodInfoHolder != null)
        {
            SubtractFoodFromPlate(foodInfoHolder.foodInfo);
        }
        else if (foodItem != null)
        {
            SubtractFoodFromPlate(foodItem.foodInfo);
        }
    }

    // handles values addition (all items on the plate)
    public void AddFoodToPlate(FoodInformation foodInfo)
    {
        totalCalories += foodInfo.caloriesPer100g;
        totalWeight += foodInfo.weight;
        UpdateDisplay();
        audioSource.PlayOneShot(onPlatePlacementSound);
        UpdateFoodMeterSlider();
        if (floatingIconSmartPlate != null)
        {
            floatingIconSmartPlate.SetIconVisibility(false);
        }
    }

    // handles values subtraction (all items on the plate)
    public void SubtractFoodFromPlate(FoodInformation foodInfo)
    {
        totalCalories -= foodInfo.caloriesPer100g;
        totalWeight -= foodInfo.weight;

        // in case there are negative values
        if (totalCalories < 0)
        {
            totalCalories = 0;
        }

        if (totalWeight < 0)
        {
            totalWeight = 0;
        }

        // if plate is empty, reset values; !!!important!!! i'm using foodData values, if plate is not reset, last item value is shown
        if (totalWeight == 0)
        {
            ResetSmartPlate();
        }
        else
        {
            UpdateDisplay();
            UpdateFoodMeterSlider();
        }
    }

    // handles setting of plate to zero
    private void ResetSmartPlate()
    {
        totalCalories = 0;
        totalWeight = 0;
        UpdateDisplay();
        UpdateFoodMeterSlider();
        if (floatingIconSmartPlate != null)
        {
            floatingIconSmartPlate.SetIconVisibility(true);
        }
    }

    // handles update of slider used as food meter
    private void UpdateFoodMeterSlider()
    {
        if (foodMeter != null)
        {
            foodMeter.UpdateFoodMeterSlider(totalCalories);
        }
    }

    // handles updade of displayed values
    void UpdateDisplay()
    {
        if (totalWeight > 0)
        {
            displayManager.UpdateSmartPlateInfo(totalCalories, totalWeight);
        }
        else
        {
            displayManager.HideSmartPlateInfo();
            if (foodMeter != null)
            {
                foodMeter.UpdateFoodMeterSlider(0); // resets food meter when Smartplate is empty
            }
        }
    }
}

// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/AudioClip.html
//https://docs.unity3d.com/ScriptReference/AudioSource.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/ScriptReference/Collider.OnTriggerExit.html
//https://forum.unity.com/threads/how-can-i-get-component-of-other-object.321249/
//https://docs.unity3d.com/ScriptReference/AudioSource.PlayOneShot.html
//https://docs.unity3d.com/ScriptReference/Collider.OnTriggerEnter.html
//https://www.youtube.com/watch?v=ysg9oaZEgwc
//https://discussions.unity.com/t/ontriggerexit-not-reactivating-a-gameobject/183177
//https://forum.unity.com/threads/solved-optimisation-of-ontriggerstay-for-traffic-lights.661324/
//https://stackoverflow.com/questions/41331296/unity-collider-ontriggerenter

