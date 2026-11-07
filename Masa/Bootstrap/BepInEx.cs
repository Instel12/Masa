using BepInEx;
using GorillaLocomotion;
using GorillaTag;
using UnityEngine;

namespace Masa.Bootstrap
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Title, PluginInfo.Version)]
    public class BepInEx : BaseUnityPlugin
    {
        void Awake() => new HarmonyLib.Harmony(PluginInfo.GUID).PatchAll();
    }
}
