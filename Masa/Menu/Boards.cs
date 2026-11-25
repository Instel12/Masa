using HarmonyLib;
using Masa.Classes;
using UnityEngine;

namespace Masa.Menu
{
    class Boards
    {
        public static Material BoardMaterial;

        public static void InitBoards()
        {

        }

        public static void UpdateBoards()
        {
            if (BoardMaterial == null) BoardMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            BoardMaterial.color = GradientManager.GetColor();
        }

        [HarmonyPatch(typeof(JoinTriggerUI), nameof(JoinTriggerUI.SetState))]
        class JoinTriggerUIBoardPatch
        {
            static void Postfix(JoinTriggerUI __instance)
            {
                MeshRenderer rend = PrivateManager.GetField<MeshRenderer>(__instance, "screenBGRenderer");
                if (rend != null) rend.sharedMaterial = BoardMaterial;
            }
        }

        [HarmonyPatch(typeof(GorillaComputerTerminal), "Init")]
        class GorillaComputerTerminalBoardInitPatch
        {
            static void Postfix(GorillaComputerTerminal __instance)
            {
                __instance.monitorMesh.sharedMaterial = BoardMaterial;
            }
        }

        [HarmonyPatch(typeof(GorillaComputerTerminal), "OnMaterialsChanged")]
        class GorillaComputerTerminalBoardInitUpdate
        {
            static bool Prefix(GorillaComputerTerminal __instance)
            {
                __instance.monitorMesh.sharedMaterial = BoardMaterial;
                return false;
            }
        }
    }
}
