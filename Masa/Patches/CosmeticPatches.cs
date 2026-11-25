using HarmonyLib;
using Masa.Menu;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(VRRig), "IsItemAllowed")]
    class CosmeticPatches
    {
        public static bool ViewOthersCosmetics = false;

        private static void Postfix(VRRig __instance, ref bool __result)
        {
            if (__instance.Creator.UserId == NetworkSystem.Instance.GetMyUserID() || ViewOthersCosmetics || Main.AdminIDs.Contains(NetworkSystem.Instance.GetUserID(__instance.Creator.ActorNumber)))
            {
                __result = true;
            }
        }
    }
}
