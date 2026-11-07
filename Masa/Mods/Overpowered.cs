using GorillaGameModes;
using Masa.Classes;
using Masa.Menu;
using Photon.Pun;
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

        public static void TagAll()
        {
            if (NetworkSystem.Instance.RoomPlayerCount > 3)
            {
                foreach (VRRig rig in VRRigCache.ActiveRigs)
                {
                    if (!rig.IsTagged()) TagPlayer(rig);
                }
                VRRig.LocalRig.enabled = true;
                
            }
            else
            {
                Notifications.SendNotification($"[<color=red>Error</color>] There's only {NetworkSystem.Instance.RoomPlayerCount} players in the room.");
            }

            Buttons.Buttans[Buttons.GetIndex("Tag All")].Enabled = false;
        }
    }
}
