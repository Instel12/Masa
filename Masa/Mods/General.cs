using System;
using GorillaLocomotion;
using GorillaNetworking;
using Masa.Classes;
using Masa.Menu;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using static GorillaNetworking.CosmeticsController;

namespace Masa.Mods
{
    class General
    {
        public static void JoinDiscord() => Application.OpenURL(Main.DiscordLink);

        public static void LockAllCosmetics()
        {
            CosmeticsController.instance.unlockedCosmetics.Clear();
            CosmeticsController.instance.unlockedBacks.Clear();
            CosmeticsController.instance.unlockedBadges.Clear();
            CosmeticsController.instance.unlockedChests.Clear();
            CosmeticsController.instance.unlockedFaces.Clear();
            CosmeticsController.instance.unlockedFurs.Clear();
            CosmeticsController.instance.unlockedHats.Clear();
            CosmeticsController.instance.unlockedPants.Clear();
            CosmeticsController.instance.unlockedPaws.Clear();
            CosmeticsController.instance.unlockedShirts.Clear();
            CosmeticsController.instance.unlockedTagFX.Clear();
            CosmeticsController.instance.unlockedThrowables.Clear();
            CosmeticsController.instance.unlockedHoverboards.Clear();

            CosmeticsController.instance.OnCosmeticsUpdated();
        }

        public static void UnlockAllCosmetics()
        {
            LockAllCosmetics();

            foreach (CosmeticItem cosmetic in CosmeticsController.instance.allCosmetics)
            {
                try
                {
                    CosmeticsController.instance.UnlockItem(cosmetic.itemName, false);
                }
                catch { }
            }
            CosmeticsController.instance.OnCosmeticsUpdated();
        }

        public static bool PlayerIDWait = false;
        public static void GetPlayerIDGun()
        {
            GunData Data = Main.DeployGun(true);
            if (Data.Triggering)
            {
                //string id = Data.Rig.Creator.GetPlayerRef().UserId;
                string id = NetworkSystem.Instance.GetUserID(Data.Rig.Creator.ActorNumber);
                GUIUtility.systemCopyBuffer = id;
                if (!PlayerIDWait) Notifications.SendNotification($"[<color=green>Player ID</color>] Copied ID: {id}");
                PlayerIDWait = true;
            }
            else
            {
                PlayerIDWait = false;
            }
        }

        public static void UnlockCompetetive()
        {
            GorillaComputer.instance.CompQueueUnlockButtonPress();
        }

        public static void PlayerInfoTags()
        {
            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                if (rig.Creator.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    TextMesh txt = new GameObject().AddComponent<TextMesh>();

                    txt.alignment = TextAlignment.Left;
                    txt.anchor = TextAnchor.LowerLeft;
                    txt.text = 
                        $"Name: {rig.Creator.NickName}\n" +
                        $"Color: <color=red>{Math.Round(rig.playerColor.r * 9)}</color> <color=green>{Math.Round(rig.playerColor.g * 9)}</color> <color=blue>{Math.Round(rig.playerColor.b * 9)}</color>\n" +
                        $"FPS: <color={FPSColor(rig.GetFPS())}>{rig.GetFPS()}</color>\n" +
                        $"Creation Date: {rig.GetCreationDate()}\n";
                    txt.transform.position = rig.headMesh.transform.position;
                    txt.transform.LookAt(GTPlayer.Instance.headCollider.transform.position);
                    txt.transform.Rotate(0, 180, 0);
                    txt.fontSize = 50;
                    txt.transform.localScale = Vector3.one * 0.01f;

                    GameObject.Destroy(txt, Time.deltaTime); // maybe it's ugly but it woks
                }
            }
        }

        public static string FPSColor(float fps)
        {
            if (fps >= 70) return "green";
            if (fps >= 45) return "yellow";
            return "red";
        }

        private static Vector3 archClick = Vector3.one * float.MaxValue;
        public static void PCButtonClick()
        {
            if (Mouse.current.leftButton.isPressed)
            {
                Ray ray = Camera.allCameras[1].ScreenPointToRay(Mouse.current.position.ReadValue());
                Physics.Raycast(ray, out var Ray, 512f, GTPlayer.Instance.locomotionEnabledLayers);

                if (archClick == Vector3.one * float.MaxValue) archClick = GorillaTagger.Instance.rightHandTriggerCollider.transform.localPosition;
                GorillaTagger.Instance.rightHandTriggerCollider.GetComponent<TransformFollow>().enabled = false;
                GorillaTagger.Instance.rightHandTriggerCollider.transform.position = Ray.point;
            }
            else
            {
                if (archClick != Vector3.one * float.MaxValue)
                {
                    GorillaTagger.Instance.rightHandTriggerCollider.transform.localPosition = archClick;
                    archClick = Vector3.one * float.MaxValue;
                }
                GorillaTagger.Instance.rightHandTriggerCollider.GetComponent<TransformFollow>().enabled = true;
            }
        }

        public static void DisablePCButtonClick()
        {
            if (archClick != Vector3.one * float.MaxValue)
            {
                GorillaTagger.Instance.rightHandTriggerCollider.transform.localPosition = archClick;
                archClick = Vector3.one * float.MaxValue;
            }
        }

        public static void UpdateNameTags()
        {
            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                rig.UpdateName(true);
            }
        }
    }
}
