using System;
using System.Collections.Generic;
using PlayFab;
using HarmonyLib;
using PlayFab.ClientModels;

namespace Masa.Patches.Safety
{
    [HarmonyPatch(typeof(PlayFabClientAPI), nameof(PlayFabClientAPI.UpdateUserTitleDisplayName))]
    public class PlayfabNamePatch
    {
        public static void Prefix(ref UpdateUserTitleDisplayNameRequest request, Action<UpdateUserTitleDisplayNameResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            request.DisplayName = RandomString(UnityEngine.Random.Range(3, 12));
        }
        
        public static string RandomString(int length = 4) // from seralyth so yeah
        {
            string random = "";
            for (int i = 0; i < length; i++)
            {
                int rand = UnityEngine.Random.Range(0, 36);
                char c = rand < 26
                    ? (char)('A' + rand)
                    : (char)('0' + (rand - 26));
                random += c;
            }

            return random;
        }
    }
}
