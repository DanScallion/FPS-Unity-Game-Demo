using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testTask
{
    public class RigidbodyController : MonoBehaviour
    {
        public GameObject cameraObj;
        public float playerAccelerationOnGround=7.5f;
        public float playerAccelerationInAir;

        public float maxWalkSpeed=10.5f;
        public float maxRunSpeed = 20.5f;
        public float decelerationRate = 2;

        private Vector2 horizontalVelocity;

        private float walkDecelerationX;
        private float walkDecelerationZ;

        public bool isGrounded = true; //kontakt z ziemią
        private Rigidbody playerRigidBody;
        public float jumpForce = 450.0f; //Siła skoku

        public bool isDebugVisualiseRayCast = false; //Wizualizuj wiązki w widoku sceny.


        void Awake()
        {
            playerRigidBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Jump();
            Move();
            CheckGroundStatus();
        }

        void Move()
        {
            horizontalVelocity = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.z);

            //  Kontroluj prędkość przyspieszania w zależności od tego, czy jest naciśnięty klawisz "Shift".
            float accelerationSpeed = Input.GetKey(KeyCode.LeftShift) ? maxRunSpeed : maxWalkSpeed;
            float maxMoveSpeed = accelerationSpeed;

            if (horizontalVelocity.magnitude > maxMoveSpeed)
            {
                horizontalVelocity = horizontalVelocity.normalized;
                horizontalVelocity *= maxMoveSpeed;
            }

            playerRigidBody.velocity = new Vector3(horizontalVelocity.x, playerRigidBody.velocity.y, horizontalVelocity.y);

            transform.rotation = Quaternion.Euler(0, cameraObj.GetComponent<CameraController>().smoothedVerticalRotation, 0);

            if (isGrounded)
            {
                // Dostosuj przyspieszenie na podstawie klawisza "Shift".
                float acceleration = Input.GetKey(KeyCode.LeftShift) ? playerAccelerationOnGround * 2 : playerAccelerationOnGround;
                playerRigidBody.AddRelativeForce(Input.GetAxis("Horizontal") * acceleration, 0, Input.GetAxis("Vertical") * acceleration);

                float xMove = Mathf.SmoothDamp(playerRigidBody.velocity.x, 0, ref walkDecelerationX, decelerationRate);
                float zMove = Mathf.SmoothDamp(playerRigidBody.velocity.z, 0, ref walkDecelerationZ, decelerationRate);
                playerRigidBody.velocity = new Vector3(xMove, playerRigidBody.velocity.y, zMove);
            }
            else
            {
                // Dostosuj przyspieszenie na podstawie klawisza "Shift".
                float acceleration = Input.GetKey(KeyCode.LeftShift) ? playerAccelerationInAir * 2 : playerAccelerationInAir;
                playerRigidBody.AddRelativeForce(Input.GetAxis("Horizontal") * acceleration, 0, Input.GetAxis("Vertical") * acceleration);
            }
        }


        void CheckGroundStatus()
        {
            // Określ liczbę promieni (raycasts) i ich pozycje.
            int numRaycasts = 5;
            Vector3[] raycastOrigins = new Vector3[numRaycasts * 2]; // Podwój rozmiar tablicy dla wykrywania lewo/prawo.

            // Określ maksymalny zasięg raycasts.
            float maxDistance = 1;

            // Oblicz pozycje raycast do wykrywania przodu/tyłu i lewo/prawo.
            for (int i = 0; i < numRaycasts; i++)
            {
                float zOffset = (i - (numRaycasts - 1) * 0.5f) * 0.2f;
                raycastOrigins[i] = transform.position + new Vector3(0, -0.5f, zOffset);

                float xOffset = (i - (numRaycasts - 1) * 0.5f) * 0.2f;
                raycastOrigins[i + numRaycasts] = transform.position + new Vector3(xOffset, -0.5f, 0);
            }

            isGrounded = false; // Załóż brak kontaktu z ziemią, dopóki nie zostanie udowodnione inaczej.

            // Wykonaj raycasty z każdej pozycji.
            foreach (Vector3 origin in raycastOrigins)
            {
                Ray landingRay = new Ray(origin, Vector3.down);

                if (Physics.Raycast(landingRay, maxDistance))
                {
                    isGrounded = true;
                }

                // Wizualizuj wiązki w widoku sceny.
                if(isDebugVisualiseRayCast)
                    Debug.DrawRay(origin, Vector3.down * maxDistance, Color.red);
            }
        }


        void Jump()
        {
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                playerRigidBody.AddForce(0, jumpForce, 0);
                isGrounded = false;
            }
        }
    }
}
