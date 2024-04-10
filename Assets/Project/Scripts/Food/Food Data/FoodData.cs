using UnityEngine;
using System.Collections.Generic;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

[System.Serializable]
public class FoodInformation
{
    // values to be assigned in scriptable object FoodData
    public string name;
    public string nutritionalInfo;
    public float caloriesPer100g; 
    public float weight; 
}

[CreateAssetMenu(fileName = "FoodData", menuName = "ScriptableObjects/FoodData", order = 1)]
public class FoodData : ScriptableObject
{
    public List<FoodInformation> foods;
}


// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://docs.unity3d.com/ScriptReference/Serializable.html
//https://docs.unity3d.com/ScriptReference/CreateAssetMenuAttribute.html
//https://docs.unity3d.com/Manual/class-ScriptableObject.html
//https://forum.unity.com/threads/using-createassetmenu-and-scriptableobject.518022/
//https://gamedevbeginner.com/scriptable-objects-in-unity/