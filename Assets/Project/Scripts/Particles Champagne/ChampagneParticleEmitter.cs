using UnityEngine;

// START - This script was copied and slightly motified from Zenva Course.
// The code was created using documentation and additional external research materials. Please review the referenced links.

public class ChampagneParticleEmitter : MonoBehaviour
{
    private ParticleSystem champagneParticle;
    private float champagnePouringThreshold = 120f; // pouring angle in degrees 

    void Awake()
    {
        champagneParticle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // calculates angle between bottle up vector and global up vector
        float upwardAngle = Vector3.Angle(transform.parent.up, Vector3.up);

        // determines if bottle is tilted beyond set threshold
        bool emitter = upwardAngle > champagnePouringThreshold;

        // starts/stops emitting particles based on bottle angle
        if (emitter && !champagneParticle.isEmitting)
        {
            champagneParticle.Play();
        }
        else if (!emitter && champagneParticle.isEmitting)
        {
            champagneParticle.Stop();
        }
    }
}

// END - This script was copied and slightly motified from Zenva Course.
// The code was created using documentation and additional external research materials. Please review the referenced links.

// References:
//https://academy.zenva.com/product/the-complete-virtual-reality-game-development-course/
//https://docs.unity3d.com/ScriptReference/ParticleSystem.html
//https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
//https://docs.unity3d.com/ScriptReference/Vector3.Angle.html
//https://docs.unity3d.com/ScriptReference/ParticleSystem.Play.html
//https://docs.unity3d.com/ScriptReference/ParticleSystem.Stop.html