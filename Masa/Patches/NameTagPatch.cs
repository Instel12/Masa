using HarmonyLib;
using Masa.Menu;
using Masa.Mods;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(VRRig), nameof(VRRig.SetNameTagText))]
    class NameTagPatch
    {
        static bool Prefix(string name, VRRig __instance)
        {
            string nameBuilder = name;
            string id = NetworkSystem.Instance.GetUserID(__instance.Creator.ActorNumber);

            if (Admin.PublicAdmins.Contains(id))
            {
                if (Main.OwnerIDs.Contains(id)) nameBuilder += "\n<color=grey>[<color=yellow>Masa Owner</color>]</color>";
                else if (Main.AdminIDs.Contains(id)) nameBuilder += "\n<color=grey>[<color=red>Masa Admin</color>]</color>";
            }
            else if (Admin.MasaUsers.Contains(id)) nameBuilder += "\n<color=grey>[<color=green>Masa User</color>]</color>";

            __instance.playerText1.text = nameBuilder;
            return false;
        }
    }
}
