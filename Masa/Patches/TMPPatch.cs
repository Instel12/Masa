using HarmonyLib;
using Masa.Classes;
using Masa.Menu;
using TMPro;
using UnityEngine;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(TextMeshPro), "OnEnable")]
    class TMPPatch
    {
        static void Prefix(TextMeshPro __instance)
        {
            if (__instance.gameObject.GetComponent<TMPHelper>() == null)
            {
                //Main.FormatTMP(__instance);
            }
        }
    }
}
