namespace Masa.Classes
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    public class WebLib : MonoBehaviour
    {
        public static void Request(string url, Action<string> callback)
        {
            WebLib temp = new GameObject("Web Request").AddComponent<WebLib>();
            temp.StartCoroutine(temp.request(url, callback));
        }

        private IEnumerator request(string url, Action<string> callback)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    callback.Invoke(null);
                }
                else
                {
                    callback.Invoke(request.downloadHandler.text);
                }
                Debug.Log("Result" + request.result);
            }

            Destroy(gameObject);
        }
    }
}