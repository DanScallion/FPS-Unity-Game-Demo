using System.Collections.Generic;
using UnityEngine;

namespace testTask
{
    public class ObjectDestroyManager : MonoBehaviour
    {
        private static ObjectDestroyManager instance;

        public static ObjectDestroyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ObjectDestroyManager>();
                    if (instance == null)
                    {
                        GameObject manager = new GameObject("ObjectDestroyManager");
                        instance = manager.AddComponent<ObjectDestroyManager>();
                    }
                }
                return instance;
            }
        }

        private Dictionary<GameObject, ObjectDestroyBehavior> destroyBehaviors = new Dictionary<GameObject, ObjectDestroyBehavior>();

        public void RegisterObject(GameObject obj, ObjectDestroyBehavior destroyBehavior)
        {
            if (!destroyBehaviors.ContainsKey(obj))
            {
                destroyBehaviors[obj] = destroyBehavior;
            }
        }

        public void UnregisterObject(GameObject obj)
        {
            destroyBehaviors.Remove(obj);
        }

        public void DestroyObject(GameObject obj)
        {
            if (destroyBehaviors.ContainsKey(obj))
            {
                // Execute the destroy behavior associated with the object.
                destroyBehaviors[obj].ExecuteBehavior(obj.transform);
                destroyBehaviors.Remove(obj);
            }
            Destroy(obj);
        }
    }
}