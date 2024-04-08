using UnityEngine;
using System.Collections;

namespace testTask
{
    public class DoorController : MonoBehaviour
    {
        public Transform door;         // Odwołanie do komponentu Transform drzwi.
        public float openDistance = 2f; // Odległość, o jaką drzwi będą się przesuwać w górę, gdy zostaną otwarte.
        public float openSpeed = 2f;    // Prędkość, z jaką drzwi się otwierają.
        public bool isOpen = false;

        private Vector3 closedPosition; // Pozycja zamkniętych drzwi.

        private void Start()
        {
            // Przechowaj początkową pozycję zamkniętych drzwi.
            closedPosition = door.position;
        }

        public void OpenDoor()
        {
            if (!isOpen)
            {
                StartCoroutine(OpenDoorCoroutine());
            }
        }

        public void CloseDoor()
        {
            if (isOpen)
            {
                StartCoroutine(CloseDoorCoroutine());
            }
        }

        private IEnumerator OpenDoorCoroutine()
        {
            isOpen = true;
            Vector3 targetPosition = closedPosition + Vector3.up * openDistance;
            float elapsedTime = 0f;

            while (elapsedTime < (openDistance / openSpeed))
            {
                door.position = Vector3.Lerp(closedPosition, targetPosition, elapsedTime / (openDistance / openSpeed));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            door.position = targetPosition;
        }

        private IEnumerator CloseDoorCoroutine()
        {
            isOpen = false;
            Vector3 targetPosition = closedPosition;
            float elapsedTime = 0f;

            while (elapsedTime < (openDistance / openSpeed))
            {
                door.position = Vector3.Lerp(door.position, targetPosition, elapsedTime / (openDistance / openSpeed));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            door.position = targetPosition;
        }
    }
}