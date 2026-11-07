using System.Collections.Generic;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using Masa.Classes;
using Masa.Menu;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Masa.Mods
{
    class Admin
    {
        public static string ServerBase = "https://instel12.github.io/MasaAdmin";
        public static string NotAdminError = "[<color=red>Error!</color>] You're not an admin.";
        #region Sender
        public static bool KickGunWait = false;

        public static void KickGun()
        {
            GunData Data = Main.DeployGun(true);
            if (Data.Triggering)
            {
                if (!KickGunWait) SendAdminRPC("Kick", new object[] { Data.Rig.Creator.ActorNumber });
                KickGunWait = true;
            }
            else
            {
                KickGunWait = false;
            }
        }

        public static bool CrashGunWait = false;
        public static void CrashGun()
        {
            GunData Data = Main.DeployGun(true);
            if (Data.Triggering)
            {
                if (!CrashGunWait) SendAdminRPC("Crash", new object[] { Data.Rig.Creator.ActorNumber });
                CrashGunWait = true;
            }
            else
            {
                CrashGunWait = false;
            }
        }

        public static bool TeleportGunWait = false;
        public static void TeleportGun()
        {
            GunData Data = Main.DeployGun(false);
            if (Data.Triggering)
            {
                if (!TeleportGunWait) SendAdminRPC("Teleport", new object[] { Data.Position });
                TeleportGunWait = true;
            }
            else
            {
                TeleportGunWait = false;
            }
        }

        public static bool PrefabGunWait = false;
        public static void PrefabGun(bool PlayerSelect, string PrefabName)
        {
            GunData Data = Main.DeployGun(PlayerSelect);
            if (Data.Triggering)
            {
                if (!PrefabGunWait) SendAdminRPC("ClonePrefab", new object[] { PrefabName, PlayerSelect ? Data.Rig.bodyTransform.transform.position : Data.Position });
                PrefabGunWait = true;
            }
            else
            {
                PrefabGunWait = false;
            }
        }
        public static void SpawnPlane()
        {
            SendAdminRPC("ClonePrefab", new object[] { "theplane", Vector3.zero });
            GTPlayer.Instance.TeleportTo(new Vector3(0, 15, 5000), Quaternion.identity, false, true);
        }

        public static bool MinecaftGunWait = false;
        public static GameObject Preview;
        public static float Size = 0.5f;
        public static void MinecraftGun(string BlockName)
        {
            GunData Data = Main.DeployGun(false);
            Vector3 NormalizedPosition = Data.Position + (Data.Origin - Data.Position).normalized * 0.35f;
            Vector3 position = new Vector3(MinecraftRound(NormalizedPosition.x, Size), MinecraftRound(NormalizedPosition.y, Size), MinecraftRound(NormalizedPosition.z, Size));
            if (Data.Deployed)
            {
                if (Preview == null)
                {
                    Preview = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Preview.GetComponent<Collider>().enabled = false;
                    Preview.GetComponent<Renderer>().material = Main.SelectMaterial;
                    Preview.transform.localScale = Vector3.one * Size;
                }
                else
                {
                    Preview.transform.position = position;
                }
            }
            else
            {
                if (Preview != null) GameObject.Destroy(Preview);
            }

            if (Data.Triggering)
            {
                if (!MinecaftGunWait) SendAdminRPC("Minecraft", new object[] { BlockName, position });

                MinecaftGunWait = true;
            }
            else
            {
                MinecaftGunWait = false;
            }
        }

        public static float MinecraftRound(float origin, float size)
        {
            //size = size / 2;
            return Mathf.Round(origin / size) * size;
        }

        public static void DestroyAll()
        {
            SendAdminRPC("DestroyAll", new object[] {});
        }

        public static float RPCDelay;
        public static void SendAdminRPC(string command, object[] attributes)
        {
            //if (RPCDelay < Time.time)
            //{
                RPCDelay = Time.time + 0.5f;
                object[] data = new object[attributes.Length + 1];
                data[0] = command;

                for (int i = 0; i < attributes.Length; i++)
                {
                    data[i + 1] = attributes[i];
                }

                PhotonNetwork.RaiseEvent(
                    53,
                    data,
                    new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.All
                    },
                    SendOptions.SendReliable
                );
            //}
            //else
            //{
            //    Notifications.SendNotification("[<color=red>Admin</color>] You hit the rate limit, slow down!");
            //}
        }
        #endregion
        #region Reciever
        public static List<GameObject> InstanitatedObjects = new List<GameObject>();

        public static void EventReceived(EventData data)
        {
            try
            {
                if (data.Code == 53)
                {
                    Player sender = PhotonNetwork.NetworkingClient.CurrentRoom.GetPlayer(data.Sender);
                    object[] arguments = data.CustomData as object[];
                    string command = arguments[0].ToString();

                    EventAction(sender, command, arguments);
                }
            }
            catch { }
        }

        public static AssetBundle TempBundle;

        public static void EventAction(Player sender, string command, object[] arguements)
        {
            switch (command)
            {
                case "Kick":
                    if ((int)arguements[1] == PhotonNetwork.LocalPlayer.ActorNumber) NetworkSystem.Instance.ReturnToSinglePlayer();
                    break;
                case "Crash":
                    if ((int)arguements[1] == PhotonNetwork.LocalPlayer.ActorNumber) Application.Quit();
                    break;
                case "Teleport":
                    if (sender.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber) GTPlayer.Instance.TeleportTo((Vector3)arguements[1], Quaternion.identity, false, true);
                    break;
                case "ClonePrefab":
                    CoroutineRunner.Coroutine(AssetTools.DownloadBundle(ServerBase + "/Bundles/" + (string)arguements[1], bundle =>
                    {
                        TempBundle = bundle;

                        GameObject Temp = GameObject.Instantiate(TempBundle.LoadAsset<GameObject>("Prefab"));
                        Temp.transform.position = (Vector3)arguements[2];
                        InstanitatedObjects.Add(Temp);
                    }));
                    
                    break;
                case "DestroyAll":
                    foreach (GameObject obj in InstanitatedObjects)
                    {
                        GameObject.Destroy(obj);
                    }
                    InstanitatedObjects.Clear();
                    break;
                case "Minecraft":
                    CoroutineRunner.Coroutine(AssetTools.DownloadBundle(ServerBase + "/Bundles/minecraft", bundle =>
                    {
                        TempBundle = bundle;

                        GameObject Temp = GameObject.Instantiate(TempBundle.LoadAsset<GameObject>((string)arguements[1]));
                        Temp.transform.position = (Vector3)arguements[2];
                        Temp.transform.localScale = Vector3.one * Size;
                        InstanitatedObjects.Add(Temp);
                    }));
                    break;
            }
        }
        #endregion
    }
}
