using System.Reflection;
using HarmonyLib;
using Masa.Classes;
using Masa.Menu;
using UnityEngine;

namespace Masa.Patches
{
    [HarmonyPatch]
    class HandTapPatch
    {
        public static bool OculusAntiReport = false;

        static MethodBase TargetMethod()
        {
            return AccessTools.Method(AccessTools.TypeByName("VRRigSerializer"), "OnHandTapRPCShared");
        }

        static void Prefix(object __instance, int audioClipIndex, bool isDownTap, bool isLeftHand, float handTapSpeed)
        {
            if (!OculusAntiReport || audioClipIndex != 67) return;

            VRRig rig = PrivateManager.GetField<VRRig>(__instance, "vrrig");

            GorillaPressableButton[] buttons = Object.FindObjectsByType<GorillaPressableButton>(FindObjectsSortMode.None);

            foreach (GorillaPressableButton btn in buttons)
            {
                bool leftNear = Vector3.Distance(rig.leftHandTransform.position,btn.transform.position) < 1f;
                bool rightNear = Vector3.Distance(rig.rightHandTransform.position, btn.transform.position) < 1f;

                if (leftNear || rightNear)
                {
                    Mods.Safety.SafetyAction();
                    Notifications.SendNotification("[<color=red>Anti Report</color>] Nearby button interaction detected.");
                    break;
                }
            }
        }
    }
}