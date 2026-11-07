// this is just in case they log who sends these and it just protects you if they do
// i cant tell cause it wont decompile

using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace Masa.Patches
{
    [HarmonyPatch]
    public class DearLemmingPatch
    {
        static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(Component), nameof(Component.SendMessage), new[] { typeof(string), typeof(object) });
        }

        static bool Prefix(Component __instance)
        {
            if (__instance is DearLemmingController)
            {
                return false;
            }

            return true;
        }
    }
}