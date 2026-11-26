using HarmonyLib;
using Masa.Menu;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Masa.Patches.Safety
{
    //[HarmonyPatch]
    //class RPCPatch
    //{
    //    public static float Delay = 0f;

    //    static MethodBase TargetMethod()
    //    {
    //        return AccessTools.Method(typeof(PhotonView), nameof(PhotonView.RPC), new Type[] { typeof(string), typeof(RpcTarget), typeof(object[]) });
    //    }

    //    static bool Prefix()
    //    {
    //        if (Time.time < Delay)
    //        {
    //            Notifications.SendNotification("[<color=red>Anti Cheat</color>] You are calling like a lot of RPCs. Some have been blocked for your safety.");
    //            return false;
    //        }

    //        Delay = Time.time + 0.1f;
    //        return true;
    //    }
    //}

    [HarmonyPatch(typeof(PhotonNetwork), nameof(PhotonNetwork.RaiseEvent))]
    class RaiseEventPatch
    {
        public static int[] RiskyEvents = new int[] { 206, 208 }; // Credits to G4L4X7
        public static float Delay = 0f;

        static bool Prefix(byte eventCode)
        {
            if (!RiskyEvents.Contains(eventCode)) return true;

            if (Time.time < Delay)
            {
                Notifications.SendNotification($"[<color=red>Anti Cheat</color>] You called event {eventCode} a lot which has been known for being detected. Some have been blocked for your safety.");
                return false;
            }
            Delay = Time.time + 0.2f;
            return true;
        }
    }
}