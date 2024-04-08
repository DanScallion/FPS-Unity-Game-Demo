using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testTask
{
    public class WeaponManager : MonoBehaviour
    {
        public List<ShootingController> weapons = new List<ShootingController>(); // Lista broni do zarządzania
        private int currentWeaponIndex = 0; // Indeks aktualnie wyposażonej broni

        private void Start()
        {
            // Upewnij się, że bieżący indeks broni mieści się w prawidłowym zakresie
            currentWeaponIndex = Mathf.Clamp(currentWeaponIndex, 0, weapons.Count - 1);

            // Zdezaktywuj wszystkie bronie oprócz pierwszej
            for (int i = 1; i < weapons.Count; i++)
            {
                weapons[i].gameObject.SetActive(false);
            }

            // Ustaw początkową broń
            EquipWeapon(currentWeaponIndex);
        }

        private void Update()
        {
            // Zmień broń, używając kółka myszy
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                int newIndex = currentWeaponIndex + (int)Mathf.Sign(scroll);
                ChangeWeapon(newIndex);
            }

            // Zmień broń, używając klawiszy numerycznych
            for (int i = 0; i < weapons.Count; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    ChangeWeapon(i);
                }
            }
        }

        private void ChangeWeapon(int newIndex)
        {
            // Upewnij się, że nowy indeks mieści się w prawidłowym zakresie
            if (newIndex < 0 || newIndex >= weapons.Count)
            {
                return;
            }

            //zresetuj animacje
            weapons[currentWeaponIndex].ResetAnimation();


            // Zdejmij obecną broń.
            weapons[currentWeaponIndex].gameObject.SetActive(false);

            //Załóż nową broń.
            EquipWeapon(newIndex);

            // Wywołaj UpdateAmmoUI() na aktualnie wyposażonej broni, aby zaktualizować jej UI
            weapons[currentWeaponIndex].UpdateAmmoUI();
        }


        private void EquipWeapon(int newIndex)
        {
            // Upewnij się, że nowy indeks mieści się w prawidłowym zakresie
            if (newIndex < 0 || newIndex >= weapons.Count)
            {
                return;
            }

            // Ustaw nową broń jako aktywną
            weapons[newIndex].gameObject.SetActive(true);

            // Zaktualizuj indeks obecnej broni
            currentWeaponIndex = newIndex;
        }
    }
}