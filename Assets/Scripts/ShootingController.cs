using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace testTask
{
    public class ShootingController : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float bulletSpeed = 10f;
        public Transform[] bulletSpawnPoints; // Tablica punktów spawnu pocisków
        public Animator slideAnimator;        // Odwołanie do komponentu Animatora zamka
        public Animator recoilAnimator;
        public float reloadDelay = 1f; // Dostosuj długość opóźnienia według potrzeb.

        // Właściwości magazynka
        public int maxCapacity = 10;          // Maksymalna liczba pocisków w magazynku.
        public int currentBullets;           // Aktualna liczba pocisków w magazynku
        public int ReserveAmmo = 20;
        [SerializeField]
        private bool canShoot = true;         // Zmienna kontrolująca opóźnienie strzelania.
        public bool shouldPlayFireAnimation = true;

        // Właściwości interfejsu użytkownika
        public TMP_Text ammoText;             // Odwołanie do elementu TextMeshPro Text do wyświetlania ilości amunicji.

        private void Start()
        {
            currentBullets = maxCapacity;      // Zainicjowane z pełnym magazynkiem.
            UpdateAmmoUI();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
            {
                if (currentBullets > 0)
                {
                    foreach (Transform spawnPoint in bulletSpawnPoints)
                    {
                        if (shouldPlayFireAnimation)
                        {
                            // Aktywuj animację "Fire" na zamku.
                            if (slideAnimator != null)
                            {
                                slideAnimator.SetTrigger("Fire");
                            }
                        }
                        if (recoilAnimator != null)
                        {
                            recoilAnimator.SetTrigger("Recoil");
                        }

                        //  Instancjonuj pocisk na punkcie spawnu.
                        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);


                        // Przydaj stałą prędkość pociskowi.
                        bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletSpeed;
                    }

                    // Zmniejszaj liczbę currentBullets po oddaniu strzału.
                    currentBullets--;

                    canShoot = false;

                    // Aktualizuj interfejs użytkownika amunicji.
                    UpdateAmmoUI();

                    // Dodaj opóźnienie przed kolejnym strzałem gracza.
                    StartCoroutine(ShootDelay());
                }
                else
                {
                    //Brak amunicji, nie można strzelać. Tutaj można dodać efekt dźwiękowy lub informację zwrotną.
                }
            }

            //Sprawdź, czy użytkownik nacisnął klawisz przeładowania.
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }

        private IEnumerator ShootDelay()
        {
            yield return new WaitForSeconds(reloadDelay);
            canShoot = true;
        }

        private void Reload()
        {
            if (currentBullets < maxCapacity)
            {
                bool wasEmptyBeforeReloading = currentBullets == 0;

                int bulletsToReload = maxCapacity - currentBullets;

                // Sprawdź, czy gracz ma wystarczającą ilość zapasowej amunicji do przeładowania.
                if (bulletsToReload <= ReserveAmmo)
                {
                    currentBullets += bulletsToReload;
                    ReserveAmmo -= bulletsToReload;
                }
                else
                {
                    currentBullets += ReserveAmmo;
                    ReserveAmmo = 0;
                }

                UpdateAmmoUI();

                // Jeśli magazynek był pusty przed przeładowaniem, odtwórz animację strzału.
                if (wasEmptyBeforeReloading && currentBullets > 0)
                {
                    slideAnimator.SetTrigger("Fire");
                }
            }
        }



        public void UpdateAmmoUI()
        {
            if (ammoText != null)
            {
                ammoText.text = currentBullets + " / " + ReserveAmmo;
            }
        }

        public void ResetAnimation()
        {
            canShoot = true;

            // zresetowania animacji
            if (slideAnimator != null)
            {
                slideAnimator.ResetTrigger("Fire");
                slideAnimator.Rebind();
                slideAnimator.Update(0f);
            }
            if (recoilAnimator != null)
            {
                recoilAnimator.ResetTrigger("Recoil");
                recoilAnimator.Rebind();
                recoilAnimator.Update(0f);

            }
        }
    }
}