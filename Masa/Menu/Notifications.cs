using System;
using System.Collections.Generic;
using GorillaLocomotion;
using TMPro;
using UnityEngine;

namespace Masa.Menu
{
    class Notifications
    {
        [Serializable]
        public class Notification
        {
            public string Message;
            public float SendTime;
        }

        public static NotificationPosition Position = NotificationPosition.Head;
        public static List<Notification> Notifications_ = new List<Notification>();
        private static TextMeshPro TMP;

        public static void Update()
        {
            if (TMP == null)
            {
                TMP = new GameObject().AddComponent<TextMeshPro>();

                TMP.font = Main.MenuFont;
                TMP.fontSize = 0.2f;
                TMP.alignment = TextAlignmentOptions.Center;

                Main.RegisterTMP(TMP);
            }

            switch (Position)
            {
                case NotificationPosition.Head:
                    TMP.transform.rotation = GTPlayer.Instance.headCollider.transform.rotation;
                    TMP.transform.position = GTPlayer.Instance.headCollider.transform.position + GTPlayer.Instance.headCollider.transform.forward;
                    break;
                case NotificationPosition.Hand:
                    TMP.transform.LookAt(GTPlayer.Instance.headCollider.transform.position);
                    TMP.transform.Rotate(0, 180, 0);
                    TMP.transform.position = GTPlayer.Instance.LeftHand.controllerTransform.transform.position;
                    break;
            }

            TMP.text = "";

            TMP.gameObject.layer = Buttons.Buttans[Buttons.GetIndex("Streamer Notifications")].Enabled ? 19 : 0;

            TMP.fontMaterial.shader = Main.ChamsShader;

            for (int i = Notifications_.Count - 1; i >= 0; i--)
            {
                Notification notification = Notifications_[i];

                if (notification.SendTime + 5f <= Time.time)
                {
                    Notifications_.RemoveAt(i);
                }
                else
                {
                    TMP.text += notification.Message + "\n";
                }
            }
        }

        public static bool Enabled = true;
        public static void SendNotification(string message)
        {
            if (Enabled)
            {
                Notifications_.Add(new Notification
                {
                    Message = message,
                    SendTime = Time.time
                });
                //Main.PlayOneShot2D(Main.NotificationSound);
            }
        }

        public enum NotificationPosition
        {
            Head,
            Hand
        }
    }
}
