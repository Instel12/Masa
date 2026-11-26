using HarmonyLib;

namespace Masa.Patches
{
    class PieceOwnedPatch
    {
        public static bool Enabled = false;

        [HarmonyPatch(typeof(BuilderSetManager), nameof(BuilderSetManager.IsPieceOwnedLocally))]
        class PieceLocal
        {
            static bool Prefix(ref bool __result)
            {
                if (Enabled)
                {
                    __result = true;
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(BuilderSetManager), nameof(BuilderSetManager.IsPieceSetOwnedLocally))]
        class SetLocal
        {
            static bool Prefix(ref bool __result)
            {
                if (Enabled)
                {
                    __result = true;
                    return false;
                }
                return true;
            }
        }

        // these are a useless security measure so they arent toggleable cuz theres no point
        [HarmonyPatch(typeof(BuilderSetManager), nameof(BuilderSetManager.IsPieceOwnedByRoom))]
        class PieceRoom
        {
            static bool Prefix(ref bool __result)
            {
                __result = true;
                return false;
            }
        }

        [HarmonyPatch(typeof(BuilderSetManager), nameof(BuilderSetManager.DoesAnyPlayerInRoomOwnPieceSet))]
        class SetRoom
        {
            static bool Prefix(ref bool __result)
            {
                __result = true;
                return false;
            }
        }
    }
}
