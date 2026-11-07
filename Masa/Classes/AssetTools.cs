using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using Masa.Menu;
using UnityEngine;
using UnityEngine.Networking;

namespace Masa.Classes
{
    class AssetTools
    {
        public static AssetBundle LoadBundle(string path)
        {
            string RealPath = path.Replace("/", ".");

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(RealPath);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();

            return bundle;
        }

        private static Dictionary<string, AssetBundle> BundleCache = new Dictionary<string, AssetBundle>();
        public static IEnumerator DownloadBundle(string url, Action<AssetBundle> callback)
        {
            if (BundleCache.TryGetValue(url, out AssetBundle cachedBundle))
            {
                callback?.Invoke(cachedBundle);
                yield break;
            }

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                Notifications.SendNotification("[<color=blue>Asset Bundle</color>] Downloading bundle...");

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    callback.Invoke(null);
                    yield break;
                }

                AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromMemoryAsync(request.downloadHandler.data);

                while (!bundleRequest.isDone) yield return null;

                AssetBundle bundle = bundleRequest.assetBundle;

                if (bundle == null)
                {
                    Notifications.SendNotification("[<color=red>Darn</color>] Failed to load bundle!");
                    callback.Invoke(null);
                    yield break;
                }

                BundleCache[url] = bundle;
                callback.Invoke(bundle);
            }
        }

        public static Dictionary<string, AudioClip> AudioCache = new Dictionary<string, AudioClip>();

        public static IEnumerator LoadAudioLocal(string fileName, Action<AudioClip> callback)
        {
            AudioClip cached;
            if (AudioCache.TryGetValue(fileName, out cached))
            {
                callback?.Invoke(cached);
                yield break;
            }

            string filePath = Path.Combine(Paths.GameRootPath, PluginInfo.Title, "Soundboard", fileName);
            string url = $"file://{filePath}";

            var handler = new DownloadHandlerAudioClip(url, AudioType.UNKNOWN);

            using (UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET, handler, null))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    callback?.Invoke(null);
                    yield break;
                }

                AudioClip clip = handler.audioClip;
                AudioCache[fileName] = clip;

                callback?.Invoke(clip);
            }
        }
    }
}
