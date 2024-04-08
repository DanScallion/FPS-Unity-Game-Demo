using UnityEngine;

namespace testTask
{
    public class ObjectDestroyObserver : MonoBehaviour
    {
        public ObjectDestroyBehavior destroyBehavior; // Assign the destroy behavior asset in the inspector.

        private void Start()
        {
            // Register this object with the ObjectDestroyManager.
            ObjectDestroyManager.Instance.RegisterObject(gameObject, destroyBehavior);
        }

        private void OnDestroy()
        {
            // Unregister this object when it's destroyed.
            ObjectDestroyManager.Instance.UnregisterObject(gameObject);
        }
    }
}