using HarmonyLib;
using Masa.Mods;
using Photon.Pun;
using UnityEngine;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(PhotonNetwork), nameof(PhotonNetwork.Disconnect))]
    class LeavePatch
    {
        static void Prefix()
        {
            foreach (GameObject obj in Admin.InstanitatedObjects)
            {
                GameObject.Destroy(obj);
            }
            Admin.InstanitatedObjects.Clear();
        }
    }
}
