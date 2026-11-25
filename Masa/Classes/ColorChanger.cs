using UnityEngine;

namespace Masa.Classes
{
    class ColorChanger : MonoBehaviour
    {
        private Renderer rend;
        void Update()
        {
            if (rend == null) rend = GetComponent<Renderer>();
            rend.material.color = GradientManager.GetColor();
        }
    }
}
