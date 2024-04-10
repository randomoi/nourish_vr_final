using UnityEngine;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

public class FoodManager : MonoBehaviour
{
    public FoodData foodData; // ref to scriptable object

    void Awake()
    {
        // loads food at the start 
        foreach (var food in foodData.foods)
        {
            Debug.Log($"Food loaded: {food.name}");
        }
    }

    // gets food
    public FoodInformation GetFoodInfo(string foodName)
    {
        return foodData.foods.Find(food => food.name == foodName);
    }
}

// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/Debug.Log.html
//https://forum.unity.com/threads/foreach-a-list-of-objects.453140/
//https://docs.unity3d.com/ScriptReference/GameObject.Find.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html