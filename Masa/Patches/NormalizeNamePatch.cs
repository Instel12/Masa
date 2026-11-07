using HarmonyLib;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(VRRig), nameof(VRRig.NormalizeName))]
    class NormalizeNamePatch
    {
        public static bool Enabled = true;
        static bool Prefix(bool doIt, string text, ref string __result)
        {
            if (!Enabled) return true;

            __result = text;
            return false;
        }
    }
}
