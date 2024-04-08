using UnityEngine;
using UnityEngine.Events;

namespace testTask
{
    public class DestroyableObject : MonoBehaviour
    {
        public enum DestructionType
        {
            ParticleEffect,
            CustomFunction,
            Both
        }

        public DestructionType destructionType;
        public ParticleSystem destructionEffect;
        public UnityEvent customFunctionEvent; //  Unity Event do wywołania niestandardowej funkcji.
        public float particleLife = 5f;

        private void OnDestroy()
        {
            if (gameObject.scene.isLoaded) // Sprawdź, czy scena obiektu jest nadal załadowana
            {
                // Obsłuż przypadki, w których obiekt został usunięty podczas rozgrywki

                if (destructionType == DestructionType.ParticleEffect || destructionType == DestructionType.Both)
                {
                    if (destructionEffect != null)
                    {
                        // Wywołaj efekt cząsteczkowy
                        ParticleSystem instantiatedEffect = Instantiate(destructionEffect, transform.position, transform.rotation);

                        // Zniszcz cząstkowy efekt po określonym czasie
                        Destroy(instantiatedEffect.gameObject, particleLife);
                    }
                }

                if (destructionType == DestructionType.CustomFunction || destructionType == DestructionType.Both)
                {
                    // Wywołaj niestandardową funkcję Unity
                    customFunctionEvent.Invoke();
                }
            }
            else // Obsłuż przypadki, w których obiekt został usunięty podczas zamykania sceny
            {

            }
        }

    }
}