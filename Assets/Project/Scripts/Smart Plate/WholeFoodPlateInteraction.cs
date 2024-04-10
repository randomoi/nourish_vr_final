using UnityEngine;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class WholeFoodPlateInteraction : MonoBehaviour
{
    // to be assigned in Unity inspector
    public FoodData foodData;

    private FoodInformation foodInfo;

    void Start()
    {
        // retrieves FoodInformation based on name
        foodInfo = foodData.foods.Find(food => food.name == gameObject.name);
        if (foodInfo == null)
        {
            Debug.LogError("FoodInformation was not found for " + gameObject.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // checks if collided with Smartplate based on tag "Plate"
        if (other.CompareTag("Plate") && foodInfo != null) 
        {
            SmartPlate plate = other.GetComponent<SmartPlate>();
            if (plate != null)
            {
                // adds food details to Smartplate
                plate.AddFoodToPlate(foodInfo);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // checks if collided with SmartPlate based on tag "Plate" and removes food details
        if (other.CompareTag("Plate") && foodInfo != null)
        {
            SmartPlate plate = other.GetComponent<SmartPlate>();
            if (plate != null)
            {
                // subtracts food from SmartPlate
                plate.SubtractFoodFromPlate(foodInfo);
            }
        }
    }
}

// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//https://docs.unity3d.com/ScriptReference/GameObject.Find.html
//https://docs.unity3d.com/ScriptReference/Debug.LogError.html
//https://docs.unity3d.com/ScriptReference/Collider.OnTriggerEnter.html
//https://docs.unity3d.com/ScriptReference/Component.CompareTag.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://forum.unity.com/threads/what-does-other-mean.182025/
//https://stackoverflow.com/questions/44172006/gameobject-name-is-returning-the-script-name-not-the-gameobject-name
//https://docs.unity3d.com/ScriptReference/Collider.OnTriggerExit.html

