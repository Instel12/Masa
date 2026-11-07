using HarmonyLib;
using Photon.Pun;

namespace Masa.Patches.Safety
{
    class AntiRPCIncrement
    {
        [HarmonyPatch(typeof(VRRig), "IncrementRPC", typeof(PhotonMessageInfoWrapped), typeof(string))]
        public class DontDoIt
        {
            private static bool Prefix(PhotonMessageInfoWrapped info, string sourceCall)
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(MonkeAgent), "IncrementRPCCall", typeof(PhotonMessageInfo), typeof(string))]
        public class AntiCall
        {
            private static bool Prefix(PhotonMessageInfo info, string callingMethod = "")
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(MonkeAgent), "IncrementRPCCallLocal")]
        public class NoLocalCall
        {
            private static bool Prefix(PhotonMessageInfoWrapped infoWrapped, string rpcFunction)
            {
                return false;
            }
        }
    }
}
