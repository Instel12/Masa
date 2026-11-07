using GorillaNetworking;
using Masa.Classes;
using Masa.Menu;
using UnityEngine;
using static GorillaNetworking.CosmeticsController;

namespace Masa.Mods
{
    class General
    {
        public static void UnlockAllCosmetics()
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

            foreach (CosmeticItem cosmetic in CosmeticsController.instance.allCosmetics)
            {
                try
                {
                    CosmeticsController.instance.UnlockItem(cosmetic.itemName, false);
                }
                catch { }
            }
            CosmeticsController.instance.OnCosmeticsUpdated?.Invoke();
        }

        public static bool PlayerIDWait = false;
        public static void GetPlayerIDGun()
        {
            GunData Data = Main.DeployGun(true);
            if (Data.Triggering)
            {
                string id = Data.Rig.Creator.GetPlayerRef().UserId;
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
    }
}
