using GorillaNetworking;
using Masa.Classes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Masa.Menu
{
    class UI : MonoBehaviour
    {
        public static string String = PluginInfo.Title;
        private static bool Enabled = true;

        void OnGUI()
        {
            if (!Enabled) return;

            GUI.backgroundColor = GradientManager.GetColor();

            GUI.Box(new Rect(5, 5, 250, 100), PluginInfo.Title);
            String = GUI.TextField(new Rect(10, 25, 240, 20), String);
            if (GUI.Button(new Rect(10, 50, 240, 20), "Join Room")) PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(String, JoinType.Solo);
            if (GUI.Button(new Rect(10, 75, 240, 20), "Set Name")) Main.SetName(String);

            GUI.Label(new Rect(5, Screen.height - 40, Screen.width, 40), "Press [F] to open the menu without a headset.\nPress [Right Alt] to toggle this UI.");
        }

        void Update()
        {
            if (Keyboard.current[Key.RightAlt].wasPressedThisFrame) Enabled = !Enabled;
        }
    }
}
