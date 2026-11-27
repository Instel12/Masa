using Unity.Cinemachine;
using GorillaLocomotion;
using HarmonyLib;
using Masa.Menu;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(CinemachineBrain), "DoNonFixedUpdate")]
    class CineMachinePatch
    {
        public static bool FirstPersonView = false;

        static bool Prefix(CinemachineBrain __instance)
        {
            if (FirstPersonView)
            {
                __instance.OutputCamera.fieldOfView = 75;
                __instance.transform.position = GTPlayer.Instance.headCollider.transform.position;
                __instance.transform.rotation = GTPlayer.Instance.headCollider.transform.rotation;

                return false;
            }
            return true;
        }

        static void Postfix()
        {
            Main.HandleMenu();
        }
    }
}
