using UnityEngine;
using UnityEngine.Events;

namespace testTask
{
    [CreateAssetMenu(fileName = "CustomDestroyBehavior", menuName = "DestroyBehaviors/Custom Destroy Behavior")]
    public class CustomDestroyBehavior : ObjectDestroyBehavior
    {
        public UnityEvent onDestroyEvent;

        public override void ExecuteBehavior(Transform destroyedTransform)
        {
            onDestroyEvent.Invoke();
            Debug.Log("ExecuteBehavior called");
        }
    }
}