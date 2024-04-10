using UnityEngine;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class PlateFoodItem : MonoBehaviour
{
    // to be assigned in Unity inspector
    public FoodData foodData;
    public FoodInformation foodInfo;

    void Start()
    {
        if (foodData != null)
        {
            foodInfo = foodData.foods.Find(food => food.name == this.gameObject.name);
            if (foodInfo == null)
            {
                Debug.LogError("FoodInformation was not found for " + this.gameObject.name, this);
            }
        }
        else
        {
            Debug.LogError("FoodData scriptable object was not assigned.", this);
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

