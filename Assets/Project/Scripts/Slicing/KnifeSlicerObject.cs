using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using EzySlice;
using System;

// START - The code was created using documentation and additional external research materials. Please review the referenced links.

// starting point of slicing was Valem's tutorial: https://www.youtube.com/watch?v=GQzW6ZJFQ94
// EzySlice Library: https://github.com/DavidArayan/ezy-slice
// VelcityEstimator: https://drive.google.com/file/d/1weQYzRNKnuq4ADg9K5mjC574dPECrq5i/view
// additionally these were used as add-ons:
// https://github.com/gradientspace/geometry3UnityDemos/blob/master/Assets/g3UnityUtils.cs
// https://gist.github.com/unitycoder/379e885c9215d48bcfb5c554e13a5d26

// please note that slicing is not working as expected, it produces unwanted artifacts that don't have grabbables and calculations for weight and calories attached
// there is probably conflict between some of the pieces I tried to put together from different developers, this code needs to be disected to find the issue
// !!!!!i'm not taking credit for any of this code!!!!
public class KnifeSlicerObject : MonoBehaviour
{
    // to be assigned in Unity inspector - for cutting
    public Transform startCutPoint;
    public Transform endCutPoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public Material CutMaterial;
    public float cutForce;
    public float cutForceMultiplier;
    public float minVelocityThreshold;

    // references material, scriptable object, food info and manager
    public PhysicMaterial SlicingPhysicsMaterial;
    public FoodData foodData;
    public SliceFoodInfoDisplayManager foodInfoDisplayManager;
    
    // to be assigned in Unity inspector - for audio
    public AudioClip cutSound; 
    private AudioSource audioSource; 

    private float volumeOriginal = 0;
    private FoodInformation foodInfoOriginal;

    void Awake()
    {
        // specifies layer on which can be cut and excludes Kitchen layer
        sliceableLayer = LayerMask.GetMask("Default") & ~LayerMask.GetMask("Kitchen");

        // makes sure AudioSource exists if, not creates it
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) 
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    //copied from https://www.youtube.com/watch?v=GQzW6ZJFQ94
    void FixedUpdate()
    {
        bool hasCut = Physics.Linecast(startCutPoint.position, endCutPoint.position, out RaycastHit hit, sliceableLayer);
        if (hasCut)
        {
            GameObject target = hit.transform.gameObject;
            // stores original info and volume before cutting
            volumeOriginal = CalculateMeshVolume(target.GetComponent<MeshFilter>().mesh);
            foodInfoOriginal = foodData.foods.Find(food => food.name == target.name);
            Slice(target);
        }
    }

    //copied from https://www.youtube.com/watch?v=GQzW6ZJFQ94
    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        if (velocity.magnitude < minVelocityThreshold)
        {
            // uses default direction if < than threshold, but it conitnues action
            velocity = (endCutPoint.position - startCutPoint.position).normalized * minVelocityThreshold;
        }

        Vector3 direction = endCutPoint.position - startCutPoint.position;
        Vector3 planeNormal = Vector3.Cross(direction, velocity).normalized;

        if (planeNormal == Vector3.zero)
        {
            planeNormal = direction.normalized;
        }

        // tries to cut object
        SlicedHull hull = target.Slice(endCutPoint.position, planeNormal, CutMaterial);
        if (hull != null)
        {
            // plays cutting/chopping sound
            audioSource.clip = cutSound;
            audioSource.Play();

            CreateSlicedObjects(hull, target, velocity.magnitude);
        }
    }

    // handles creation of slices
    //copied from https://www.youtube.com/watch?v=GQzW6ZJFQ94
    void CreateSlicedObjects(SlicedHull hull, GameObject originalObject, float velocityMagnitude)
    {
        GameObject upperHull = hull.CreateUpperHull(originalObject, CutMaterial);
        GameObject lowerHull = hull.CreateLowerHull(originalObject, CutMaterial);

        if (upperHull != null && lowerHull != null)
        {
            SetupSlicedObject(upperHull, velocityMagnitude);
            SetupSlicedObject(lowerHull, velocityMagnitude);

            // cleans up original object
            Destroy(originalObject);
        }
    }

    // handles setup of slice
    //copied from https://www.youtube.com/watch?v=GQzW6ZJFQ94
    void SetupSlicedObject(GameObject slicedObject, float velocityMagnitude)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.useGravity = true;

        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = false;
        collider.material = SlicingPhysicsMaterial;

        float cutForceDynamic = Mathf.Clamp(velocityMagnitude, 0.1f, 1f) * cutForceMultiplier;
        rb.AddExplosionForce(cutForceDynamic, slicedObject.transform.position, 1);

        XRGrabInteractable grabInteractableObject = slicedObject.AddComponent<XRGrabInteractable>();

        // setups attach point for grabbing a piece
        Vector3 centroid = CalculateMeshCenter(slicedObject);
        GameObject attachPointObject = new GameObject("Attach Point");
        attachPointObject.transform.SetParent(slicedObject.transform, false);
        attachPointObject.transform.position = centroid;
        grabInteractableObject.attachTransform = attachPointObject.transform;

        // adds food info to cut piece
        AssignFoodInfoToSlice(slicedObject, volumeOriginal, foodInfoOriginal);

        // adds grab events to show/hide food info
        grabInteractableObject.selectEntered.AddListener(args => foodInfoDisplayManager.DisplaySliceInfo(slicedObject.GetComponent<FoodInformationHolder>().foodInfo));
        grabInteractableObject.selectExited.AddListener(args => foodInfoDisplayManager.HideSliceFoodInfo());
    }

    // handles calculation of mesh center
    // copied from https://github.com/gradientspace/geometry3UnityDemos/blob/master/Assets/g3UnityUtils.cs
    Vector3 CalculateMeshCenter(GameObject obj)
    {
        MeshFilter filterMesh = obj.GetComponent<MeshFilter>();
        if (filterMesh == null || filterMesh.mesh == null)
        {
            Debug.LogWarning("CalculateMeshCenter: MeshFilter is null.");
            return Vector3.zero;
        }

        Vector3[] vertices = filterMesh.mesh.vertices;
        Vector3 sumV = Vector3.zero;
        foreach (Vector3 vert in vertices)
        {
            // transforms vertex to world space
            sumV += obj.transform.TransformPoint(vert); 
        }

        if (vertices.Length > 0)
        {
            return sumV / vertices.Length; // returns average 
        }

        return Vector3.zero; // zero if no vertices
    }

    // handles mesh volume calculation
    //copied from https://gist.github.com/unitycoder/379e885c9215d48bcfb5c554e13a5d26
    float CalculateMeshVolume(Mesh mesh)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += VolumeOfTriangle(p1, p2, p3);
        }
        return Mathf.Abs(volume); // must be positive value
    }

    // handles calculation of volume of triangle
    // copied from https://gist.github.com/unitycoder/379e885c9215d48bcfb5c554e13a5d26
    float VolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    // adds info on each cut piece 
    void AssignFoodInfoToSlice(GameObject slicedObject, float volumeOriginal, FoodInformation foodInfoOriginal)
    {
        float slicedVolume = CalculateMeshVolume(slicedObject.GetComponent<MeshFilter>().mesh);
        float weightRatio = slicedVolume / volumeOriginal;
        float slicedWeight = (float)Math.Round(foodInfoOriginal.weight * weightRatio, 2); // rounds numbers to two decimal places
        float slicedCaloriesPer100g = (float)Math.Round(foodInfoOriginal.caloriesPer100g * weightRatio, 2); // rounds numbers to two decimal places


        FoodInformationHolder foodInfoHolder = slicedObject.GetComponent<FoodInformationHolder>();
        if (foodInfoHolder == null)
        {
            foodInfoHolder = slicedObject.AddComponent<FoodInformationHolder>();
        }
        foodInfoHolder.foodInfo = new FoodInformation
        {
            name = foodInfoOriginal.name + " Slice",
            caloriesPer100g = slicedCaloriesPer100g,
            weight = slicedWeight
        };

        // makes sure that when user grabs slice, information is attached to it 
        DisplayFoodInfoOnGrab onGrabDisplay = slicedObject.GetComponent<DisplayFoodInfoOnGrab>();
        if (onGrabDisplay == null)
        {
            onGrabDisplay = slicedObject.AddComponent<DisplayFoodInfoOnGrab>();
        }
    }
}

// !!!!!i'm not taking credit for any of this code!!!!
// END - The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://www.youtube.com/watch?v=GQzW6ZJFQ94
//https://drive.google.com/file/d/1weQYzRNKnuq4ADg9K5mjC574dPECrq5i/view
//https://docs.unity3d.com/ScriptReference/Transform.html
//https://docs.unity3d.com/ScriptReference/LayerMask.html
//https://docs.unity3d.com/ScriptReference/Material.html
//https://docs.unity3d.com/Manual/class-AudioClip.html
//https://docs.unity3d.com/ScriptReference/AudioSource.html
//https://docs.unity3d.com/ScriptReference/LayerMask.GetMask.html
//https://discussions.unity.com/t/ignore-one-layermask-question/186174
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/ScriptReference/GameObject.AddComponent.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
//https://docs.unity3d.com/ScriptReference/Physics.Linecast.html
//https://discussions.unity.com/t/getting-gameobject-from-raycast-hit/24658
//https://discussions.unity.com/t/find-all-objects-hit-by-physics-linecast/164288
//https://docs.unity3d.com/ScriptReference/MeshFilter.html
//https://docs.unity3d.com/ScriptReference/Vector3.Cross.html
//https://docs.unity3d.com/ScriptReference/Vector3-normalized.html
//https://docs.unity3d.com/ScriptReference/Vector3-zero.html
//https://forum.unity.com/threads/struggle-with-normalized-direction-vector.458250/
//https://forum.unity.com/threads/sounds-getting-cuttoff-with-audiosource-play.320575/
//https://docs.unity3d.com/ScriptReference/Object.Destroy.html
//https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html
//https://docs.unity3d.com/ScriptReference/MeshCollider.html
//https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html
//https://docs.unity3d.com/ScriptReference/Rigidbody.AddExplosionForce.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.html
//https://discussions.unity.com/t/how-to-attach-an-object-onto-another-object/141462
//https://forum.unity.com/threads/get-centre-point-of-multiple-child-objects.131921/
//https://docs.unity3d.com/ScriptReference/Transform.SetParent.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.0/api/UnityEngine.XR.Interaction.Toolkit.SelectExitEvent.html
//https://forum.unity.com/threads/using-xrbaseinteractor-selectentered.1457695/
//https://forum.unity.com/threads/dynamically-adding-rigidbody-and-colliders-to-parts-of-a-character-makes-it-fly-off.1524976/
//https://stackoverflow.com/questions/67169231/applying-velocity-to-rigidbody-makes-it-float-instead-of-dashing
//https://forum.unity.com/threads/floating-problem-with-rigidbody-velocity-in-unity.771644/
//https://forum.unity.com/threads/solved-checking-if-getcomponent-meshfilter-mesh-null.340360/
//https://docs.unity3d.com/ScriptReference/MeshFilter-mesh.html
//https://discussions.unity.com/t/check-if-getcomponent-meshfilter-mesh-null-not-working/68762
//https://docs.unity3d.com/ScriptReference/Vector3-zero.html
//https://docs.unity3d.com/ScriptReference/Transform.TransformPoint.html
//https://docs.unity3d.com/560/Documentation/ScriptReference/Transform.TransformPoint.html
//https://forum.unity.com/threads/whats-the-math-behind-transform-transformpoint.107401/
//https://irfanbaysal.medium.com/differences-between-transformvector-transformpoint-and-transformdirection-2df6f3ebbe11
//https://discussions.unity.com/t/how-would-one-calculate-a-3d-mesh-volume-in-unity/16895
//https://stackoverflow.com/questions/1406029/how-to-calculate-the-volume-of-a-3d-mesh-object-the-surface-of-which-is-made-up-t
//https://docs.unity3d.com/ScriptReference/Mathf.Round.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//https://forum.unity.com/threads/audio-source-returning-null.1437709/
//https://discussions.unity.com/t/audio-source-is-null-when-changing-scene/218356
//https://docs.unity3d.com/ScriptReference/AudioClip.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable.html
//https://docs.unity3d.com/ScriptReference/Events.UnityEvent.RemoveListener.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs.html
//https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/api/UnityEngine.XR.Interaction.Toolkit.SelectExitEventArgs.html
//https://docs.unity3d.com/ScriptReference/GameObject.CompareTag.html
//https://forum.unity.com/threads/what-does-other-mean.182025/
//https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
//https://docs.unity3d.com/ScriptReference/AudioSource.PlayOneShot.html
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.Invoke.html
//https://forum.unity.com/threads/sound-not-playing-on-box-collision.1142656/
//https://discussions.unity.com/t/play-sound-on-collision-doesnt-work/159523
