using GorillaLocomotion;
using HarmonyLib;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(GTPlayer), nameof(GTPlayer.GetSlidePercentage))]
    class SlidePatch
    {
        public static bool AllRedWalls = false;
        public static bool AllBlueWalls = false;
        public static bool NoSlip = false;

        public static void Postfix(ref float __result)
        {
            if (AllRedWalls) __result = 1;
            if (AllBlueWalls) __result *= 0.75f;
            if (NoSlip) __result = 0;
        }
    }
}
