using HarmonyLib;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(KIDManager), nameof(KIDManager.HasPermissionToUseFeature))]
    class KIDPatch
    {
        public static bool Bypass = false;
        static bool Prefix(ref bool __result)
        {
            if (Bypass)
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
}
