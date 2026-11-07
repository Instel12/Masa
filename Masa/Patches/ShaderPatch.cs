using HarmonyLib;
using UnityEngine;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(GameObject), nameof(GameObject.CreatePrimitive))]
    static class ShaderPatch
    {
        static void Postfix(GameObject __result)
        {
            __result.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Unlit");
        }
    }
}
