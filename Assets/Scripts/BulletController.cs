using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testTask
{
    public class BulletController : MonoBehaviour
    {
        public float speed = 10f;
        public float damage = 10f;
        public float lifetime = 5f; // Czas w sekundach przed zniszczeniem pocisku.
        public List<ObjectMaterial.MaterialType> destroyableMaterials;

        private int bulletsLayer; // Indeks warstwy dla pocisków.

        private void Start()
        {
            //  Automatycznie zniszcz pocisk po określonym czasie życia.
            Destroy(gameObject, lifetime);

            // Pobierz indeks warstwy dla pocisków.
            bulletsLayer = LayerMask.NameToLayer("Bullets");
        }

        private void Update()
        {
            // Poruszaj pocisk do przodu z stałą prędkością.
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            // Sprawdź, czy obiekt ma skrypt ObjectMaterial.
            ObjectMaterial objectMaterial = other.GetComponent<ObjectMaterial>();

            //  Ignoruj kolizje z obiektami na warstwie "Bullets".
            if (other.gameObject.layer == bulletsLayer)
            {
                return;
            }

            if (objectMaterial != null)
            {
                // Sprawdź, czy materiał obiektu pasuje do któregoś z materiałów podatnych na zniszczenie.
                if (destroyableMaterials.Contains(objectMaterial.material))
                {
                    //Jeśli to materiał podatny na zniszczenie, zastosuj obrażenia do obiektu lub zniszcz go.
                    if (objectMaterial.strength > damage)
                    {
                        // Zadaj obrażenia obiektowi, jeśli może być uszkodzony
                        objectMaterial.strength -= damage;
                    }
                    else
                    {
                        //  Zniszcz obiekt, jeśli nie ma już wytrzymałości.
                        //ObjectDestroyManager.Instance.NotifyObjectDestroyed();
                        //ObjectDestroyManager.Instance.DestroyObject(other.gameObject);
                        Destroy(other.gameObject);
                    }

                    //  Zniszcz pocisk.
                    Destroy(gameObject);
                }
            }
            else
            {
                // Zniszcz pocisk, jeśli trafi obiekt bez skryptu ObjectMaterial.
                Destroy(gameObject);
            }
        }
    }
}