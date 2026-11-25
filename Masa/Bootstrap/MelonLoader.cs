#if false
using MelonLoader;
using Masa;

[assembly: MelonInfo(typeof(Masa.Bootstrap.MelonLoader), PluginInfo.Title, PluginInfo.Version, PluginInfo.Author)]
namespace Masa.Bootstrap
{
    class MelonLoader : MelonMod
    {
        public override void OnInitializeMelon() => new HarmonyLib.Harmony(PluginInfo.GUID).PatchAll();
    }
}
#endif