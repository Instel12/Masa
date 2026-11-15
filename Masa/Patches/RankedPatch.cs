using GorillaNetworking;
using HarmonyLib;
using Masa.Classes;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(PhotonNetworkController), nameof(PhotonNetworkController.AttemptToJoinRankedPublicRoom))]
    class RankedPatch
    {
        public static bool Enabled = false;

        public static bool Prefix(GorillaNetworkJoinTrigger triggeredTrigger, JoinType roomJoinType = JoinType.Solo)
        {
            if (Enabled)
            {
                PrivateManager.GetMethod(PhotonNetworkController.Instance, "AttemptToJoinRankedPublicRoomAsync").Invoke(PhotonNetworkController.Instance, new object[]
                {
                    triggeredTrigger,
                    RankedProgressionManager.Instance.GetRankedMatchmakingTier().ToString(),
                    "Quest",
                    roomJoinType
                });
            }
            return true;
        }
    }
}
