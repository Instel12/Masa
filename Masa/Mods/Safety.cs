using System;
using System.Collections.Generic;
using System.Linq;
using Masa.Classes;
using Masa.Menu;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Masa.Mods
{
    class Safety
    {
        public static void FlushRPCs()
        {
            if (NetworkSystem.Instance.InRoom)
            {
                try
                {
                    MonkeAgent.instance.rpcErrorMax = int.MaxValue;
                    MonkeAgent.instance.rpcCallLimit = int.MaxValue;
                    MonkeAgent.instance.logErrorMax = int.MaxValue;
                    PrivateManager.ClearField(MonkeAgent.instance, "userRPCCalls");
                    //MonkeAgent.instance.userRPCCalls.Clear();

                    PhotonNetwork.MaxResendsBeforeDisconnect = int.MaxValue;
                    PhotonNetwork.QuickResends = int.MaxValue;

                    PhotonNetwork.SendAllOutgoingCommands();
                }
                catch { }
            }
        }
        public static void AntiCosmetic(string id, string name)
        {
            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                if (rig.OwnsCosmetic(id))
                {
                    if (Safety.CurrentSafetyAction != 1) Main.TrackerQueue.Add(new Main.TrackerData
                    {
                        CosmeticID = id,
                        Room = PhotonNetwork.CurrentRoom.Name,
                        Player = rig.Creator.NickName
                    });

                    SafetyAction();

                    Notifications.SendNotification($"[<color=green>Anti Cosmetic</color>] {rig.Creator.NickName} owned the {name} so you have disconnected.");
                }
            }
        }

        public static void AntiReport()
        {
            foreach (GorillaPlayerScoreboardLine line in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
                if (line.linePlayer == NetworkSystem.Instance.LocalPlayer)
                {
                    Transform button = line.reportButton.gameObject.transform;
                    foreach (VRRig rig in VRRigCache.ActiveRigs)
                    {
                        if (!rig.isLocal)
                        {
                            if (Vector3.Distance(rig.leftHandTransform.position, button.position) < 0.5 ||
                                Vector3.Distance(rig.rightHandTransform.position, button.position) < 0.5)
                            {
                                SafetyAction();

                                Notifications.SendNotification($"[<color=red>Anti Report</color>] {rig.Creator.NickName} attempted to report you.");
                                Room.Disconnect();
                            }
                        }
                    }
                }
            }
        }

        public static void AntiMute()
        {
            foreach (GorillaPlayerScoreboardLine line in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
                if (line.linePlayer == NetworkSystem.Instance.LocalPlayer)
                {
                    Transform button = line.muteButton.gameObject.transform;
                    foreach (VRRig rig in VRRigCache.ActiveRigs)
                    {
                        if (!rig.isLocal)
                        {
                            if (Vector3.Distance(rig.leftHandTransform.position, button.position) < 0.5 ||
                                Vector3.Distance(rig.rightHandTransform.position, button.position) < 0.5)
                            {
                                SafetyAction();

                                Notifications.SendNotification($"[<color=red>Anti Mute</color>] {rig.Creator.NickName} attempted to mute you.");
                                Room.Disconnect();
                            }
                        }
                    }
                }
            }
        }

        public static int CurrentSafetyAction = 0;
        public static void SafetyAction()
        {
            if (CurrentSafetyAction == 0) Room.Disconnect();
            // 1 just does nothing
            else if (CurrentSafetyAction == 2) Room.Reconnect();
            else if (CurrentSafetyAction == 3) Room.JoinRandom();
            else if (CurrentSafetyAction == 4) RandomIdentity();
        }

        public static void RandomIdentity()
        {
            Main.SetName("gorilla" + Random.Range(1000, 9999));
            Main.SetColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        }
    }
}