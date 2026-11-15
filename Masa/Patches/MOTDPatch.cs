using System;
using System.Security.Policy;
using GorillaNetworking;
using HarmonyLib;
using Masa.Classes;
using Masa.Menu;
using TMPro;
using UnityEngine;

namespace Masa.Patches
{
    [HarmonyPatch(typeof(PlayFabTitleDataTextDisplay), "OnTitleDataRequestComplete")]
    class MOTDPatch
    {
        public static TextMeshPro Header;
        public static TextMeshPro Body;

        public static void FindThem()
        {
            if (Header == null) Header = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdHeadingText").GetComponent<TextMeshPro>();
            if (Body == null) Body = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdBodyText").GetComponent<TextMeshPro>();
        }

        public static void CustomMOTD()
        {
            FindThem();

            Header.characterSpacing = 0;
            Body.characterSpacing = 0;
            Main.RegisterTMP(Header);
            Main.RegisterTMP(Body);
            Header.text = PluginInfo.Title;
            Body.text = $"Thank you for choosing {PluginInfo.Title}!\n\nIf you use your head and your menu is up to date, you should be fine.\nAll mods: {Buttons.Buttans.Count}\nVersion: {PluginInfo.Version}\n\n<color=red>Nothing that happens to you while using this menu is on me.</color>\n\nMOTD: {Main.MOTDs[DateTime.Today.DayOfYear % Main.MOTDs.Length]}";
        }

        public static void StockMOTD()
        {
            FindThem();

            Main.UnregisterTMP(Header);
            Main.UnregisterTMP(Body);

            Header.text = "MESSAGE OF THE DAY";
            PlayFabTitleDataCache.Instance.GetTitleData(
                PrivateManager.GetField<string>(Body.GetComponent<PlayFabTitleDataTextDisplay>(), "playfabKey"),
                result => Body.text = result.Replace("\\r", "\r").Replace("\\n", "\n").Trim('"'),
                error => { }
            );
        }

        static bool Prefix(PlayFabTitleDataTextDisplay __instance)
        {
            return false;
        }
    }
}
