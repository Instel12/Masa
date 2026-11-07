using System;
using GorillaTagScripts;
using HarmonyLib;

namespace Masa.Patches
{
    public class SubscriptionPatch
    {
        public static bool Enabled;

        [HarmonyPatch(typeof(SubscriptionManager), nameof(SubscriptionManager.IsLocalSubscribed))]
        public class Local
        {
            private static bool Prefix(ref bool __result)
            {
                if (!Enabled) return true;

                __result = true;
                return false;
            }
        }

        [HarmonyPatch(typeof(SubscriptionManager), nameof(SubscriptionManager.LocalSubscriptionStatus))]
        public class LocalStatus
        {
            private static bool Prefix(ref SubscriptionManager.SubscriptionStatus __result)
            {
                if (!Enabled) return true;

                __result = SubscriptionManager.SubscriptionStatus.Active;
                return false;
            }
        }

        [HarmonyPatch(typeof(SubscriptionManager), nameof(SubscriptionManager.LocalSubscriptionDetails))]
        public class LocalDetails
        {
            private static bool Prefix(ref SubscriptionManager.SubscriptionDetails __result)
            {
                if (!Enabled) return true;

                __result = new SubscriptionManager.SubscriptionDetails
                {
                    active = true,
                    daysAccrued = 0,
                    subscriptionFeatureSettings = new[] { true, true },
                    tier = int.MaxValue,
                    subscriptionActiveUntilDate = DateTime.MaxValue,
                    autoRenew = true,
                    autoRenewMonths = int.MaxValue
                };
                return false;
            }
        }
    }
}