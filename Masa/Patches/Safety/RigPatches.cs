using HarmonyLib;

namespace Masa.Patches.Safety
{
    // credits to iiDk (i know, he's a racist, its still a working patch)
    class RigPatches
    {
        [HarmonyPatch(typeof(VRRig), nameof(VRRig.OnDisable))]
        public class OnDisable
        {
            public static bool Prefix(VRRig __instance)
            {
                return !__instance.isLocal;
            }
        }

        [HarmonyPatch(typeof(VRRig), nameof(VRRig.PostTick))]
        public class PostTick
        {
            public static bool Prefix(VRRig __instance)
            {
                return !__instance.isLocal || __instance.enabled;
            }
        }
    }
}
