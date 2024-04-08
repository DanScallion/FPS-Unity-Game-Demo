using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testTask
{
    public class AmmoPickup : MonoBehaviour
    {
        public int ammoAmount = 10; //  Ilość amunicji rezerwowej dla gracza.
        public float rotationSpeed = 30f;

        private void Start()
        {
            // Losowo obróć obiekt na początku.
            transform.Rotate(Vector3.forward, Random.Range(0f, 360f));
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Szukaj skryptu ShootingController w hierarchii potomków obiektu gracza.
                ShootingController shootingController = other.GetComponentInChildren<ShootingController>();

                if (shootingController != null)
                {
                    // Dodaj ammoAmount do rezerwy amunicji gracza.
                    shootingController.ReserveAmmo += ammoAmount;

                    //Zaktualizuj interfejs użytkownika, aby odzwierciedlić nową liczbę amunicji, używając referencji w ShootingController.
                    shootingController.UpdateAmmoUI();

                    // Tutaj możena dopisać dodatkowe funksje np. dźwięk podnoszenia.

                    // Zniszcz ammo pickup.
                    Destroy(gameObject);
                }
            }
        }
    }
}