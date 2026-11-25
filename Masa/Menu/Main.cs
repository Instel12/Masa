using GorillaLocomotion;
using HarmonyLib;
using UnityEngine;
using TMPro;
using Masa.Classes;
using Photon.Pun;
using System.Collections.Generic;
using Masa.Mods;
using UnityEngine.InputSystem;
using GorillaNetworking;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System;
using BepInEx;
using System.IO;
using System.Linq;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.Networking;
using System.Collections;
using Masa.Patches;

namespace Masa.Menu
{
    [HarmonyPatch(typeof(GTPlayer), "LateUpdate")]
    static class Main
    {
        [System.Serializable]
        public class AdminConfig
        {
            public string CurrentVersion;
            public string DiscordLink;
            public string Status;

            public string[] Owner;
            public string[] Admins;

            public string[] SpecialCosmetics;
        }

        public static bool MenuButton;
        public static bool LeftHandMenu;

        public static GTPlayer.HandState HandState;
        public static GTPlayer.HandState OtherHandState;

        public static float MenuWidth = 0.25f;

        public static AssetBundle MainBundle;
        public static TMP_FontAsset MenuFont;
        public static AudioClip NotificationSound;
        public static Shader ChamsShader;
        public static Material BreadMaterial;
        public static Material SelectMaterial;
        public static Material StarMaterial;
        public static Shader ToonShader;

        public static Material CrystalMaterial;
        public static bool CrystalMenu;

        public static float Delay;
        public static GameObject Pointer;

        public static List<int> ThisCategory = new List<int>();
        public static int CurrentPage = 0;

        public static Texture2D WhiteSquareLol;

        public static bool AnimatedTitle = true;

        public static bool RedactText = false;
        public static FontStyles FontStyle = FontStyles.UpperCase;

        public static bool TimeFooter = false;
        public static bool FPSFooter = false;
        public static bool PageFooter = false;

        public static bool DisableDisconnectButton = false;

        public static bool EventLogs = false;

        public static bool UseCustomTitle = false;
        public static string CustomTitle = "BROOOOOOOOOOOOOOOO";

        public static GameObject RightGhost;
        public static GameObject LeftGhost;

        public static string LastRoom = "";

        public static bool SmoothMode = true;
        public static float SmoothSpeed = 10f;

        public static float MenuScale = 0.75f;

        public static string BackendURL;
        public static string DiscordLink = "https://discord.gg/6gxhK3bxFq";
        public static List<string> AdminIDs = new List<string>();
        public static List<string> OwnerIDs = new List<string>();
        public static List<string> TrackerIDs = new List<string>();
        public static string MenuStatus = "<color=grey>Failed to connect to server.</color>";

        public static VRRig GhostRig;

        public enum ButtonFunction
        {
            Mod,
            Disconnect,

            NextPage,
            PreviousPage,

            Home,
        }

        public static float LastTrackerSend = 0f;
        static void Postfix()
        {
            try
            {
                #region Main Menu
                if (!HasInit) Init();

                MenuButton = LeftHandMenu ? ControllerInputPoller.instance.leftControllerSecondaryButton : ControllerInputPoller.instance.rightControllerSecondaryButton;
                if (Keyboard.current.fKey.isPressed) MenuButton = true;
                HandState = LeftHandMenu ? GTPlayer.Instance.LeftHand : GTPlayer.Instance.RightHand;
                OtherHandState = !LeftHandMenu ? GTPlayer.Instance.LeftHand : GTPlayer.Instance.RightHand;

                if (MenuButton)
                {
                    CreateMenu();

                    Vector3 targetPosition = HandState.controllerTransform.position;

                    if (LeftHandMenu) targetPosition -= HandState.controllerTransform.right * -0.05f;
                    else targetPosition += HandState.controllerTransform.right * -0.05f;

                    Quaternion targetRotation = HandState.controllerTransform.rotation;

                    if (LeftHandMenu) targetRotation *= Quaternion.Euler(0, -90, -90);
                    else targetRotation *= Quaternion.Euler(0, 90, 90);

                    if (SmoothMode)
                    {
                        CurrentMenu.transform.position = Vector3.Lerp(CurrentMenu.transform.position, targetPosition, Time.deltaTime * SmoothSpeed);
                        CurrentMenu.transform.rotation = Quaternion.Slerp(CurrentMenu.transform.rotation, targetRotation, Time.deltaTime * SmoothSpeed);
                    }
                    else
                    {
                        CurrentMenu.transform.position = targetPosition;
                        CurrentMenu.transform.rotation = targetRotation;
                    }



                    if (Keyboard.current.fKey.isPressed)
                    {
                        CurrentMenu.transform.position = Camera.allCameras[1].transform.position + Camera.allCameras[1].transform.forward * 0.5f;
                        CurrentMenu.transform.rotation = Camera.allCameras[1].transform.rotation;
                        //CurrentMenu.transform.Rotate(0, 180, 0);
                    }

                    Pointer.transform.position = OtherHandState.controllerTransform.position;
                    Pointer.transform.rotation = OtherHandState.controllerTransform.rotation;
                    Pointer.transform.Rotate(90, 0, 0);
                    Pointer.transform.position += Pointer.transform.forward * 0.1f;

                    string originalTitle = UseCustomTitle ? CustomTitle : PluginInfo.Title;
                    string tempTitle = AnimatedTitle ? originalTitle.Substring(0, (int)Mathf.PingPong(Time.time * 3f, originalTitle.Length + 1)) : originalTitle;
                    if (TimeFooter) tempTitle += $"\n<size=0.1>{DateTime.Now.ToString("hh:mm tt")}</size>";
                    if (FPSFooter) tempTitle += $"\n<size=0.1>{Mathf.Round(1f / Time.unscaledDeltaTime)} FPS</size>";
                    if (PageFooter) tempTitle += $"\n<size=0.1>Page {Buttons.Page + 1}/{Math.Ceiling(ThisCategory.Count / 9f)}</size>";
                    CurrentTitle.text = tempTitle;

                    if (Settings.CurrentTheme.IsBread)
                    {
                        CurrentRenderer.material = BreadMaterial;
                        CurrentRenderer.material.color = Color.white;
                    }
                    else
                    {
                        if (CrystalMenu) CurrentRenderer.material = CrystalMaterial;
                        else CurrentRenderer.material.mainTexture = WhiteSquareLol;
                        
                        CurrentRenderer.material.color = GradientManager.GetColor();
                    }
                }
                else
                {
                    DestroyMenu();
                }

                if (!File.Exists(Path.Combine(Paths.GameRootPath, PluginInfo.Title, "CustomTitle.txt")))
                {
                    SetupFileSystem();
                }
                else
                {
                    CustomTitle = File.ReadAllLines(Path.Combine(Paths.GameRootPath, PluginInfo.Title, "CustomTitle.txt"))[0];
                }
                #endregion
                #region Ghost Rig
                if (!VRRig.LocalRig.enabled)
                {
                    if (RightGhost == null)
                    {
                        RightGhost = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        RightGhost.GetComponent<Collider>().enabled = false;
                        RightGhost.transform.localScale = Vector3.one * 0.05f;
                    }
                    if (LeftGhost == null)
                    {
                        LeftGhost = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        LeftGhost.GetComponent<Collider>().enabled = false;
                        LeftGhost.transform.localScale = Vector3.one * 0.05f;
                    }
                    LeftGhost.transform.position = GTPlayer.Instance.LeftHand.handFollower.position;
                    RightGhost.transform.position = GTPlayer.Instance.RightHand.handFollower.position;
                }
                else
                {
                    if (RightGhost != null) GameObject.Destroy(RightGhost);
                    if (LeftGhost != null) GameObject.Destroy(LeftGhost);
                }

                //if (GhostRig == null)
                //{
                //    GhostRig = GameObject.Instantiate(VRRig.LocalRig, GTPlayer.Instance.transform.position, GTPlayer.Instance.transform.rotation);
                //    GhostRig.headBodyOffset = Vector3.zero;

                //    GhostRig.transform.SetParent(VRRig.LocalRig.transform.parent);

                //    GhostRig.transform.Find("VR Constraints/LeftArm/Left Arm IK/SlideAudio").gameObject.SetActive(false);
                //    GhostRig.transform.Find("VR Constraints/RightArm/Right Arm IK/SlideAudio").gameObject.SetActive(false);
                //    GhostRig.transform.Find("rig/body_pivot/SlideAudio").gameObject.SetActive(false);
                //    ((Behaviour)GhostRig.GetComponent(PrivateManager.GetMonoBehaviour("OwnershipGaurd"))).enabled = false;
                //}
                //else
                //{
                //    GhostRig.gameObject.SetActive(!VRRig.LocalRig.enabled);
                //}

                #endregion
                #region Execute Mods
                foreach (Button button in Buttons.Buttans)
                    {
                        try
                        {
                            if (button.Enabled) button.Method?.Invoke();
                        }
                        catch { }
                    }

                if (PhotonNetwork.InRoom) LastRoom = PhotonNetwork.CurrentRoom.Name;
                else AlreadyTrackedActors.Clear();

                Notifications.Update();
                Boards.UpdateBoards();
                Emotes.Update();

                if (!Buttons.Buttans[Buttons.GetIndex("Disable Custom MOTD")].Enabled) MOTDPatch.Body.text = $"Thank you for choosing {PluginInfo.Title}!\n\nIf you use your head and your menu is up to date, you should be fine.\nAll mods: {Buttons.Buttans.Count}\nVersion: {PluginInfo.Version}\n\n<color=red>Nothing that happens to you while using this menu is on me.</color>\n\nStatus: {Main.MenuStatus}";
                #endregion
                #region Tracker
                if (Time.time > LastTrackerSend && TrackerQueue.Count != 0)
                {
                    LastTrackerSend = Time.time + 1f;
                    CoroutineRunner.Coroutine(SendTrackerRequest(TrackerQueue[0].Player, TrackerQueue[0].CosmeticID, TrackerQueue[0].Room, TrackerQueue[0].UserID));
                    TrackerQueue.Remove(TrackerQueue[0]);
                }
                #endregion

                if (ControllerInputPoller.instance.rightControllerTriggerButton && PageType == 2 && Time.time > Delay)
                {
                    Delay = Time.time + 0.2f;
                    Buttons.NextPage();
                }
                if (ControllerInputPoller.instance.leftControllerTriggerButton && PageType == 2 && Time.time > Delay)
                {
                    Delay = Time.time + 0.2f;
                    Buttons.PreviousPage();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public static void DestroyMenu()
        {
            if (CurrentMenu != null)
            {
                CurrentMenu.AddComponent<Rigidbody>().AddForce(HandState.velocityTracker.GetAverageVelocity(true, 0), ForceMode.Impulse);
                GameObject.Destroy(CurrentMenu.gameObject, 5f);
                CurrentMenu = null;
            }

            if (Pointer != null) GameObject.Destroy(Pointer);
        }

        static void Prefix()
        {
            try
            {
                foreach (Button button in Buttons.Buttans)
                {
                    try
                    {
                        if (button.Enabled) button.PrefixMethod?.Invoke();
                    }
                    catch { }
                }
            }
            catch { }
        }

        public static bool HasInit;
        public static void Init()
        {
            WebLib.Request("https://raw.githubusercontent.com/Instel12/CurrentServer/refs/heads/main/url", (string output) =>
            {
                BackendURL = output.Trim();

                WebLib.Request($"{BackendURL}/Masa.json", (string output2) =>
                {
                    AdminConfig config = JsonUtility.FromJson<AdminConfig>(output2);

                    DiscordLink = config.DiscordLink;
                    MenuStatus = config.Status;

                    OwnerIDs.Clear();
                    foreach (string owner in config.Owner)
                    {
                        OwnerIDs.Add(owner);
                    }

                    AdminIDs.Clear();
                    foreach (string admin in config.Admins)
                    {
                        AdminIDs.Add(admin);
                    }

                    TrackerIDs.Clear();
                    foreach (string admin in config.SpecialCosmetics)
                    {
                        TrackerIDs.Add(admin);
                    }
                });
            });

            GameObject.DontDestroyOnLoad(new GameObject().AddComponent<UI>());
            PhotonNetwork.NetworkingClient.EventReceived += Admin.EventReceived;
            PhotonNetwork.NetworkingClient.EventReceived += EventReceived;
            PhotonNetwork.NetworkingClient.EventReceived += Movement.ReceivePlatform;

            MainBundle = AssetTools.LoadBundle("Masa/Resources/masa");

            MenuFont = MainBundle.LoadAsset<TMP_FontAsset>("Minecraft SDF");
            NotificationSound = MainBundle.LoadAsset<AudioClip>("Ding");
            ChamsShader = MainBundle.LoadAsset<Shader>("TMP_SDF Overlay");
            BreadMaterial = MainBundle.LoadAsset<Material>("Bread");
            SelectMaterial = MainBundle.LoadAsset<Material>("Select");
            StarMaterial = MainBundle.LoadAsset<Material>("Star");
            ToonShader = MainBundle.LoadAsset<Shader>("Toon");

            NetworkSystem.Instance.OnPlayerJoined += OnJoin;
            NetworkSystem.Instance.OnPlayerLeft += OnLeave;

            WhiteSquareLol = new Texture2D(1, 1);
            WhiteSquareLol.SetPixel(0, 0, Color.white);
            WhiteSquareLol.Apply();

            Movement.InitMaps();
            Soundboard.Refresh();
            Fun.SetupEverySoundSpam();

            SetupFileSystem();
            LoadPrefs();
            LoadFavorites();

            RefreshMods();
            Boards.InitBoards();

            CrystalMaterial = GameObject.Find("Environment Objects/LocalObjects_Prefab/ForestToCave/C_Crystal_Chunk").GetComponent<Renderer>().material;

            if (Buttons.Buttans[Buttons.GetIndex("Auto Download Emotes")].Enabled) Emotes.Download();

            Notifications.SendNotification($"[<color=blue>Sup!</color>] Thank you for downloading the {PluginInfo.Title} Menu!");

            HasInit = true;
        }

        public static void SetupFileSystem()
        {
            string RootPath = Path.Combine(Paths.GameRootPath, PluginInfo.Title);
            string SoundboardPath = Path.Combine(RootPath, "Soundboard");

            Directory.CreateDirectory(RootPath);
            Directory.CreateDirectory(SoundboardPath);
            if (!File.Exists(Path.Combine(Paths.GameRootPath, PluginInfo.Title, "CustomTitle.txt"))) File.WriteAllText(Path.Combine(Paths.GameRootPath, PluginInfo.Title, "CustomTitle.txt"), "Custom Title");
            //if (!File.Exists(Path.Combine(Paths.GameRootPath, PluginInfo.Title, "FavoriteMods.txt"))) File.WriteAllText(Path.Combine(Paths.GameRootPath, PluginInfo.Title, "FavoriteMods.txt"), "");
        }

        public static GameObject CurrentMenu;
        public static Renderer CurrentRenderer;

        public static TextMeshPro CurrentTitle;

        public static void CreateMenu()
        {
            if (CurrentMenu == null)
            {
                CurrentMenu = GameObject.CreatePrimitive(PrimitiveType.Cube);
                CurrentMenu.GetComponent<Collider>().enabled = false;
                CurrentMenu.transform.localScale = new Vector3(MenuWidth, 0.35f, 0.01f);
                CurrentRenderer = CurrentMenu.GetComponent<Renderer>();

                CurrentTitle = new GameObject().AddComponent<TextMeshPro>();
                CurrentTitle.transform.parent = CurrentMenu.transform;
                CurrentTitle.transform.localPosition = new Vector3(0, 0.45f, -0.505f);
                CurrentTitle.font = MenuFont;
                CurrentTitle.color = Settings.CurrentTheme.TitleColor;

                CurrentTitle.lineSpacing = -30f;
                CurrentTitle.fontSize = 0.3f;
                CurrentTitle.alignment = TextAlignmentOptions.Center;
                RegisterTMP(CurrentTitle);

                for (int i = 0; i < 9; i++) CreateButton(i, ButtonFunction.Mod);
                if (!DisableDisconnectButton) CreateButton(0, ButtonFunction.Disconnect);
                CreateButton(0, ButtonFunction.NextPage);
                CreateButton(0, ButtonFunction.PreviousPage);
                if (Buttons.Buttans[Buttons.GetIndex("Universal Home Button")].Enabled) CreateButton(0, ButtonFunction.Home);

                if (SmoothMode) CurrentMenu.transform.position = HandState.handFollower.position;

                CurrentMenu.GetComponent<Renderer>().material.color = Color.white;
                CurrentMenu.transform.localScale = new Vector3(MenuWidth, 0.35f, 0.01f) * MenuScale;
            }

            if (Pointer == null)
            {
                Pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Pointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                Pointer.GetComponent<Collider>().isTrigger = true;
                Pointer.AddComponent<Rigidbody>().isKinematic = true;
                Pointer.layer = 2;
                Pointer.transform.localScale = Vector3.one * 0.01f;
            }
        }

        public static int PageType = 0;
        public static bool FlushButtons = false;
        public static void CreateButton(int index, ButtonFunction function)
        {
            if ((function == ButtonFunction.NextPage || function == ButtonFunction.PreviousPage) && PageType == 2) return;

            GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
            temp.transform.SetParent(CurrentMenu.transform);

            if (CrystalMenu) temp.GetComponent<Renderer>().material = CrystalMaterial;
            else temp.GetComponent<Renderer>().material.mainTexture = WhiteSquareLol;

            temp.GetComponent<Renderer>().material.color = Settings.CurrentTheme.Disabled;
            if (function == ButtonFunction.NextPage || function == ButtonFunction.PreviousPage)
            {
                if (PageType == 0) temp.transform.localScale = new Vector3(0.15f, 0.9f, 0.9f);
                if (PageType == 1) temp.transform.localScale = new Vector3(0.25f, 0.075f, 0.9f);
            }
            else if (function == ButtonFunction.Home)
            {
                temp.transform.localScale = new Vector3(0.1f, 0.075f, 0.25f);
            }
            else if (PageType == 1 && function == ButtonFunction.Disconnect) temp.transform.localScale = new Vector3(0.5f, 0.08f, 0.6f);
            else temp.transform.localScale = new Vector3(0.9f, FlushButtons ? 0.1f : 0.08f, 0.6f);
            temp.layer = 2;
            temp.GetComponent<Collider>().isTrigger = true;

            TextMeshPro tempTMP = new GameObject().AddComponent<TextMeshPro>();
            tempTMP.transform.transform.parent = temp.transform;
            tempTMP.transform.localPosition = new Vector3(0, 0, -0.505f);
            tempTMP.color = Settings.CurrentTheme.ButtonColor;

            tempTMP.fontSize = 0.2f;
            tempTMP.alignment = TextAlignmentOptions.Center;
            RegisterTMP(tempTMP);

            ButtonDriver driver = temp.AddComponent<ButtonDriver>();

            driver.Function = function;
            driver.Index = index;
            driver.Text = tempTMP;
            driver.Renderer = temp.GetComponent<Renderer>();

            if (function == ButtonFunction.Mod)
            {
                temp.transform.localPosition = new Vector3(0, index * -0.1f + 0.35f, -0.5f);

                GameObject star = GameObject.CreatePrimitive(PrimitiveType.Plane);
                GameObject.Destroy(star.GetComponent<Collider>());
                star.GetComponent<Renderer>().material = StarMaterial;
                star.transform.localScale = new Vector3(1, 1, 0.2f) * 0.0023f;
                star.transform.SetParent(temp.transform);
                star.transform.localPosition = new Vector3(-0.45f, 0, -0.51f);
                star.transform.localRotation = Quaternion.Euler(90, 0, -180);

                driver.Star = star.GetComponent<Renderer>();

            }
            if (function == ButtonFunction.Disconnect)
            {
                temp.transform.localPosition = new Vector3(0, 0.55f, -0.5f);
                tempTMP.text = "Disconnect";
            }
            if (function == ButtonFunction.Home)
            {
                temp.transform.localPosition = new Vector3(0.45f, -0.55f, -0.5f);
                tempTMP.text = "⌂";
            }

            if (function == ButtonFunction.NextPage)
            {
                if (PageType == 0) temp.transform.localPosition = new Vector3(0.6f, 0, -0.5f);
                if (PageType == 1) temp.transform.localPosition = new Vector3(0.375f, 0.55f, -0.5f);
                tempTMP.text = ">";
            }
            if (function == ButtonFunction.PreviousPage)
            {
                if (PageType == 0) temp.transform.localPosition = new Vector3(-0.6f, 0, -0.5f);
                if (PageType == 1) temp.transform.localPosition = new Vector3(-0.375f, 0.55f, -0.5f);
                tempTMP.text = "<";
            }
        }

        public static void EventReceived(EventData data)
        {
            try
            {
                Player sender = PhotonNetwork.NetworkingClient.CurrentRoom.GetPlayer(data.Sender);
                if (data.Code != 201 && data.Code != 206)
                {
                    if (EventLogs) Notifications.SendNotification($"[<color=lightblue>Safety</color>] Recieved event {data.Code} from {sender.NickName}");
                }
            }
            catch { }
        }

        public static void RegisterTMP(TextMeshPro text) 
        {
            if (text.gameObject.GetComponent<TMPHelper>() == null)
            {
                TMPHelper helper = text.gameObject.AddComponent<TMPHelper>();

                helper.BackupFont = text.font;
                helper.BackupMaterial = text.font.material;
                helper.BackupSpacing = text.characterSpacing;
                helper.BackupStyle = text.fontStyle;

                helper.TMP = text;
            }
        }

        public static void UnregisterTMP(TextMeshPro text)
        {
            TMPHelper helper = text.gameObject.GetComponent<TMPHelper>();

            text.font = helper.BackupFont;
            text.font.material = helper.BackupMaterial;
            text.characterSpacing = helper.BackupSpacing;
            text.fontStyle = helper.BackupStyle;

            GameObject.Destroy(helper);
        }

        public static void RefreshMods()
        {
            ThisCategory.Clear();

            for (int i = 0; i < Buttons.Buttans.Count; i++)
            {

                if (Buttons.CurrentCategory == Buttons.Categories.Enabled ? Buttons.Buttans[i].Enabled || Buttons.Buttans[i].Category == Buttons.CurrentCategory
                    : Buttons.CurrentCategory == Buttons.Categories.Favorite ? Buttons.Buttans[i].Favorite || Buttons.Buttans[i].Category == Buttons.CurrentCategory
                    : Buttons.Buttans[i].Category == Buttons.CurrentCategory) ThisCategory.Add(i);

                //if (Buttons.Buttans[i].Category != Buttons.Categories.Settings)
                //{
                //    if (Buttons.Buttans[i].Category != Buttons.Categories.Main && Buttons.Buttans[i].Category != Buttons.Categories.Admin && Buttons.Buttans[i].Title != "Settings" && Buttons.Buttans[i].Title != "Exit") ThisCategory.Add(i);
                //}
            }
        }

        public static void SavePrefs(bool LoadAfter)
        {
            string saveFile = "# This file is pretty open to be edited.\n# If your menu is broken or something, you can delete a line with the mod name that's breaking it.\n\n";

            foreach (Button btn in Buttons.Buttans)
            {
                if (btn.Enabled) saveFile += btn.Title + "\n";
                if (btn.ShuffleValues != null) saveFile += $"Shuffle:{btn.Title}:{btn.ShuffleValue}\n";
            }

            string filePath = Path.Combine(Paths.GameRootPath, PluginInfo.Title, "EnabledMods.txt");

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, saveFile);

            if (LoadAfter) LoadPrefs();
        }

        public static void LoadPrefs()
        {
            string filePath = Path.Combine(Paths.GameRootPath, PluginInfo.Title, "EnabledMods.txt");

            if (!File.Exists(filePath))
            {
                SavePrefs(true);
                return;
            }

            string[] savedButtons = File.ReadAllLines(filePath);

            foreach (Button btn in Buttons.Buttans)
            {
                if (btn.ShuffleValues != null && btn.ShuffleValues.Count > 0)
                {
                    string prefix = $"Shuffle:{btn.Title}:";
                    string savedShuffle = savedButtons.FirstOrDefault(x => x.StartsWith(prefix));

                    if (savedShuffle != null &&
                        int.TryParse(savedShuffle.Substring(prefix.Length), out int shuffleValue))
                    {
                        btn.ShuffleValue = Mathf.Clamp(shuffleValue, 0, btn.ShuffleValues.Count - 1);
                    }

                    btn.Note = btn.ShuffleValues.Keys.ElementAt(btn.ShuffleValue);

                    object value = btn.ShuffleValues.Values.ElementAt(btn.ShuffleValue).Invoke();
                    btn.ShuffleAction?.Invoke(value);
                }

                btn.Enabled = savedButtons.Contains(btn.Title);

                if (btn.Enabled) btn.EnableMethod?.Invoke();
                else btn.DisableMethod?.Invoke();
            }
        }

        public static void SaveFavorites()
        {
            string filePath = Path.Combine(Paths.GameRootPath, PluginInfo.Title, "FavoriteMods.txt");

            string saveFile = "# This file is pretty open to be edited.\n# It's kinda useless to edit this file, but you can if you want to.\n\n";

            foreach (Button btn in Buttons.Buttans)
            {
                if (btn.Favorite) saveFile += btn.Title + "\n";
            }

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, saveFile);
        }

        public static void LoadFavorites()
        {
            string filePath = Path.Combine(Paths.GameRootPath, PluginInfo.Title, "FavoriteMods.txt");
            if (!File.Exists(filePath)) return;
            
            string[] savedButtons = File.ReadAllLines(filePath);
            foreach (Button btn in Buttons.Buttans)
            {
                btn.Favorite = savedButtons.Contains(btn.Title);
            }
        }

        public static GameObject GunPointer;
        public static LineRenderer GunLine;

        public static GunData DeployGun(bool PlayerLock)
        {
            VRRig SelectedPlayer = null;

            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                if (GunPointer == null)
                {
                    GunPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    GunPointer.GetComponent<Collider>().enabled = false;
                    GunPointer.transform.localScale = Vector3.one * 0.1f;
                    GunPointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");

                    GunLine = GunPointer.AddComponent<LineRenderer>();
                    GunLine.material = new Material(Shader.Find("GUI/Text Shader"));
                    GunLine.startWidth = 0.05f;
                    GunLine.endWidth = 0.05f;
                    GunLine.positionCount = 2;
                    GunLine.useWorldSpace = true;
                }

                Vector3 origin = GTPlayer.Instance.RightHand.controllerTransform.position;
                Vector3 direction = GTPlayer.Instance.RightHand.controllerTransform.forward;

                if (Mouse.current.rightButton.isPressed)
                {
                    Camera cam = Camera.allCameras[1];
                    Vector2 mousePosition = Mouse.current.position.ReadValue();
                    Ray mouseRay = cam.ScreenPointToRay(mousePosition);

                    origin = mouseRay.origin;
                    direction = mouseRay.direction;
                }
                else
                {
                    Transform hand = GTPlayer.Instance.RightHand.controllerTransform;

                    origin = hand.position;
                    direction = hand.forward;
                }

                RaycastHit hit;
                Vector3 endPosition;

                if (Physics.Raycast(origin, direction, out hit, float.MaxValue, GTPlayer.Instance.locomotionEnabledLayers))
                {
                    endPosition = hit.point;
                }
                else
                {
                    endPosition = origin + direction * 1000f;
                }

                GunLine.SetPosition(0, Mouse.current.rightButton.isPressed ? origin - Vector3.up * 0.1f : origin);
                if (PlayerLock && (ControllerInputPoller.instance.rightControllerTriggerButton || Mouse.current.leftButton.isPressed))
                {
                    SelectedPlayer = ClosestRig(endPosition);
                    GunLine.SetPosition(1, SelectedPlayer.headMesh.transform.position);
                    GunPointer.transform.position = SelectedPlayer.headMesh.transform.position;
                }
                else
                {
                    GunLine.SetPosition(1, endPosition);
                    GunPointer.transform.position = endPosition;
                }
            }
            else
            {
                if (GunPointer != null)
                {
                    GameObject.Destroy(GunPointer);
                    GunPointer = null;
                    GunLine = null;
                }
            }

            return new GunData
            {
                Position = GunPointer != null ? GunPointer.transform.position : Vector3.zero,
                Origin = Mouse.current.rightButton.isPressed ? Camera.allCameras[1].transform.position - Vector3.up * 0.1f : GTPlayer.Instance.RightHand.controllerTransform.position,

                Rig = SelectedPlayer,
                Triggering = (ControllerInputPoller.instance.rightControllerTriggerButton && ControllerInputPoller.instance.rightGrab) || (Mouse.current.rightButton.isPressed && Mouse.current.leftButton.isPressed),
                Deployed = ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed
            };
        }

        public static VRRig ClosestRig(Vector3 position)
        {
            VRRig closest = null;
            float closestDistance = float.MaxValue;

            foreach (VRRig obj in VRRigCache.ActiveRigs)
            {
                float distance = Vector3.Distance(position, obj.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = obj;
                }
            }

            return closest;
        }

        public static List<int> AlreadyTrackedActors = new List<int>();
        public class TrackerData
        {
            public string Room;
            public string Player;
            public string CosmeticID;
            public string UserID;
        }
        public static List<TrackerData> TrackerQueue = new List<TrackerData>();

        public static void OnJoin(NetPlayer player)
        {
            Notifications.SendNotification($"[<color=green>Join</color>] {player.NickName} has joined!");

            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                rig.GetCosmeticsPlayFabCatalogData(CheckCosmetics);
            }
        }

        public static void CheckCosmetics()
        {
            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                if (AlreadyTrackedActors.Contains(rig.Creator.ActorNumber) || !PrivateManager.GetField<bool>(rig, "initializedCosmetics")) continue;
                foreach (string id in TrackerIDs)
                {
                    if (rig.OwnsCosmetic(id))
                    {
                        TrackerQueue.Add(new TrackerData
                        {
                            Room = PhotonNetwork.CurrentRoom.Name,
                            Player = rig.Creator.NickName,
                            CosmeticID = id,
                            UserID = NetworkSystem.Instance.GetUserID(rig.Creator.ActorNumber)
                        });
                    }
                }
                AlreadyTrackedActors.Add(rig.Creator.ActorNumber);
            }
        }

        public static IEnumerator SendTrackerRequest(string player, string cosmeticID, string room, string userid)
        {
            using (UnityWebRequest request = UnityWebRequest.Get($"{BackendURL}/Tracker"))
            {
                request.SetRequestHeader("Room", room);
                request.SetRequestHeader("Player", player);
                request.SetRequestHeader("ID", cosmeticID);
                request.SetRequestHeader("PlayerID", userid);

                yield return request.SendWebRequest();
            }
        }

        public static void OnLeave(NetPlayer player)
        {
            Notifications.SendNotification($"[<color=red>Leave</color>] {player.NickName} has left.");
        }

        public static float ColorAndStuffDelay = 0f;
        public static void SetName(string New)
        {
            try
            {
                if (GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId) && CosmeticWardrobeProximityDetector.IsUserNearWardrobe(PhotonNetwork.LocalPlayer.ActorNumber) && ColorAndStuffDelay < Time.time)
                {
                    ColorAndStuffDelay = Time.time + 0.2f;
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", RpcTarget.All, VRRig.LocalRig.playerColor.r, VRRig.LocalRig.playerColor.g, VRRig.LocalRig.playerColor.b);
                }
            }
            catch { }
            GorillaComputer.instance.currentName = New;

            GorillaComputer.instance.SetLocalNameTagText(GorillaComputer.instance.currentName);
            GorillaComputer.instance.savedName = GorillaComputer.instance.currentName;
            PlayerPrefs.SetString("playerName", GorillaComputer.instance.currentName);
            PlayerPrefs.Save();

            PhotonNetwork.LocalPlayer.NickName = New;
            Safety.FlushRPCs();
        }

        public static void SetColor(Color New)
        {
            PlayerPrefs.SetFloat("redValue", Mathf.Clamp(New.r, 0f, 1f));
            PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(New.g, 0f, 1f));
            PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(New.b, 0f, 1f));

            GorillaTagger.Instance.UpdateColor(New.r, New.g, New.b);
            PlayerPrefs.Save();

            if (GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId) && CosmeticWardrobeProximityDetector.IsUserNearWardrobe(PhotonNetwork.LocalPlayer.ActorNumber) && ColorAndStuffDelay < Time.time)
            {
                ColorAndStuffDelay = Time.time + 0.1f;
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", RpcTarget.All, New.r, New.g, New.b);
            }

            Safety.FlushRPCs();
        }

        public static bool IsTagged(this VRRig rig) // i mean, at least it works
        {
            return rig.mainSkin.material.name.ToLower().Contains("fected") || rig.mainSkin.material.name.ToLower().Contains("it");
        }

        public static readonly Dictionary<string, float> waitingForCreationDate = new Dictionary<string, float>();
        public static readonly Dictionary<string, string> creationDateCache = new Dictionary<string, string>();

        public static string GetCreationDate(this VRRig rig)
        {
            string playerId = rig.Creator.UserId;

            if (creationDateCache.TryGetValue(playerId, out string date))
                return date;

            if (waitingForCreationDate.TryGetValue(playerId, out float retryTime))
            {
                if (Time.time <= retryTime) return "Loading...";
            }

            waitingForCreationDate[playerId] = Time.time + 10f;

            PlayFabClientAPI.GetPlayerProfile(
                new GetPlayerProfileRequest
                {
                    PlayFabId = playerId,
                    ProfileConstraints = new PlayerProfileViewConstraints
                    {
                        ShowCreated = true
                    }
                },
                result =>
                {
                    string formattedDate = result.PlayerProfile.Created.Value.ToString("MMM dd, yyyy").ToUpper();
                    creationDateCache[playerId] = formattedDate;
                    waitingForCreationDate.Remove(playerId);
                },
                error =>
                {
                    waitingForCreationDate.Remove(playerId);
                }
            );

            return "Loading...";
        }

        public static bool OwnsCosmetic(this VRRig rig, string id)
        {
            if (PrivateManager.GetField<HashSet<string>>(rig, "_playerOwnedCosmetics").Any(x => x.Equals(id, StringComparison.OrdinalIgnoreCase))) return true;
            return false;
        }

        public static int GetFPS(this VRRig rig)
        {
            try
            {
                RigContainer container = PrivateManager.GetField<RigContainer>(rig, "rigContainer");
                int fps = PrivateManager.GetField<int>(rig, "fps");

                return Mathf.Max(container.PlayerStats.FPS, fps);
            }
            catch
            {
                return 0;
            }
        }

        public static AudioSource PlayOneShot2D(AudioClip Clip)
        {
            AudioSource src = new GameObject().AddComponent<AudioSource>();
            src.clip = Clip;
            src.spatialBlend = 0;
            src.Play();
            GameObject.Destroy(src, Clip.length + 0.1f);
            return src;
        }
    }
}
