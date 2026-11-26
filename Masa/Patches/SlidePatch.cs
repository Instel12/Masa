using GorillaLocomotion;
using HarmonyLib;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(GTPlayer), nameof(GTPlayer.GetSlidePercentage))]
    class SlidePatch
    {
        public static bool AllRedWalls = false;
        public static bool NoSlip = false;

        public static void Postfix(ref float __result)
        {
            if (AllRedWalls) __result = 1;
            if (NoSlip) __result = 0;
        }
    }
}
