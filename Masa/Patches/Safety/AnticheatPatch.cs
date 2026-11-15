using HarmonyLib;
using Masa.Menu;

namespace Masa.Patches.Safety
{
    [HarmonyPatch(typeof(MonkeAgent), nameof(MonkeAgent.SendReport))]
    static class AnticheatPatch
    {
        static bool Prefix(string susReason, string susId, string susNick)
        {
            if (susReason.ToLower() != "empty rig")
                Notifications.SendNotification($"[<color=red>Anti Cheat</color>] {susNick} has been reported for {susReason}.");

            return false;
        }
    }
}