using UnityEngine;

namespace testTask
{
    public interface IDestroyedObserver
    {
        void OnObjectDestroyed(GameObject destroyedObject, Transform destroyedTransform);
    }
}
