using System;
using System.Collections.Generic;
using System.Linq;
using GorillaLocomotion;
using GorillaNetworking;
using Masa.Classes;
using Masa.Mods;
using Masa.Patches;
using TMPro;
using UnityEngine;
using static Masa.Mods.Settings;
using Random = UnityEngine.Random;

namespace Masa.Menu
{
    class  Buttons
    {
        public enum Categories
        {
            Main,
            Settings,
            Enabled,
            Favorite,

            Admin,
            Movement,
            Safety,
            General,
            Room,
            Emotes,
            Fun,
            Soundboard,
            Overpowered,
            Credits,

            Maps,
            EverySound,
        }

        public static List<Button> Buttans = new List<Button>
        {
            #region Main
                new Button { Category = Categories.Main, Title = "Settings", OneShot = true, Method =() => LoadCategory(Categories.Settings) },
                new Button { Category = Categories.Main, Title = "Admin", OneShot = true, Method =() => LoadCategory(Categories.Admin) },
                new Button { Category = Categories.Main, Title = "Enabled Mods", OneShot = true, Method =() => LoadCategory(Categories.Enabled) },
                new Button { Category = Categories.Main, Title = "Favorite Mods", OneShot = true, Method =() => LoadCategory(Categories.Favorite) },
                new Button { Category = Categories.Main, Title = "Movement", OneShot = true, Method =() => LoadCategory(Categories.Movement) },
                new Button { Category = Categories.Main, Title = "Room", OneShot = true, Method =() => LoadCategory(Categories.Room) },
                new Button { Category = Categories.Main, Title = "Fun", OneShot = true, Method =() => LoadCategory(Categories.Fun) },
                new Button { Category = Categories.Main, Title = "Soundboard", OneShot = true, Method =() => LoadCategory(Categories.Soundboard) },
                new Button { Category = Categories.Main, Title = "Emotes", OneShot = true, Method =() => LoadCategory(Categories.Emotes) },
                new Button { Category = Categories.Main, Title = "Safety", OneShot = true, Method =() => LoadCategory(Categories.Safety) },
                new Button { Category = Categories.Main, Title = "General", OneShot = true, Method =() => LoadCategory(Categories.General) },
                new Button { Category = Categories.Main, Title = "Overpowered", OneShot = true, Method =() => LoadCategory(Categories.Overpowered) },
                new Button { Category = Categories.Main, Title = "Credits", OneShot = true, Method =() => LoadCategory(Categories.Credits) },
            #endregion
            #region Enabled (this needs to be first always cause the menu is stupid)
                new Button { Category = Categories.Enabled, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },
            #endregion
            #region Favorite (this also needs to be first always cause the menu its also stupid)
                new Button { Category = Categories.Favorite, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },
            #endregion
            #region Settings
                new Button { Category = Categories.Settings, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },

                new Button { Category = Categories.Settings, Title = "Shuffle Font", ShuffleAction = value => Settings.ShuffleFont(value),
                    ShuffleValues = new Dictionary<string, Func<object>>
                    {
                        { "Minecraft", () => Main.MainBundle.LoadAsset<TMP_FontAsset>("Minecraft SDF") },
                        { "Comic Sans", () => Main.MainBundle.LoadAsset<TMP_FontAsset>("Comic Sans SDF") },
                        { "Agency", () =>Main.MainBundle.LoadAsset<TMP_FontAsset>("Agency SDF") },
                        { "Space Mono", () => Main.MainBundle.LoadAsset < TMP_FontAsset >("Space Mono SDF") },
                        { "Arial", () => TMP_Settings.defaultFontAsset },
                        { "Utopium", () => Resources.FindObjectsOfTypeAll<TMP_FontAsset>().FirstOrDefault(x => x.name.ToLower().Contains("utopium")) },
                    }
                },
                new Button { Category = Categories.Settings, Title = "Shuffle Theme", ShuffleAction = value => Settings.ShuffleTheme(value),
                    ShuffleValues = new Dictionary<string, Func<object>>
                    {
                        {
                            "Masa", () => new Theme
                            {
                                Colors = new List<Color>
                                {
                                    Color.black,
                                    new Color(0.5f, 0, 0),
                                    Color.black
                                }
                            }
                        },
                        {
                            "Hope Reborn", () => new Theme
                            {
                                Colors = new List<Color>
                                {
                                    Color.black,
                                    Color.midnightBlue,
                                    Color.black
                                }
                            }
                        },
                        {
                            "GRAND", () => new Theme // i know alot about this menu cause it was my first one
                            {
                                Colors = new List<Color> { Color.yellow },
                                FlushButtons = true
                            }
                        },
                        {
                            "Hudbud", () => new Theme
                            {
                                Colors = new List<Color> { Color.cyan }
                            }
                        },
                        {
                            "Dark", () => new Theme
                            {
                                Colors = new List<Color> { Color.black }
                            }
                        },
                        {
                            "ii's Stupid", () => new Theme
                            {
                                Colors = new List<Color>
                                {
                                    new Color32(255, 128, 0, 128),
                                    new Color32(255, 102, 0, 128),
                                    new Color32(255, 128, 0, 128)
                                },
                                Disabled = new Color32(226, 74, 44, 255),
                                Enabled = new Color32(255, 102, 0, 128)
                            }
                        },
                        {
                            "Shiba Gold", () => new Theme
                            {
                                Colors = new List<Color>
                                {
                                    Color.black,
                                    Color.yellow,
                                    Color.black
                                },
                                Enabled = Color.magenta,
                                Disabled = Color.yellow,
                                ButtonColor = Color.black
                            }
                        },
                        {
                            "Seralyth", () => new Theme
                            {
                                Colors = new List<Color> { new Color32(22, 22, 22, 128) },
                                Disabled = new Color32(118, 6, 252, 128),
                                Enabled = new Color32(98, 0, 232, 128)
                            }
                        },
                        {
                            "Mango's Mods", () => new Theme
                            {
                                Colors = new List<Color> { Color.black },
                                Disabled = Color.blue,
                                Enabled = Color.magenta
                            }
                        },
                        {
                            "Aspect Menu", () => new Theme
                            {
                                Colors = new List<Color> { Color.black },
                                Disabled = Color.black,
                                Enabled = Color.grey,
                                TitleColor = Color.red
                            }
                        }
                    }
                },
                new Button { Category = Categories.Settings, Title = "Shuffle Font Type", ShuffleValue = 5, ShuffleAction = value => Settings.ShuffleFontType(value),
                    ShuffleValues = new Dictionary<string, Func<object>>
                    {
                        { "Normal", () => FontStyles.Normal },
                        { "Bold", () => FontStyles.Bold },
                        { "Italic", () => FontStyles.Italic },
                        { "Underline", () => FontStyles.Underline },
                        { "Lower Case", () => FontStyles.LowerCase },
                        { "Upper Case", () => FontStyles.UpperCase },
                        { "Small Caps", () => FontStyles.SmallCaps },
                        { "Strikethrough", () => FontStyles.Strikethrough },
                    }
                },
                new Button { Category = Categories.Settings, Title = "Shuffle Page Type", ShuffleAction = value => Settings.ShufflePageType(value),
                    ShuffleValues = new Dictionary<string, Func<object>>
                    {
                        { "Sides", () => 0 },
                        { "Top", () => 1 },
                        { "Triggers", () => 2 },
                    }
                },
                new Button { Category = Categories.Settings, Title = "Shuffle Safety Action", ShuffleAction = value => Settings.ShuffleSafetyType(value),
                    ShuffleValues = new Dictionary<string, Func<object>>
                    {
                        { "Disconnect", () => 0 },
                        { "Just Notify", () => 1 },
                        { "Reconnect", () => 2 },
                        { "Join Random", () => 3 },
                        { "Disconnect & Change Identity", () => 4 },
                    }
                },

                new Button { Category = Categories.Settings, Title = "Left Hand Menu", OneShot = false, EnableMethod =() => Main.LeftHandMenu = true, DisableMethod =() => Main.LeftHandMenu = false },
                new Button { Category = Categories.Settings, Title = "Thick Menu", OneShot = false, EnableMethod =() => Main.MenuWidth = 0.4f, DisableMethod =() => Main.MenuWidth = 0.25f },
                new Button { Category = Categories.Settings, Title = "Thin Menu", OneShot = false, EnableMethod =() => Main.MenuWidth = 0.15f, DisableMethod =() => Main.MenuWidth = 0.25f },
                new Button { Category = Categories.Settings, Title = "Lerp Menu", OneShot = false, EnableMethod =() => Main.SmoothMode = true, DisableMethod =() => Main.SmoothMode = false },
                new Button { Category = Categories.Settings, Title = "Rainbow Menu", OneShot = false, EnableMethod =() => GradientManager.Rainbow = true, DisableMethod =() => GradientManager.Rainbow =  false },
                new Button { Category = Categories.Settings, Title = "Bread Menu", OneShot = false, EnableMethod =() => Main.BreadMode = true, DisableMethod =() => Main.BreadMode =  false },
                new Button { Category = Categories.Settings, Title = "Crystal Menu", OneShot = false, EnableMethod =() => Main.CrystalMenu = true, DisableMethod =() => Main.CrystalMenu =  false },
                new Button { Category = Categories.Settings, Title = "Disable Disconnect Button", OneShot = false, EnableMethod =() => Main.DisableDisconnectButton = true, DisableMethod =() => Main.DisableDisconnectButton =  false },

                new Button { Category = Categories.Settings, Title = "Hand Notifications", OneShot = true, Method =() => Notifications.Position = Notifications.NotificationPosition.Hand },
                new Button { Category = Categories.Settings, Title = "Head Notifications", OneShot = true, Method =() => Notifications.Position = Notifications.NotificationPosition.Head },

                new Button { Category = Categories.Settings, Title = "Animated Title", Enabled = true, OneShot = false, EnableMethod =() => Main.AnimatedTitle = true, DisableMethod =() => Main.AnimatedTitle = false },
                new Button { Category = Categories.Settings, Title = "Redact Text", OneShot = false, EnableMethod =() => Main.RedactText = true, DisableMethod =() => Main.RedactText = false }, // who is using this lol

                new Button { Category = Categories.Settings, Title = "FPS Footer", OneShot = false, EnableMethod =() => Main.FPSFooter = true, DisableMethod =() => Main.FPSFooter = false },
                new Button { Category = Categories.Settings, Title = "Time Footer", OneShot = false, EnableMethod =() => Main.TimeFooter = true, DisableMethod =() => Main.TimeFooter = false },

                new Button { Category = Categories.Settings, Title = "Disable Notifications", OneShot = false, EnableMethod =() => Notifications.Enabled = false, DisableMethod =() => Notifications.Enabled = true },
                new Button { Category = Categories.Settings, Title = "Custom Title", OneShot = false, EnableMethod =() => Main.UseCustomTitle = true, DisableMethod =() => Main.UseCustomTitle = false },
                new Button { Category = Categories.Settings, Title = "Disable Custom MOTD", OneShot = false, EnableMethod =() => MOTDPatch.StockMOTD(), DisableMethod =() => MOTDPatch.CustomMOTD() },
                new Button { Category = Categories.Settings, Title = "Auto Download Emotes", Enabled = true, OneShot = false },
            #endregion
            #region Admin
                new Button { Category = Categories.Admin, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },

                new Button { Category = Categories.Admin, Title = "Admin Kick Gun", OneShot = false, Method =() => Admin.KickGun() },
                new Button { Category = Categories.Admin, Title = "Admin Crash Gun", OneShot = false, Method =() => Admin.CrashGun() },
                new Button { Category = Categories.Admin, Title = "Admin Teleport All Gun", OneShot = false, Method =() => Admin.TeleportGun() },

                new Button { Category = Categories.Admin, Title = "Admin Spawn Plane", OneShot = true, Method =() => Admin.SpawnPlane() },

                new Button { Category = Categories.Admin, Title = "Admin Destroy All", OneShot = true, Method =() => Admin.DestroyAll() },
                new Button { Category = Categories.Admin, Title = "Admin Jail Gun", OneShot = false, Method =() => Admin.PrefabGun(true, "jail") },
                new Button { Category = Categories.Admin, Title = "Admin Bryson Gun", OneShot = false, Method =() => Admin.PrefabGun(false, "bryson") },
                new Button { Category = Categories.Admin, Title = "Admin Dingus Gun", OneShot = false, Method =() => Admin.PrefabGun(false, "dingus") },
                new Button { Category = Categories.Admin, Title = "Admin Poop Gun", OneShot = false, Method =() => Admin.PrefabGun(false, "poop") },
                new Button { Category = Categories.Admin, Title = "Admin Dr. Ryuta Kawashima Gun", OneShot = false, Method =() => Admin.PrefabGun(false, "brainage") },

                new Button { Category = Categories.Admin, Title = "Admin Minecraft Bed", OneShot = false, Method =() => Admin.MinecraftGun("Bed") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Bedrock", OneShot = false, Method =() => Admin.MinecraftGun("Bedrock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Brick", OneShot = false, Method =() => Admin.MinecraftGun("Brick") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Coal Block", OneShot = false, Method =() => Admin.MinecraftGun("CoalBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Coal Ore", OneShot = false, Method =() => Admin.MinecraftGun("CoalOre") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Cobblestone", OneShot = false, Method =() => Admin.MinecraftGun("Cobblestone") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Command Block", OneShot = false, Method =() => Admin.MinecraftGun("CommandBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Crafting Table", OneShot = false, Method =() => Admin.MinecraftGun("CraftingTable") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Diamond Block", OneShot = false, Method =() => Admin.MinecraftGun("DiamondBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Diamond Ore", OneShot = false, Method =() => Admin.MinecraftGun("DiamondOre") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Dirt", OneShot = false, Method =() => Admin.MinecraftGun("Dirt") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Emerald Block", OneShot = false, Method =() => Admin.MinecraftGun("EmeraldBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Emerald Ore", OneShot = false, Method =() => Admin.MinecraftGun("EmeraldOre") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Glass", OneShot = false, Method =() => Admin.MinecraftGun("Glass") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Gold Block", OneShot = false, Method =() => Admin.MinecraftGun("GoldBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Gold Ore", OneShot = false, Method =() => Admin.MinecraftGun("GoldOre") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Grass", OneShot = false, Method =() => Admin.MinecraftGun("Grass") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Iron Block", OneShot = false, Method =() => Admin.MinecraftGun("IronBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Iron Ore", OneShot = false, Method =() => Admin.MinecraftGun("IronOre") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Leaves", OneShot = false, Method =() => Admin.MinecraftGun("Leaves") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Log", OneShot = false, Method =() => Admin.MinecraftGun("Log") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Netherrack", OneShot = false, Method =() => Admin.MinecraftGun("Netherrack") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Obsidian", OneShot = false, Method =() => Admin.MinecraftGun("Obsidian") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Planks", OneShot = false, Method =() => Admin.MinecraftGun("Planks") },
            #endregion
            #region Movement
                new Button { Category = Categories.Movement, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Movement, Title = "Fly", OneShot = false, PrefixMethod =() => Movement.Fly() },
                new Button { Category = Categories.Movement, Title = "Teleport Gun", OneShot = false, Method =() => Movement.TeleportGun() },
                new Button { Category = Categories.Movement, Title = "Platforms", OneShot = false, Method =() => Movement.Platforms() },
                new Button { Category = Categories.Movement, Title = "Noclip", OneShot = false, Method =() => Movement.Noclip() },
                //new Button { Category = Categories.Movement, Title = "Longarms", OneShot = false, Method =() => Movement.Longarms() },
                //new Button { Category = Categories.Movement, Title = "Multiplied Longarms", OneShot = false, Method =() => Movement.MultipiedLongarms() },
                //new Button { Category = Categories.Movement, Title = "Predictions", OneShot = false, Method =() => Movement.Predictions() },
                new Button { Category = Categories.Movement, Title = "Sticky Platforms", OneShot = false, Method =() => Movement.StickyPlatforms() },
                new Button { Category = Categories.Movement, Title = "Rig Gun", OneShot = false, Method =() => Movement.RigGun() },
                new Button { Category = Categories.Movement, Title = "Keyboard Fly", OneShot = false, Method =() => Movement.KeyboardFly() },
                new Button { Category = Categories.Movement, Title = "Teleport to Map", OneShot = true, Method =() => LoadCategory(Categories.Maps) },
                new Button { Category = Categories.Movement, Title = "No Tag Freeze", OneShot = false, Method =() => GTPlayer.Instance.disableMovement = false },
                new Button { Category = Categories.Movement, Title = "Force Tag Freeze", OneShot = false, Method =() => GTPlayer.Instance.disableMovement = true, DisableMethod =() => GTPlayer.Instance.disableMovement = false },
                new Button { Category = Categories.Movement, Title = "Networked Platforms Gun", OneShot = false, Method =() => Movement.NetworkedPlatformGun() },
                new Button { Category = Categories.Movement, Title = "Networked Platforms", OneShot = false, EnableMethod =() => Movement.NetworkedPlatforms = true, DisableMethod =() => Movement.NetworkedPlatforms = false },
                #region Maps
                    new Button { Category = Categories.Maps, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Movement) },
                #endregion
            #endregion
            #region Room
                new Button { Category = Categories.Room, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Room, Title = "Disconnect", OneShot = true, Method =() => Room.Disconnect() },
                new Button { Category = Categories.Room, Title = "Reconnect", OneShot = true, Method =() => Room.Reconnect() },
                new Button { Category = Categories.Room, Title = "Join Random", OneShot = true, Method =() => Room.JoinRandom() },
                new Button { Category = Categories.Room, Title = "Disable Network Triggers", OneShot = false, EnableMethod =() => NetworkTriggerPatch.Disable = true, DisableMethod =() => NetworkTriggerPatch.Disable = false },
            #endregion
            #region Emotes
                new Button { Category = Categories.Emotes, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Emotes, Title = "Download Emotes", OneShot = true, Method =() => Emotes.Download() },
            #endregion
            #region Fun
                new Button { Category = Categories.Fun, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Fun, Title = "Ghost Monke", OneShot = false, Method =() => Fun.GhostMonke() },
                new Button { Category = Categories.Fun, Title = "Copy Player Gun", OneShot = false, Method =() => Fun.CopyPlayer() },
                new Button { Category = Categories.Fun, Title = "Fake Lag", OneShot = false, Method =() => Fun.FakeLag(), DisableMethod =() => VRRig.LocalRig.enabled = true },
                new Button { Category = Categories.Fun, Title = "Tracers", OneShot = false, Method =() => Fun.Tracers() },
                new Button { Category = Categories.Fun, Title = "Punch", OneShot = false, Method =() => Fun.Punch() },
                //new Button { Category = Categories.Fun, Title = "Force April Fools", OneShot = false, EnableMethod =() => Fun.ForceAprilFools(), DisableMethod =() => Fun.CleanupAprilFools() },
                new Button { Category = Categories.Fun, Title = "No Finger Movement", OneShot = false, Method =() => Fun.NoFingerMovement() },

                new Button { Category = Categories.Fun, Title = "Fix Head", OneShot = true, Method =() => Fun.FixHead() },
                new Button { Category = Categories.Fun, Title = "Head Spin [<color=grey>X</color>]", OneShot = false, Method =() => Fun.SpinHead("x"), DisableMethod =() => Fun.FixHead() },
                new Button { Category = Categories.Fun, Title = "Head Spin [<color=grey>Y</color>]", OneShot = false, Method =() => Fun.SpinHead("y"), DisableMethod =() => Fun.FixHead() },
                new Button { Category = Categories.Fun, Title = "Head Spin [<color=grey>Z</color>]", OneShot = false, Method =() => Fun.SpinHead("z"), DisableMethod =() => Fun.FixHead() },
                new Button { Category = Categories.Fun, Title = "Spaz Head", OneShot = false, Method =() => Fun.SpazHead(), DisableMethod =() => Fun.FixHead() },

                new Button { Category = Categories.Fun, Title = "Sound Spam [<color=grey>Random</color>]", OneShot = false, Method =() => Fun.RandomSpam() },
                new Button { Category = Categories.Fun, Title = "Sound Spam [<color=grey>Metal</color>]", OneShot = false, Method =() => Fun.SoundSpam(18) },
                new Button { Category = Categories.Fun, Title = "Sound Spam [<color=grey>Rock</color>]", OneShot = false, Method =() => Fun.SoundSpam(0) },
                new Button { Category = Categories.Fun, Title = "Sound Spam [<color=grey>Grass</color>]", OneShot = false, Method =() => Fun.SoundSpam(7) },
                new Button { Category = Categories.Fun, Title = "Sound Spam [<color=grey>Jman</color>]", OneShot = false, Method =() => Fun.SoundSpam(Random.Range(336, 338)) },
                new Button { Category = Categories.Fun, Title = "Literally Every Sound Spam", OneShot = true, Method =() => LoadCategory(Categories.EverySound) },
                #region Every Sound
                    new Button { Category = Categories.EverySound, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Fun) },
                #endregion

            new Button { Category = Categories.Fun, Title = "Slap", OneShot = false, Method =() => Fun.Slap() },
            #endregion
            #region Safety
                new Button { Category = Categories.Safety, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Safety, Title = "Log Events", OneShot = false, EnableMethod =() => Main.EventLogs = true, DisableMethod =() => Main.EventLogs = false },
                new Button { Category = Categories.Safety, Title = "Anti AFK Kick", Enabled = true, OneShot = false, EnableMethod =() => PhotonNetworkController.Instance.disableAFKKick = true, DisableMethod =() => PhotonNetworkController.Instance.disableAFKKick = false },
                new Button { Category = Categories.Safety, Title = "Disable Quit Box", Enabled = true, OneShot = false, EnableMethod =() => QuitBoxPatch.DisableQuitBox = true, DisableMethod =() => QuitBoxPatch.DisableQuitBox = false },

                new Button { Category = Categories.Safety, Title = "Flush RPCs", OneShot = true, Method =() => Safety.FlushRPCs() },

                new Button { Category = Categories.Safety, Title = "Anti Report Oculus Menu", Note = "Expereimental", Enabled = false, OneShot = false, EnableMethod =() => HandTapPatch.OculusAntiReport = true, DisableMethod =() => HandTapPatch.OculusAntiReport = false },
                new Button { Category = Categories.Safety, Title = "Anti Report", Enabled = true, OneShot = false, Method =() => Safety.AntiReport() },
                new Button { Category = Categories.Safety, Title = "Anti Mute", Enabled = false, OneShot = false, Method =() => Safety.AntiMute() },

                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Admin Badge</color>]", Enabled = true, OneShot = false, Method =() => Safety.AntiCosmetic("LBAAD.", "Administrator Badge") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Dev Stick</color>]", Enabled = true, OneShot = false, Method =() => Safety.AntiCosmetic("LBAAK.", "Developer Stick") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Forest Guide</color>]", Enabled = false, OneShot = false, Method =() => Safety.AntiCosmetic("LMAPY.", "Forest Guide Stick") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Illustrator</color>]", Enabled = false, OneShot = false, Method =() => Safety.AntiCosmetic("LBAGS.", "Illustrator Badge") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Finger Painter</color>]", Enabled = false, OneShot = false, Method =() => Safety.AntiCosmetic("LBADE.", "Finger Painter Badge") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>AA Creator</color>]", Enabled = false, OneShot = false, Method =() => Safety.AntiCosmetic("LBANI.", "AA Creator Badge") },

                new Button { Category = Categories.Safety, Title = "Random Identity", Enabled = false, OneShot = true, Method =() => Safety.RandomIdentity() },
            #endregion
            #region General
                new Button { Category = Categories.General, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.General, Title = "Normalize Names", OneShot = false, EnableMethod =() => NormalizeNamePatch.Enabled = false, DisableMethod =() => NormalizeNamePatch.Enabled = true },
                new Button { Category = Categories.General, Title = "View Other's Unowned Cosmetics", OneShot = false, EnableMethod =() => CosmeticPatches.ViewOthersCosmetics = true, DisableMethod =() => CosmeticPatches.ViewOthersCosmetics = false },
                new Button { Category = Categories.General, Title = "Unlock All Cosmetics", OneShot = true, Method =() => General.UnlockAllCosmetics() },
                new Button { Category = Categories.General, Title = "Copy ID Gun", OneShot = false, Method =() => General.GetPlayerIDGun() },
                new Button { Category = Categories.General, Title = "Unlock VIM", OneShot = false, EnableMethod =() => SubscriptionPatch.Enabled = true, DisableMethod =() => SubscriptionPatch.Enabled = false },
                new Button { Category = Categories.General, Title = "Unlock Competetive", OneShot = false, Method =() => General.UnlockCompetetive() },
            #endregion
            #region Overpowered
                new Button { Category = Categories.Overpowered, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Overpowered, Title = "Tag Gun", OneShot = false, Method =() => Overpowered.TagGun() },
                new Button { Category = Categories.Overpowered, Title = "Tag All", OneShot = false, Method =() => Overpowered.TagAll(), DisableMethod =() => VRRig.LocalRig.enabled = true },
            #endregion
            #region Credits
                new Button { Category = Categories.Credits, Title = "Exit", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Credits, Title = "Instel", OneShot = true, Method =() => Notifications.SendNotification("[<color=green>Instel12</color>] Menu's lead developer.") },
                new Button { Category = Categories.Credits, Title = "Kyle The Scientist", OneShot = true, Method =() => Notifications.SendNotification("[<color=green>KyleTheScientist</color>] Referance for loading asset bundles.") },
                new Button { Category = Categories.Credits, Title = "Poison", OneShot = true, Method =() => Notifications.SendNotification("[<color=green>heycanihavethis</color>] Guidance on how Gorilla Tag works and mod methods.") },
                new Button { Category = Categories.Credits, Title = "iiDk", OneShot = true, Method =() => Notifications.SendNotification("[<color=green>iiDk-the-actual</color>] Asset bundle for Fortnite emotes.") },
                new Button { Category = Categories.Credits, Title = "Ollib0t_", OneShot = true, Method =() => Notifications.SendNotification("[<color=green>iiDk-the-actual</color>] Very smart graphic designer.") },
                new Button { Category = Categories.Credits, Title = "G4L4X7", OneShot = true, Method =() => Notifications.SendNotification("[<color=green>G4L4X7M0DZ</color>] Was actually the only one to answer a question i had about the anti cheat.") },
            #endregion
        };

        public static int Page;
        public static Categories CurrentCategory = Categories.Main;
        public static void LoadCategory(Categories category)
        {
            CurrentCategory = category;
            Page = 0;
            Main.RefreshMods();
        }

        public static int GetIndex(string name)
        {
            for (int i = 0; i < Buttans.Count; i++)
            {
                if (Buttans[i].Title == name) return i;
            }
            return -1;
        }

        public static void NextPage()
        {
            int maxPage = Mathf.Max(0, (Main.ThisCategory.Count - 1) / 9);

            if (Page < maxPage)
            {
                Page++;
            }
            else
            {
                Page = 0;
            }
        }

        public static void PreviousPage()
        {
            if (Page > 0)
            {
                Page--;
            }
            else
            {
                Page = Mathf.Max(0, (Main.ThisCategory.Count - 1) / 9);
            }
        }
    }
}
