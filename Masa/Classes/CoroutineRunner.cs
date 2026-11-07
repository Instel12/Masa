using System.Collections;
using UnityEngine;

namespace Masa.Classes
{
    class CoroutineRunner : MonoBehaviour
    {
        public static void Coroutine(IEnumerator coroutine)
        {
            GameObject obj = new GameObject();
            obj.AddComponent<CoroutineRunner>().StartCoroutine(coroutine);
        }
    }
}
