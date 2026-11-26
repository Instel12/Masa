using HarmonyLib;
using Masa.Classes;
using TMPro;

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
