using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testTask
{
    public class CameraController : MonoBehaviour
    {
        public float mouseXsens = 15f;
        public float mouseYsens = 14f;
        public float smoothDamp = .5f;


        float xRotation=0f, yRotation=0f;

        public float smoothedVerticalRotation, smoothedHorizontalRotation;

        public float verticalRotationVelocity, horizontalRotationVelocity;

        void Start()
        {
            // Zablokuj kursor i ukryj go na starcie.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        
        void LateUpdate()
        {
            // Odczytaj ruch myszy i aktualizuj obroty kamery.
            yRotation += Input.GetAxisRaw("Mouse X")  * mouseXsens;
            xRotation += Input.GetAxisRaw("Mouse Y")  * mouseYsens;

            // Wygładź obroty kamery.
            smoothedHorizontalRotation = Mathf.SmoothDamp(smoothedHorizontalRotation, xRotation, ref horizontalRotationVelocity, smoothDamp);
            smoothedVerticalRotation = Mathf.SmoothDamp(smoothedVerticalRotation, yRotation, ref verticalRotationVelocity, smoothDamp);

            // Ogranicz obroty kamery w pionie.
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            // Ustaw nową rotację kamery.
            transform.rotation = Quaternion.Euler(-smoothedHorizontalRotation, smoothedVerticalRotation, 0);
        }
    }
}