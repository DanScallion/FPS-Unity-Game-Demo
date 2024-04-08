using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testTask
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 7.5f;
        public float runSpeed = 11.5f;
        public float jumpForce = 8.0f;
        public float gravity = 9.81f;
        public Camera playerCamera;
        public float lookSensitivity = 2.0f;
        public float lookXLimit = 45.0f;

        CharacterController characterController;
        Vector3 movement = Vector3.zero;
        float rotationX = 0;

        [HideInInspector]
        public bool canMove = true;

        void Start()
        {
            characterController = GetComponent<CharacterController>();

            //  Zablokuj kursor.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            //  Oblicz kierunek ruchu na podstawie wejścia.
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float moveSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float moveSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float verticalMovement = movement.y;
            movement = (forward * moveSpeedX) + (right * moveSpeedY);

            // Apply jump force
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                movement.y = jumpForce;
            }
            else
            {
                movement.y = verticalMovement;
            }

            // Zastosuj siłę skoku.
            if (!characterController.isGrounded)
            {
                movement.y -= gravity * Time.deltaTime;
            }

            //  Porusz kontrolerem.
            characterController.Move(movement * Time.deltaTime);

            //  Rotacja gracza i kamery.
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSensitivity;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSensitivity, 0);
            }
        }
    }
}