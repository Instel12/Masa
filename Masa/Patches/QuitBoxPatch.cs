using HarmonyLib;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(GorillaQuitBox), nameof(GorillaQuitBox.OnBoxTriggered))]
    class QuitBoxPatch
    {
        public static bool DisableQuitBox = true;
        static bool Prefix(GorillaQuitBox __instance)
        {
            return !DisableQuitBox;
        }
    }
}
