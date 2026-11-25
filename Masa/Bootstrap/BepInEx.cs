using System.ComponentModel;
using BepInEx;

namespace Masa.Bootstrap
{
    [Description(PluginInfo.Description)]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Title, PluginInfo.Version)]
    public class BepInEx : BaseUnityPlugin
    {
        void Awake() => new HarmonyLib.Harmony(PluginInfo.GUID).PatchAll();
    }
}
