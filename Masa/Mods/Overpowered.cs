using GorillaGameModes;
using Masa.Classes;
using Masa.Menu;
using UnityEngine;

namespace Masa.Mods
{
    class Overpowered
    {
        public static void AmITagged() // this is for testing the thing
        {
            Notifications.SendNotification(VRRig.LocalRig.IsTagged() ? "yup" : "nah");
        }

        public static float TagDelay;
        public static void TagPlayer(VRRig rig)
        {
            if (!NetworkSystem.Instance.InRoom)
            {
                Notifications.SendNotification("[<color=red>Error</color>] You are not in a room.");
                return;
            }

            if (!VRRig.LocalRig.IsTagged())
            {
                Notifications.SendNotification("[<color=red>Error</color>] You are not tagged.");
                return;
            }

            if (TagDelay < Time.time && !rig.IsTagged())
            {
                TagDelay = Time.time + 0.3f;

                VRRig.LocalRig.enabled = false;

                VRRig.LocalRig.leftHandTransform.position = rig.transform.position;
                VRRig.LocalRig.rightHandTransform.position = rig.transform.position;
                VRRig.LocalRig.transform.position = rig.transform.position;

                GameMode.ReportTag(rig.Creator);
                Safety.FlushRPCs();

                if (!rig.IsTagged()) VRRig.LocalRig.enabled = true;
            }
            else
            {
                VRRig.LocalRig.enabled = true;
                Notifications.SendNotification("[<color=red>Error</color>] Player is already tagged.");
            }
        }

        public static void TagGun()
        {
            GunData Data = Main.DeployGun(true);

            if (Data.Triggering)
            {
                Data.Rig.IsTagged();
                if (Data.Triggering)
                {
                    if (!Data.Rig.IsTagged())
                    {
                        TagPlayer(Data.Rig);
                    }
                    else
                    {
                        VRRig.LocalRig.enabled = true;
                    }
                }
                else
                {
                    VRRig.LocalRig.enabled = true;
                }
            }
        }

        public static bool EveryoneTagged()
        {
            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                if (!rig.IsTagged()) return false;
            }
            return true;
        }

        public static void TagAll()
        {
            if (NetworkSystem.Instance.RoomPlayerCount > 3)
            {
                foreach (VRRig rig in VRRigCache.ActiveRigs)
                {
                    if (!rig.IsTagged()) TagPlayer(rig);
                }
            }
            else
            {
                Notifications.SendNotification($"[<color=red>Error</color>] There's only {NetworkSystem.Instance.RoomPlayerCount} player(s) in the room.");
            }

            if (EveryoneTagged())
            {
                Buttons.Buttans[Buttons.GetIndex("Tag All")].Enabled = false;
                VRRig.LocalRig.enabled = true;
            }
        }

        public static void TargetSpam()
        {
            if (NetworkSystem.Instance.IsMasterClient)
            {
                foreach (HitTargetNetworkState hitTargetNetworkState in GameObject.FindObjectsByType<HitTargetNetworkState>(FindObjectsSortMode.None))
                {
                    PrivateManager.SetField(hitTargetNetworkState, "hitCooldownTime", 0);
                    hitTargetNetworkState.TargetHit(Vector3.zero, Vector3.zero);
                }
            }
            else
            {
                Notifications.SendNotification($"[<color=red>Error</color>] You are not master.");
            }
        }

        public static float GreyZoneDelay = 0;
        public static void GreyZone(bool enable)
        {
            if (GreyZoneManager.Instance.HasAuthority)
            {
                if (enable)
                {
                    GreyZoneDelay = Time.time + 10;
                    if (Time.time > GreyZoneDelay) GreyZoneManager.Instance.ActivateGreyZoneAuthority();
                }
                else
                {
                    GreyZoneManager.Instance.DeactivateGreyZoneAuthority();
                }
            }
            else
            {
                Notifications.SendNotification($"[<color=red>Error</color>] You don't have ownership of the grey zone. You most likely need to be master.");
            }
        }
    }
}
