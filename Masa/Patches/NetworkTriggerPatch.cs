using GorillaNetworking;
using HarmonyLib;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(GorillaNetworkJoinTrigger), nameof(GorillaNetworkJoinTrigger.OnBoxTriggered))]
    class NetworkTriggerPatch
    {
        public static bool Disable;
        static bool Prefix()
        {
            return !Disable;
        }
    }
}
