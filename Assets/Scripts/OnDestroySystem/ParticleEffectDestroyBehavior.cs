using UnityEngine;

namespace testTask
{
    [CreateAssetMenu(fileName = "ParticleEffectBehavior", menuName = "DestroyBehaviors/Particle Effect")]
    public class ParticleEffectDestroyBehavior : ObjectDestroyBehavior
    {
        public ParticleSystem destructionParticles; // Reference to the particle system for destruction effect.

        public override void ExecuteBehavior(Transform destroyedTransform)
        {
            if (destructionParticles != null)
            {
                // Instantiate the destruction particles at the object's position.
                Instantiate(destructionParticles, destroyedTransform.position, Quaternion.identity);
            }
        }
    }
}