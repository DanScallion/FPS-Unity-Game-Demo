using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testTask
{
    public class ObjectMaterial : MonoBehaviour
    {
        public enum MaterialType
        {
            Wood,
            Metal,
            CartoonBox,
            Glass,
            HumanBody,
            // Dodaj więcej rodzajów materiałów, jeśli jest to potrzebne.
        }

        public MaterialType material;

        // Możesz również dodać inne właściwości specyficzne dla materiału, takie jak wytrzymałość, kolor itp.
        public float strength;
        public Color color;
        public bool applyMaterialProperties = false;

        private void Start()
        {
            ApplyMaterialProperties();
        }


        // Ta metoda może być używana do zastosowania właściwości materiału do obiektu.
        public void ApplyMaterialProperties()
        {
            if (applyMaterialProperties)
            {
                Renderer objectRenderer = GetComponent<Renderer>();

                switch (material)
                {
                    case MaterialType.Wood:
                        // Zastosuj właściwości materiału (np. teksturę, kolor) do renderera obiektu.
                        // Tutaj możesz także ustawić inne właściwości specyficzne dla materiału.
                        objectRenderer.material.color = color;
                        break;

                    case MaterialType.Metal:
                        objectRenderer.material.color = color;
                        break;

                    case MaterialType.CartoonBox:
                        objectRenderer.material.color = color;
                        break;
                    case MaterialType.Glass:

                        objectRenderer.material.color = color;
                        break;
                    case MaterialType.HumanBody:

                        objectRenderer.material.color = color;
                        break;
                }
            }
        }
    }
}