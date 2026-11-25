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
                new Button { Category = Categories.Main, Title = "Settings", Description = "Enters Settings.", OneShot = true, Method =() => LoadCategory(Categories.Settings) },
                new Button { Category = Categories.Main, Title = "<color=purple>Join Discord</color>", Description = "Join our tuff community.", OneShot = true, Method =() => General.JoinDiscord() },
                new Button { Category = Categories.Main, Title = "Admin", Description = "Enters Admin Mods.", OneShot = true, Method =() => Admin.EnterAdmin() },
                new Button { Category = Categories.Main, Title = "Enabled Mods", Description = "Enters Enabled Mods.", OneShot = true, Method =() => LoadCategory(Categories.Enabled) },
                new Button { Category = Categories.Main, Title = "Favorite Mods", Description = "Enters Favorite Mods.", OneShot = true, Method =() => LoadCategory(Categories.Favorite) },
                new Button { Category = Categories.Main, Title = "Movement", Description = "Enters Movement Mods", OneShot = true, Method =() => LoadCategory(Categories.Movement) },
                new Button { Category = Categories.Main, Title = "Room", Description = "Enters Room Mods.", OneShot = true, Method =() => LoadCategory(Categories.Room) },
                new Button { Category = Categories.Main, Title = "Fun", Description = "Enters Fun Mods.", OneShot = true, Method =() => LoadCategory(Categories.Fun) },
                new Button { Category = Categories.Main, Title = "Soundboard", Description = "Enters the Soundboard.", OneShot = true, Method =() => LoadCategory(Categories.Soundboard) },
                new Button { Category = Categories.Main, Title = "Emotes", Description = "Enters Emotes.", OneShot = true, Method =() => LoadCategory(Categories.Emotes) },
                new Button { Category = Categories.Main, Title = "Safety", Description = "Enters Safety Mods.", OneShot = true, Method =() => LoadCategory(Categories.Safety) },
                new Button { Category = Categories.Main, Title = "General", Description = "Enters General Mods.", OneShot = true, Method =() => LoadCategory(Categories.General) },
                new Button { Category = Categories.Main, Title = "Overpowered", Description = "Enters Overpowered Mods.", OneShot = true, Method =() => LoadCategory(Categories.Overpowered) },
                new Button { Category = Categories.Main, Title = "Credits", Description = "Shows the credits.", OneShot = true, Method =() => LoadCategory(Categories.Credits) },
            #endregion
            #region Enabled (this needs to be first always cause the menu is stupid)
                new Button { Category = Categories.Enabled, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },
            #endregion
            #region Favorite (this also needs to be first always cause the menu its also stupid)
                new Button { Category = Categories.Favorite, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },
            #endregion
            #region Settings
                new Button { Category = Categories.Settings, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },

                new Button { Category = Categories.Settings, Title = "Shuffle Font", Description = "Changes the font.", ShuffleAction = value => Settings.ShuffleFont(value),
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
                new Button { Category = Categories.Settings, Title = "Shuffle Theme", Description = "Changes the theme.", ShuffleAction = value => Settings.ShuffleTheme(value), ShuffleValues = Settings.Themes },
                new Button { Category = Categories.Settings, Title = "Shuffle Font Type", Description = "Changes the style of the font.", ShuffleValue = 5, ShuffleAction = value => Settings.ShuffleFontType(value),
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
                new Button { Category = Categories.Settings, Title = "Shuffle Page Type", Description = "Changes how you change pages.", ShuffleAction = value => Settings.ShufflePageType(value),
                    ShuffleValues = new Dictionary<string, Func<object>>
                    {
                        { "Sides", () => 0 },
                        { "Top", () => 1 },
                        { "Triggers", () => 2 },
                    }
                },
                new Button { Category = Categories.Settings, Title = "Shuffle Safety Action", Description = "Changes what happens when a safety feature is triggered.", ShuffleAction = value => Settings.ShuffleSafetyType(value),
                    ShuffleValues = new Dictionary<string, Func<object>>
                    {
                        { "Disconnect", () => 0 },
                        { "Just Notify", () => 1 },
                        { "Reconnect", () => 2 },
                        { "Join Random", () => 3 },
                        { "Disconnect & Change Identity", () => 4 },
                    }
                },
                new Button { Category = Categories.Settings, Title = "Shuffle Menu Scale", Description = "Changes the size of the menu.", ShuffleAction = value => Settings.ShuffleMenuSize(value),
                    ShuffleValues = new Dictionary<string, Func<object>>
                    {
                        { "Small", () => 0.75f },
                        { "Medium", () => 1f },
                        { "Big", () => 1.25f },
                        { "Massive", () => 3f },
                        { "Microscopic", () => 0.25f },
                    }
                },
                new Button { Category = Categories.Settings, Title = "Shuffle Pull Strength", Description = "Changes how much of a boost Pull Mod gives you.", ShuffleValue = 1, ShuffleAction = value => Settings.ShufflePullStrength(value),
                    ShuffleValues = new Dictionary<string, Func<object>>
                    {
                        { "Small", () => 0.1f },
                        { "Medium", () => 0.3f },
                        { "High", () => 0.5f },
                        { "Insane", () => 1f },
                    }
                },

                new Button { Category = Categories.Settings, Title = "Left Hand Menu", Description = "Puts the menu in your left hand.", OneShot = false, EnableMethod =() => Main.LeftHandMenu = true, DisableMethod =() => Main.LeftHandMenu = false },
                new Button { Category = Categories.Settings, Title = "Thick Menu", Description = "Makes the menu wider.", OneShot = false, EnableMethod =() => Main.MenuWidth = 0.4f, DisableMethod =() => Main.MenuWidth = 0.25f },
                new Button { Category = Categories.Settings, Title = "Thin Menu", Description = "Makes the menu thin.", OneShot = false, EnableMethod =() => Main.MenuWidth = 0.15f, DisableMethod =() => Main.MenuWidth = 0.25f },
                new Button { Category = Categories.Settings, Title = "Lerp Menu", Description = "Makes the menu's position smooth.", OneShot = false, EnableMethod =() => Main.SmoothMode = true, DisableMethod =() => Main.SmoothMode = false },
                new Button { Category = Categories.Settings, Title = "Crystal Menu", OneShot = false, Description = "Makes all menu materials like a crystal from the Caves map.", EnableMethod =() => Main.CrystalMenu = true, DisableMethod =() => Main.CrystalMenu =  false },
                new Button { Category = Categories.Settings, Title = "Disable Disconnect Button", Description = "Removes the disconnect button.", OneShot = false, EnableMethod =() => Main.DisableDisconnectButton = true, DisableMethod =() => Main.DisableDisconnectButton =  false },

                new Button { Category = Categories.Settings, Title = "Hand Notifications", Description = "Shows your notifications in your hand.", OneShot = true, Method =() => Notifications.Position = Notifications.NotificationPosition.Hand },
                new Button { Category = Categories.Settings, Title = "Head Notifications", Description = "Shows your notifications in front of your face.", OneShot = true, Method =() => Notifications.Position = Notifications.NotificationPosition.Head },

                new Button { Category = Categories.Settings, Title = "Animated Title", Description = "Gives the title a ping-pong effect.", Enabled = true, OneShot = false, EnableMethod =() => Main.AnimatedTitle = true, DisableMethod =() => Main.AnimatedTitle = false },
                new Button { Category = Categories.Settings, Title = "Redact Text", Description = "Makes the text unreadable. (fsr)", OneShot = false, EnableMethod =() => Main.RedactText = true, DisableMethod =() => Main.RedactText = false }, // who is using this lol

                new Button { Category = Categories.Settings, Title = "FPS Footer", Description = "Adds how many frames per second your game is running at under the title.", OneShot = false, EnableMethod =() => Main.FPSFooter = true, DisableMethod =() => Main.FPSFooter = false },
                new Button { Category = Categories.Settings, Title = "Time Footer", Description = "Adds your device's time under the title.", OneShot = false, EnableMethod =() => Main.TimeFooter = true, DisableMethod =() => Main.TimeFooter = false },
                new Button { Category = Categories.Settings, Title = "Page Footer", Description = "Adds the page number under the title.", OneShot = false, EnableMethod =() => Main.PageFooter = true, DisableMethod =() => Main.PageFooter = false },

                new Button { Category = Categories.Settings, Title = "Universal Home Button", Description = "Adds a button to go home wherever you are.", OneShot = false },
                new Button { Category = Categories.Settings, Title = "Disable Notifications", Description = "Makes you not recieve notifications.", OneShot = false, EnableMethod =() => Notifications.Enabled = false, DisableMethod =() => Notifications.Enabled = true },
                new Button { Category = Categories.Settings, Title = "Streamer Notifications", Description = "Makes your notifications only show on the VR view.", OneShot = false },

                new Button { Category = Categories.Settings, Title = "Custom Title", Description = "Makes the title the content of Gorilla Tag/Masa/CustomTitle.txt.", OneShot = false, EnableMethod =() => Main.UseCustomTitle = true, DisableMethod =() => Main.UseCustomTitle = false },
                new Button { Category = Categories.Settings, Title = "Disable Custom MOTD", Description = "Returns the stock MOTD.", OneShot = false, EnableMethod =() => MOTDPatch.StockMOTD(), DisableMethod =() => MOTDPatch.CustomMOTD() },
                new Button { Category = Categories.Settings, Title = "Auto Download Emotes", Description = "Downloads the emotes asset bundle when the menu initizes.", Enabled = true, OneShot = false },
            #endregion
            #region Admin
                new Button { Category = Categories.Admin, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },

                new Button { Category = Categories.Admin, Title = "Admin Kick Gun", Description = "Kicks the player that you desire.", OneShot = false, Method =() => Admin.KickGun() },
                new Button { Category = Categories.Admin, Title = "Admin Crash Gun", Description = "Crashes the game of the player that you desire.", OneShot = false, Method =() => Admin.CrashGun() },
                new Button { Category = Categories.Admin, Title = "Admin Teleport All Gun", Description = "Teleports all players to where you want.", OneShot = false, Method =() => Admin.TeleportGun() },

                new Button { Category = Categories.Admin, Title = "Admin Show Role", Description = "Shows all menu users your admin role under your name.", OneShot = true, Method =() => Admin.RevealAdmin() },
                new Button { Category = Categories.Admin, Title = "Admin Hide Role", Description = "Hides from all menu users your admin role under your name.", OneShot = true, Method =() => Admin.HideAdmin() },
                new Button { Category = Categories.Admin, Title = "Admin Show Menu Users", Description = "Shows under everyone's name if they are using Masa.", OneShot = true, Method =() => Admin.ShowUsers() },

                //new Button { Category = Categories.Admin, Title = "Admin Spawn Plane", Description = "Teleports you to a big ahh plane.", OneShot = true, Method =() => Admin.SpawnPlane() },

                new Button { Category = Categories.Admin, Title = "Admin Destroy All", Description = "Destroys everything an admin had made.", OneShot = true, Method =() => Admin.DestroyAll() },
                new Button { Category = Categories.Admin, Title = "Admin Jail Gun", Description = "Puts the player in jail.", OneShot = false, Method =() => Admin.PrefabGun(true, "jail") },
                new Button { Category = Categories.Admin, Title = "Admin Bryson Gun", Description = "Starts an insane concert.", OneShot = false, Method =() => Admin.PrefabGun(false, "bryson") },
                new Button { Category = Categories.Admin, Title = "Admin Dingus Gun", Description = "Spawns the cat.", OneShot = false, Method =() => Admin.PrefabGun(false, "dingus") },
                new Button { Category = Categories.Admin, Title = "Admin Poop Gun", Description = "How mature?", OneShot = false, Method =() => Admin.PrefabGun(false, "poop") },
                new Button { Category = Categories.Admin, Title = "Admin Dr. Ryuta Kawashima Gun", Description = "Spawns the guy from Brain Age.", OneShot = false, Method =() => Admin.PrefabGun(false, "brainage") },

                new Button { Category = Categories.Admin, Title = "Admin Minecraft Bed", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Bed") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Bedrock", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Bedrock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Brick", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Brick") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Coal Block", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("CoalBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Coal Ore", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("CoalOre") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Cobblestone", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Cobblestone") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Command Block", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("CommandBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Crafting Table", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("CraftingTable") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Diamond Block", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("DiamondBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Diamond Ore", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("DiamondOre") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Dirt", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Dirt") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Emerald Block", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("EmeraldBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Emerald Ore", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("EmeraldOre") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Glass", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Glass") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Gold Block", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("GoldBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Gold Ore", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("GoldOre") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Grass", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Grass") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Iron Block", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("IronBlock") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Iron Ore", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("IronOre") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Leaves", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Leaves") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Log", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Log") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Netherrack", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Netherrack") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Obsidian", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Obsidian") },
                new Button { Category = Categories.Admin, Title = "Admin Minecraft Planks", Description = "Play Minecraft.", OneShot = false, Method =() => Admin.MinecraftGun("Planks") },
            #endregion
            #region Movement
                new Button { Category = Categories.Movement, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Movement, Title = "Fly", Description = "Allows you to fly.", OneShot = false, PrefixMethod =() => Movement.Fly() },
                new Button { Category = Categories.Movement, Title = "Teleport Gun", Description = "Teleport to where you desire.", OneShot = false, Method =() => Movement.TeleportGun() },
                new Button { Category = Categories.Movement, Title = "Sticky Platforms", Description = "Platforms but your hands stick to them.", OneShot = false, Method =() => Movement.StickyPlatforms() },
                new Button { Category = Categories.Movement, Title = "Platforms", OneShot = false, Description = "Spawns a platform out of thin air.", Method =() => Movement.Platforms() },
                new Button { Category = Categories.Movement, Title = "Frozone", OneShot = false, Description = "Makes you the guy from that one Disney movie.", Method =() => Movement.Frozone() },
                new Button { Category = Categories.Movement, Title = "Noclip", Description = "Disables collision.", OneShot = false, Method =() => Movement.Noclip() },
                //new Button { Category = Categories.Movement, Title = "Longarms", OneShot = false, Method =() => Movement.Longarms() },
                //new Button { Category = Categories.Movement, Title = "Multiplied Longarms", OneShot = false, Method =() => Movement.MultipiedLongarms() },
                //new Button { Category = Categories.Movement, Title = "Predictions", OneShot = false, Method =() => Movement.Predictions() },
                new Button { Category = Categories.Movement, Title = "Rig Gun", Description = "Puts your rig where you desire.", OneShot = false, Method =() => Movement.RigGun() },
                new Button { Category = Categories.Movement, Title = "Keyboard Fly", Description = "Allows you to move with WASD.", OneShot = false, Method =() => Movement.KeyboardFly() },
                new Button { Category = Categories.Movement, Title = "Teleport to Map", Description = "Enters a selection of maps to teleport to.", OneShot = true, Method =() => LoadCategory(Categories.Maps) },
                new Button { Category = Categories.Movement, Title = "No Tag Freeze", OneShot = false, Description = "Doesn't freeze your locomotion when you get tagged.", Method =() => GTPlayer.Instance.disableMovement = false },
                new Button { Category = Categories.Movement, Title = "Force Tag Freeze", Description = "Doesn't let you gain velocity like when you first get tagged.", OneShot = false, Method =() => GTPlayer.Instance.disableMovement = true, DisableMethod =() => GTPlayer.Instance.disableMovement = false },
                new Button { Category = Categories.Movement, Title = "Networked Platforms Gun", Description = "Spawns platforms for people with networked platforms. (Compatible with other menus.)", OneShot = false, Method =() => Movement.NetworkedPlatformGun() },
                new Button { Category = Categories.Movement, Title = "Networked Platforms", Description = "Allows you to see networked platforms. (Compatible with other menus.)", OneShot = false, EnableMethod =() => Movement.NetworkedPlatforms = true, DisableMethod =() => Movement.NetworkedPlatforms = false },
                new Button { Category = Categories.Movement, Title = "Pull Mod", Description = "Pulls your player in the direction you are facing to make you faster.", OneShot = false, Method =() => Movement.PullMod() },
                new Button { Category = Categories.Movement, Title = "Disable Wind Barriers", Description = "Wind barriers aint got shit on you!", OneShot = false, EnableMethod =() => WindPatch.WindDisabled = true, DisableMethod =() => WindPatch.WindDisabled = false },
                new Button { Category = Categories.Movement, Title = "No Slip", Description = "Makes no surfaces slippery.", OneShot = false, EnableMethod =() => SlidePatch.NoSlip = true, DisableMethod =() => SlidePatch.NoSlip = false },
                new Button { Category = Categories.Movement, Title = "All Slip", Description = "Makes all surfaces slippery.", OneShot = false, EnableMethod =() => SlidePatch.AllBlueWalls = true, DisableMethod =() => SlidePatch.AllBlueWalls = false },
                new Button { Category = Categories.Movement, Title = "All Ice", Description = "Makes all surfaces fully slippery.", OneShot = false, EnableMethod =() => SlidePatch.AllRedWalls = true, DisableMethod =() => SlidePatch.AllRedWalls = false },
                new Button { Category = Categories.Movement, Title = "Low Gravity", Description = "Makes the gravity less strong.", OneShot = false, Method =() => Movement.LowGravity() },
                new Button { Category = Categories.Movement, Title = "High Gravity", Description = "Makes the gravity stronger.", OneShot = false, Method =() => Movement.HighGravity() },
                new Button { Category = Categories.Movement, Title = "Zero Gravity", Description = "Makes gravity not pull you down.", OneShot = false, Method =() => Movement.HighGravity() },
                #region Maps
                    new Button { Category = Categories.Maps, Title = "Exit", Description = "Returns to Movement Mods.", OneShot = true, Method =() => LoadCategory(Categories.Movement) },
                #endregion
            #endregion
            #region Room
                new Button { Category = Categories.Room, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Room, Title = "Disconnect", Description = "Leaves the current room.", OneShot = true, Method =() => Room.Disconnect() },
                new Button { Category = Categories.Room, Title = "Reconnect", Description = "Rejoins or joins the last room.", OneShot = true, Method =() => Room.Reconnect() },
                new Button { Category = Categories.Room, Title = "Join Random", Description = "Joins a public room.", OneShot = true, Method =() => Room.JoinRandom() },
                new Button { Category = Categories.Room, Title = "Disable Network Triggers", Description = "Turns off the ability to join random rooms.", OneShot = false, EnableMethod =() => NetworkTriggerPatch.Disable = true, DisableMethod =() => NetworkTriggerPatch.Disable = false },
            #endregion
            #region Emotes
                new Button { Category = Categories.Emotes, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Emotes, Title = "Download Emotes", Description = "Downloads the emotes bundle.", OneShot = true, Method =() => Emotes.Download() },
            #endregion
            #region Fun
                new Button { Category = Categories.Fun, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Fun, Title = "Ghost Monke", Description = "Lets you leave your body.", OneShot = false, Method =() => Fun.GhostMonke() },
                new Button { Category = Categories.Fun, Title = "Invisible Monke", Description = "Lets you go invisible.", OneShot = false, Method =() => Fun.InvisMonke() },
                new Button { Category = Categories.Fun, Title = "Copy Player Gun", Description = "Copies another player's rig.", OneShot = false, Method =() => Fun.CopyPlayer() },
                new Button { Category = Categories.Fun, Title = "Fake Lag", Description = "Makes your rig lag.", OneShot = false, Method =() => Fun.FakeLag(), DisableMethod =() => VRRig.LocalRig.enabled = true },
                new Button { Category = Categories.Fun, Title = "Casual Tracers", Description = "Puts tracers matching the player's color on everyone.", OneShot = false, Method =() => Fun.Tracers() },
                new Button { Category = Categories.Fun, Title = "Infection Tracers", Description = "Puts tracers on everyone. (Red if tagged, green if not.)", OneShot = false, Method =() => Fun.InfectionTracers() },
                new Button { Category = Categories.Fun, Title = "Punch", Description = "Allows other players to hit you.", OneShot = false, Method =() => Fun.Punch() },
                new Button { Category = Categories.Fun, Title = "No Finger Movement", Description = "Stops your fingers from moving.", OneShot = false, Method =() => Fun.NoFingerMovement() },

                new Button { Category = Categories.Fun, Title = "Fix Head", Description = "Resets your head's rotation.", OneShot = true, Method =() => Fun.FixHead() },
                new Button { Category = Categories.Fun, Title = "Head Spin [<color=grey>X</color>]", Description = "Spins your head on the X axis.", OneShot = false, PrefixMethod =() => Fun.SpinHead("x"), DisableMethod =() => Fun.FixHead() },
                new Button { Category = Categories.Fun, Title = "Head Spin [<color=grey>Y</color>]", Description = "Spins your head on the Y axis.", OneShot = false, PrefixMethod =() => Fun.SpinHead("y"), DisableMethod =() => Fun.FixHead() },
                new Button { Category = Categories.Fun, Title = "Head Spin [<color=grey>Z</color>]", Description = "Spins your head on the Z axis.", OneShot = false, PrefixMethod =() => Fun.SpinHead("z"), DisableMethod =() => Fun.FixHead() },
                new Button { Category = Categories.Fun, Title = "Spaz Head", Description = "Makes your head go insane.", OneShot = false, PrefixMethod =() => Fun.SpazHead(), DisableMethod =() => Fun.FixHead() },

                new Button { Category = Categories.Fun, Title = "Sound Spam [<color=grey>Random</color>]", Description = "Spams a hand tap.", OneShot = false, Method =() => Fun.RandomSpam() },
                new Button { Category = Categories.Fun, Title = "Sound Spam [<color=grey>Metal</color>]", Description = "Spams a hand tap.", OneShot = false, Method =() => Fun.SoundSpam(18) },
                new Button { Category = Categories.Fun, Title = "Sound Spam [<color=grey>Rock</color>]", Description = "Spams a hand tap.", OneShot = false, Method =() => Fun.SoundSpam(0) },
                new Button { Category = Categories.Fun, Title = "Sound Spam [<color=grey>Grass</color>]", Description = "Spams a hand tap.", OneShot = false, Method =() => Fun.SoundSpam(7) },
                new Button { Category = Categories.Fun, Title = "Sound Spam [<color=grey>Jman</color>]", Description = "Spams a hand tap.", OneShot = false, Method =() => Fun.SoundSpam(Random.Range(336, 338)) },
                new Button { Category = Categories.Fun, Title = "Literally Every Sound Spam", Description = "Enters a category with every hand tap.", OneShot = true, Method =() => LoadCategory(Categories.EverySound) },

                new Button { Category = Categories.Fun, Title = "Open Basement Door", Description = "Opens the basement door enterance.", OneShot = true, Method =() => Fun.OpenNaNoor(true) },
                new Button { Category = Categories.Fun, Title = "Close Basement Door", Description = "Closes the basement door enterance.", OneShot = true, Method =() => Fun.OpenNaNoor(false) },
                new Button { Category = Categories.Fun, Title = "Basement Door Spam", Description = "Quickly opens and closes the basement door enterance.", OneShot = false, Method =() => Fun.OpenNaNoorSpam() },

                new Button { Category = Categories.Fun, Title = "Water Splash Gun", Description = "Splash water wherever you desire.", OneShot = false, Method =() => Fun.SplashGun() },
                new Button { Category = Categories.Fun, Title = "Water Splash Hands", Description = "Splash water on your hands when you press your grips.", OneShot = false, Method =() => Fun.SplashHands() },
                new Button { Category = Categories.Fun, Title = "Cum Self", Description = "Splashes water on your dihh.", OneShot = false, Method =() => Fun.Cum(true) },
                new Button { Category = Categories.Fun, Title = "Cum Gun", Description = "Splashes water on your dihh.", OneShot = false, Method =() => Fun.Cum(false) },

                new Button { Category = Categories.Fun, Title = "Rainbow Player", Description = "Makes your player's color rainbow.", OneShot = false, Method =() => Fun.Rainbow() },

                new Button { Category = Categories.Fun, Title = "Grab Bug", Description = "Grab the bug from anywhere.", OneShot = false, Method =() => Fun.GrabBug() },
                new Button { Category = Categories.Fun, Title = "Grab Bat", Description = "Grab the bat from anywhere.", OneShot = false, Method =() => Fun.GrabBat() },

                new Button { Category = Categories.Fun, Title = "Bug Gun", Description = "Teleport the bug to wherever your point.", OneShot = false, Method =() => Fun.BugGun() },
                new Button { Category = Categories.Fun, Title = "Bat Gun", Description = "Teleport the bat to wherever your point.", OneShot = false, Method =() => Fun.BatGun() },

                new Button { Category = Categories.Fun, Title = "Become Bug", Description = "Lets you play as the bug.", OneShot = false, Method =() => Fun.BecomeBug(), DisableMethod =() => VRRig.LocalRig.enabled = true },
                new Button { Category = Categories.Fun, Title = "Become Bat", Description = "Lets you play as the bat.", OneShot = false, Method =() => Fun.BecomeBat(), DisableMethod =() => VRRig.LocalRig.enabled = true },

                new Button { Category = Categories.Fun, Title = "Reset Quest Score", Description = "Sets your quest score to 0.", OneShot = true, Method =() => Fun.SetQuestScore(0) },
                new Button { Category = Categories.Fun, Title = "Max Quest Score", Description = "Sets your quest score to the max integer.", OneShot = true, Method =() => Fun.SetQuestScore(int.MaxValue) },
                new Button { Category = Categories.Fun, Title = "Spaz Quest Score", Description = "Sets your quest score to random numbers.", OneShot = false, Method =() => Fun.SetQuestScore(Random.Range(0, 99999)) },

                new Button { Category = Categories.Fun, Title = "Corrupt Textures", Description = "Makes the textures break in different ways every 10 seconds.", OneShot = false, Method =() => Fun.CorruptMode() },
                #region Every Sound
                    new Button { Category = Categories.EverySound, Title = "Exit", Description = "Returns to Fun Mods.", OneShot = true, Method =() => LoadCategory(Categories.Fun) },
                #endregion

                new Button { Category = Categories.Fun, Title = "Slap", Description = "Slap other players.", OneShot = false, Method =() => Fun.Slap() },
            #endregion
            #region Safety
            new Button { Category = Categories.Safety, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Safety, Title = "Log Events", Description = "Sends a notification every time sombody sends a Photon event.", OneShot = false, EnableMethod =() => Main.EventLogs = true, DisableMethod =() => Main.EventLogs = false },
                new Button { Category = Categories.Safety, Title = "Anti AFK Kick", Description = "Doesn't kick you when you're idle.", Enabled = true, OneShot = false, EnableMethod =() => PhotonNetworkController.Instance.disableAFKKick = true, DisableMethod =() => PhotonNetworkController.Instance.disableAFKKick = false },
                new Button { Category = Categories.Safety, Title = "Disable Quit Box", Description = "Doesn't crash you when you fall out of the map.", Enabled = true, OneShot = false, EnableMethod =() => QuitBoxPatch.DisableQuitBox = true, DisableMethod =() => QuitBoxPatch.DisableQuitBox = false },

                new Button { Category = Categories.Safety, Title = "Flush RPCs", Description = "Makes the anti-cheat think you haven't sent any RPCs.", OneShot = true, Method =() => Safety.FlushRPCs() },

                new Button { Category = Categories.Safety, Title = "Anti Report Oculus Menu", Description = "Executes your safety action when somebody tries to report you via the Oculus menu.", Note = "Expereimental", Enabled = false, OneShot = false, EnableMethod =() => HandTapPatch.OculusAntiReport = true, DisableMethod =() => HandTapPatch.OculusAntiReport = false },
                new Button { Category = Categories.Safety, Title = "Anti Report", Description = "Executes your safety action when sombody tries to report you via a scoreboard.", Enabled = true, OneShot = false, Method =() => Safety.AntiReport() },
                new Button { Category = Categories.Safety, Title = "Anti Mute", Description = "Executes your safety action when sombody tries to mute you via a scoreboard.", Enabled = false, OneShot = false, Method =() => Safety.AntiMute() },

                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Admin Badge</color>]", Description = "Executes your safety action when sombody with this cosmetic is in your room.", Enabled = true, OneShot = false, Method =() => Safety.AntiCosmetic("LBAAD.", "Administrator Badge") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Dev Stick</color>]", Description = "Executes your safety action when sombody with this cosmetic is in your room.", Enabled = true, OneShot = false, Method =() => Safety.AntiCosmetic("LBAAK.", "Developer Stick") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Forest Guide</color>]", Description = "Executes your safety action when sombody with this cosmetic is in your room.", Enabled = false, OneShot = false, Method =() => Safety.AntiCosmetic("LMAPY.", "Forest Guide Stick") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Illustrator</color>]", Description = "Executes your safety action when sombody with this cosmetic is in your room.", Enabled = false, OneShot = false, Method =() => Safety.AntiCosmetic("LBAGS.", "Illustrator Badge") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Finger Painter</color>]", Description = "Executes your safety action when sombody with this cosmetic is in your room.", Enabled = false, OneShot = false, Method =() => Safety.AntiCosmetic("LBADE.", "Finger Painter Badge") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>AA Creator</color>]", Description = "Executes your safety action when sombody with this cosmetic is in your room.", Enabled = false, OneShot = false, Method =() => Safety.AntiCosmetic("LBANI.", "AA Creator Badge") },
                new Button { Category = Categories.Safety, Title = "Anti Cosmetic [<color=grey>Party Hat</color>]", Description = "Executes your safety action when sombody with this cosmetic is in your room.", Enabled = false, OneShot = false, Method =() => Safety.AntiCosmetic("LHAAC.", "Party Hat") },

                new Button { Category = Categories.Safety, Title = "Random Identity", Description = "Makes you look fresh out of tutorial.", Enabled = false, OneShot = true, Method =() => Safety.RandomIdentity() },
            #endregion
            #region General
                new Button { Category = Categories.General, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                //new Button { Category = Categories.General, Title = "Normalize Names", Description = "Makes you see names like \"BADGORILLA\" again.", OneShot = false, EnableMethod =() => NameTagPatch.Enabled = false, DisableMethod =() => NameTagPatch.Enabled = true },
                new Button { Category = Categories.General, Title = "View Other's Unowned Cosmetics", Description = "Lets you see what sombody has equipped even if they don't own it.", OneShot = false, EnableMethod =() => CosmeticPatches.ViewOthersCosmetics = true, DisableMethod =() => CosmeticPatches.ViewOthersCosmetics = false },
                new Button { Category = Categories.General, Title = "Unlock All Cosmetics", Description = "Unlocks every cosmetic on your wardrobe.", OneShot = true, Method =() => General.UnlockAllCosmetics() },
                new Button { Category = Categories.General, Title = "Lock All Cosmetics", Description = "Locks every cosmetic on your wardrobe.", OneShot = true, Method =() => General.LockAllCosmetics() },
                new Button { Category = Categories.General, Title = "Copy ID Gun", Description = "Copies a player's ID.", OneShot = false, Method =() => General.GetPlayerIDGun() },
                new Button { Category = Categories.General, Title = "Unlock VIM", Description = "Unlocks the Gorilla Tag Fan Club.", OneShot = false, EnableMethod =() => SubscriptionPatch.Enabled = true, DisableMethod =() => SubscriptionPatch.Enabled = false },
                new Button { Category = Categories.General, Title = "Unlock Competetive", Description = "Unlocks the ability to play in competetive queue.", OneShot = true, Method =() => General.UnlockCompetetive() },
                new Button { Category = Categories.General, Title = "Standalone Ranked Lobbies", Description = "Allows you to join the standalone ranked lobbies.", OneShot = false, EnableMethod =() => RankedPatch.Enabled = true, DisableMethod =() => RankedPatch.Enabled = false },
                new Button { Category = Categories.General, Title = "Player Info Tags", Description = "Puts some player info on top of everyone's head. (Looks like Untitled lol)", OneShot = false, Method =() => General.PlayerInfoTags() },
                new Button { Category = Categories.General, Title = "PC Button Click", Description = "Lets you click buttons with your mouse.", OneShot = false, Method =() => General.PCButtonClick(), DisableMethod =() => General.DisablePCButtonClick() },
                new Button { Category = Categories.General, Title = "Update Nametags", Description = "Refreshes everyone's nametag.", OneShot = true, Method =() => General.UpdateNameTags() },
            #endregion
            #region Overpowered
                new Button { Category = Categories.Overpowered, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },
                new Button { Category = Categories.Overpowered, Title = "Tag Gun", Description = "Tags whoever you desire.", OneShot = false, Method =() => Overpowered.TagGun() },
                new Button { Category = Categories.Overpowered, Title = "Tag All", Description = "Tags everyone.", OneShot = false, Method =() => Overpowered.TagAll(), DisableMethod =() => VRRig.LocalRig.enabled = true },
                new Button { Category = Categories.Overpowered, Title = "Target Spam", Description = "Makes the targets go insane!", Master = true, OneShot = false, Method =() => Overpowered.TargetSpam() },
                new Button { Category = Categories.Overpowered, Title = "Enable Grey Zone", Description = "Makes everyone's game grey.", Master = true, OneShot = false, Method =() => Overpowered.GreyZone(true) },
                new Button { Category = Categories.Overpowered, Title = "Disable Grey Zone", Description = "Makes everyone's game full color again.", Master = true, OneShot = true, EnableMethod =() => Overpowered.GreyZone(false) },
            #endregion
            #region Credits
                new Button { Category = Categories.Credits, Title = "Exit", Description = "Returns to the main menu.", OneShot = true, Method =() => LoadCategory(Categories.Main) },
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
