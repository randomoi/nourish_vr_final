using UnityEngine;
using UnityEngine.UI;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class DynamicFoodMeterSlider : MonoBehaviour
{
    // to be assigned in Unity inspector
    public Slider foodMeterSlider; 
    public Image sliderFill;

    // to be assigned in Unity inspector
    [SerializeField] private float maxFoodCalories = 1000f; 

    public void UpdateFoodMeterSlider(float currentCalories)
    {
        if (currentCalories == 0)
        {
            // resets slider if no calories
            ResetMeterSlider();
        }
        else
        {
            // updates slider with current calorie count
            foodMeterSlider.value = Mathf.Clamp(currentCalories, 0, maxFoodCalories);
            UpdateFoodMeterSliderColor(currentCalories);
        }
    }

    // handles slider reset
    private void ResetMeterSlider()
    {
        foodMeterSlider.value = 0;
        sliderFill.color = Color.green; 
    }

    // sets slider visibility
    public void SetFoodMeterSliderVisibility(bool isVisible)
    {
        foodMeterSlider.gameObject.SetActive(isVisible);
    }

    // handles slider color updates based on set values
    private void UpdateFoodMeterSliderColor(float currentFoodCalories)
    {
        // color updating based on value
        if (currentFoodCalories <= 500)
        {
            sliderFill.color = Color.green;
        }
        else if (currentFoodCalories <= 750)
        {
            sliderFill.color = Color.yellow;
        }
        else if (currentFoodCalories > 1000)
        {
            sliderFill.color = Color.red;
        }
        else // for values between 751 to 999
        {
            sliderFill.color = new Color(1, 0.5f, 0); // dark orange
        }
    }
}

// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/2018.2/Documentation/ScriptReference/UI.Slider.html
//https://www.youtube.com/watch?v=nTLgzvklgU8
//https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-Slider.html
//https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html
//https://docs.unity3d.com/ScriptReference/Color.html
//https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
//https://docs.unity3d.com/2018.2/Documentation/ScriptReference/UI.Image.html
//https://discussions.unity.com/t/change-color-in-c-with-rgb-values/168605
//https://forum.unity.com/threads/help-me-reset-this-slider-please.1261043/






