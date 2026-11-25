using HarmonyLib;

namespace Masa.Patches
{
    class WindPatch
    {
        public static bool WindDisabled = false;

        [HarmonyPatch(typeof(ForceVolume), nameof(ForceVolume.OnTriggerEnter))]
        class WindEnterPatch
        {
            static bool Prefix()
            {
                return !WindDisabled;
            }
        }

        [HarmonyPatch(typeof(ForceVolume), nameof(ForceVolume.OnTriggerExit))]
        class WindExitPatch
        {
            static bool Prefix()
            {
                return !WindDisabled;
            }
        }

        [HarmonyPatch(typeof(ForceVolume), nameof(ForceVolume.OnTriggerStay))]
        class WindStayPatch
        {
            static bool Prefix()
            {
                return !WindDisabled;
            }
        }
    }
}
