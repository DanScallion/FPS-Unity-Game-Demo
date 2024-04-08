using UnityEngine;

namespace testTask
{
    public abstract class ObjectDestroyBehavior : ScriptableObject
    {
        public abstract void ExecuteBehavior(Transform destroyedTransform);
    }

}